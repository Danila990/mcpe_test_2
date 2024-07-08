using Scripts.AD;
using Scripts.AddonData;
using Scripts.ObjectPoolPattern;
using Scripts.UI.UIStates.DialogueBoxScripts;
using Scripts.UI.UIStates.MessageBoxScripts;
using Scripts.UI.UIStates.OpenAddonPageScripts;
using Scripts.UI.UIStates.UICore;
using System;
using UnityEngine;
using System.Threading;
using UnityEngine.Localization;
using Assets.Scripts.UI.UIStates.UICore.PageStacks;
using Assets.Scripts.UI.UIStates.UICore.ElementContainers;
using Assets.Scripts.UI.UIPages.Pages.AddonPageScripts;
using System.Threading.Tasks;

namespace Scripts.UI.UIStates.AddonPageScripts
{
	public class AddonPage : UISimplePage
	{
		public static ICreation<AddonPageView> Creator;

		private readonly AddonPageView _view;
		private readonly AddonPageModel _model;

		private CancellationTokenSource _fillCancellationToken;
		private Task _setAddonFavoriteTask = Task.CompletedTask;
		private string _versions;

		public AddonPage(int addonId, SimplePageStack mainPageStack) : base(mainPageStack)
		{
			_view = Creator.Create();
			_view.RewardButton.onClick.AddListener(ShowAdWithRewardAction);
			_view.SupportedVersionsButton.gameObject.SetActive(false);
			_view.SupportedVersionsButton.onClick.AddListener(ShowSupportedVersions);
			_model = new AddonPageModel(addonId);
			FillPage();
		}

		public override void Dispose()
		{
			base.Dispose();
			_fillCancellationToken?.Cancel();
			GameObject.Destroy(_view.gameObject);
		}

		protected override SimplePageView GetSimplePageView()
		{
			return _view;
		}

		private async void FillPage()
		{
			_fillCancellationToken = new CancellationTokenSource();
			CancellationToken token = _fillCancellationToken.Token;
			try
			{
				var loadAddonDataTask = LoadAddonData(token);
				var loadImagesTask = LoadImages(token);
				var loadFavoriteStateTask = LoadFavoriteState(token);
				await Task.WhenAll(loadAddonDataTask, loadImagesTask, loadFavoriteStateTask);
			}
			catch(OperationCanceledException)
			{
			}
			finally
			{
				_fillCancellationToken.Dispose();
				_fillCancellationToken = null;
			}
		}

		private async Task LoadAddonData(CancellationToken token)
		{
			var addonData = await _model.GetAddonData(token);
			token.ThrowIfCancellationRequested();
			_view.HeaderText.LocalizationId = addonData.AddonNameLocalizationId;
			_view.DescriptionText.LocalizationId = addonData.DescriptionLocalizationId;
			_view.SupportedVersionsButton.gameObject.SetActive(addonData.Versions != null);
			_versions = addonData.Versions;
		}

		private async Task LoadImages(CancellationToken token)
		{
			var imageKeys = await _model.GetImageKeys(token);
			token.ThrowIfCancellationRequested();
			_view.ImagesScrollRect.LoadImages(imageKeys);
		}

		private async Task LoadFavoriteState(CancellationToken token)
		{
			var isFavorite = await _model.IsAddonFavorite(token);
			token.ThrowIfCancellationRequested();
			_view.FavoriteButton.isOn = isFavorite;
			_view.FavoriteButton.onValueChanged.AddListener(OnFavoriteStateChange);
		}

		private void ShowSupportedVersions()
		{
			LocalizedString supportedVersions = _view.SupportedVersionsText;
			supportedVersions.Arguments = new object[] { _versions };
			var supportedVersionsMessageBox = new MessageBox(this);
			supportedVersionsMessageBox.Text.StringReference = supportedVersions;
			LocalPageStack.ShowLast(supportedVersionsMessageBox);
		}

		private void ShowAdWithRewardAction()
		{
			Action rewardButtonAction = _model.RewardAdType switch
			{
				RewardAdType.InterAd => ShowInterAdWithRewardAction,
				RewardAdType.RewardAd => ShowRewardAdWithRewardAction,
				_ => throw new ArgumentException("Unknown RewardAdType"),
			};

			rewardButtonAction?.Invoke();
		}

		private void ShowInterAdWithRewardAction()
		{
			AdsController.Instance.ShowInterAd(result => StartLoadingAddonAndOpenModPage());
		}

		private void ShowRewardAdWithRewardAction()
		{
			var dialogueBox = new DialogueBox(this);
			dialogueBox.OnYesButtonClick += () => AdsController.Instance.ShowRewardAd(result => StartLoadingAddonAndOpenModPage());
			dialogueBox.Text.StringReference = _view.WatchRewardAdText;
			LocalPageStack.ShowLast(dialogueBox);
		}

		private void StartLoadingAddonAndOpenModPage()
		{
			var openModPage = new OpenModPage(_model.AddonId, MainPageStack);
			MainPageStack.ShowLast(openModPage);
		}

		private void OnFavoriteStateChange(bool isFavorite)
		{
			if(!_setAddonFavoriteTask.IsCompleted)
			{
				return;
			}

			_setAddonFavoriteTask = _model.SetAddonFavirite(isFavorite, default);
		}
	}
}
