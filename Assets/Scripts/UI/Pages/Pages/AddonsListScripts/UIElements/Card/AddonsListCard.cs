using Assets.Scripts.UI.UIPages.Pages.AddonsListScripts.UIElements.Card;
using Assets.Scripts.UI.UIStates.AddonsListScripts.AddonsData;
using Assets.Scripts.UI.UIStates.UICore.PageStacks;
using Scripts.ObjectPoolPattern;
using Scripts.UI.UIStates.AddonPageScripts;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Scripts.UI.UIStates.AddonsListScripts.AddonsData
{
	public class AddonsListCard : IDisposable
	{
		public static IPool<AddonsListCardView> Pool;

		private readonly AddonsListCardView _view;
		private readonly AddonsListCardModel _model;

		private CancellationTokenSource _cancellationToken;

		public AddonsListCard(int addonId)
		{
			_view = Pool.GetFree();
			_view.OnCardButton.onClick.AddListener(CardClicked);
			_model = new AddonsListCardModel(addonId);
			LoadPreviewAddonData();
		}

		public void Dispose()
		{
			_cancellationToken?.Cancel();
			Pool.SetFree(_view);
		}

		public void SetParent(Transform parent)
		{
			_view.gameObject.transform.SetParent(parent, false);
		}

		private async void LoadPreviewAddonData()
		{
			_cancellationToken = new CancellationTokenSource();
			CancellationToken token = _cancellationToken.Token;
			try
			{
				var setAddonPreviewDataTask = SetAddonPreviewData(token);
				var setFavoriteStateTask = SetFavoriteState(token);
				await Task.WhenAll(setAddonPreviewDataTask, setFavoriteStateTask);
			}
			catch(OperationCanceledException)
			{
			}
			finally 
			{
				_cancellationToken.Dispose();
				_cancellationToken = null;
			}
		}

		private async Task SetAddonPreviewData(CancellationToken token) 
		{
			var previewData = await _model.GetPreviewData(token);
			token.ThrowIfCancellationRequested();
			_view.AddonNameText.LocalizationId = previewData.AddonNameLocalizationId;
			_view.PreviewImage.Load(previewData.PreviewImageKey);
		}

		private async Task SetFavoriteState(CancellationToken token) 
		{
			var isFavorite = await _model.IsAddonFavorite(token);
			token.ThrowIfCancellationRequested();
			_view.IsFavoriteToggle.isOn = isFavorite;
		}

		private void CardClicked()
		{
			var addonPage = new AddonPage(_model.AddonId, SimplePageStack.PageStack);
			addonPage.OnHide += UpdateFavoriteState;
			SimplePageStack.PageStack.ShowLast(addonPage);
		}

		private async void UpdateFavoriteState() 
		{
			if(_cancellationToken != null) 
			{
				return;
			}

			_cancellationToken = new CancellationTokenSource();
			CancellationToken token = _cancellationToken.Token;
			try
			{
				await SetFavoriteState(token);
			}
			catch(OperationCanceledException)
			{
			}
			finally
			{
				_cancellationToken.Dispose();
				_cancellationToken = null;
			}
		}
	}
}
