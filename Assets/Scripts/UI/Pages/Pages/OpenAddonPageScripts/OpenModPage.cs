using Assets.Scripts;
using Assets.Scripts.UI.Pages.Pages.OpenAddonPageScripts;
using Assets.Scripts.UI.UIStates.UICore.ElementContainers;
using Assets.Scripts.UI.UIStates.UICore.PageStacks;
using Scripts.AddonData.AddonOpen;
using Scripts.FileLoaders;
using Scripts.ObjectPoolPattern;
using Scripts.UI.NativeMessageBoxScripts;
using Scripts.UI.UIStates.DialogueBoxScripts;
using Scripts.UI.UIStates.UICore;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Localization;

namespace Scripts.UI.UIStates.OpenAddonPageScripts
{
	public class OpenModPage : UISimplePage
	{
		public static ICreation<OpenModPageView> Creator;

		private readonly OpenModPageView _view;
		private readonly OpenModPageModel _model;

		private CancellationTokenSource _cancellationToken;

		public OpenModPage(int addonId, SimplePageStack mainPageStack) : base(mainPageStack)
		{
			_view = Creator.Create();
			_view.ReloadButton.onClick.AddListener(LoadAddon);
			_view.OpenAddonButton.Button.onClick.AddListener(OpenAddon);
			_model = new OpenModPageModel(addonId);
			LoadAddon();
		}

		private bool IsLoading
		{
			get
			{
				return _cancellationToken != null;
			}
		}

		public override void OnEscapePressed()
		{
			if(!_view.IsHideByEscape)
			{
				return;
			}

			if(IsLoading)
			{
				ShowDialogueBoxWithCloseQuestion();
			}
			else
			{
				MainPageStack.HideLast();
			}
		}

		public override void Dispose()
		{
			base.Dispose();
			_model.OnProgressUpdate -= OnLoadingProgressUpdate;
			_cancellationToken?.Cancel();
			GameObject.Destroy(_view.gameObject);
		}

		protected override SimplePageView GetSimplePageView()
		{
			return _view;
		}

		private async void LoadAddon()
		{
			_cancellationToken = new CancellationTokenSource();
			CancellationToken token = _cancellationToken.Token;
			try
			{
				OnLoadingStart();
				var result = await _model.LoadAddon(token);
				token.ThrowIfCancellationRequested();
				OnLoadingEnd(result);
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

		private void ShowDialogueBoxWithCloseQuestion()
		{
			var dialogueBox = new DialogueBox(this);
			dialogueBox.OnYesButtonClick += MainPageStack.HideLast;
			dialogueBox.Text.StringReference = _view.AddonInstallationIncompleteText;
			LocalPageStack.ShowLast(dialogueBox);
		}

		private void OnLoadingStart()
		{
			_view.OpenAddonButton.ActivateDisabler();
			_view.ReloadButton.gameObject.SetActive(false);
			DeactivateResultTexts();
			_view.WaitText.gameObject.SetActive(true);
			OnLoadingProgressUpdate(0);
		}

		private void DeactivateResultTexts()
		{
			_view.WaitText.gameObject.SetActive(false);
			_view.LoadingSuccessText.gameObject.SetActive(false);
			_view.LoadingUnsuccessText.gameObject.SetActive(false);
		}

		private void OnLoadingProgressUpdate(float value)
		{
			_view.LoadingBar?.UpdateProgress(value);
			_view.PercentsText?.UpdateProgress(value);
		}

		private void OnLoadingEnd(LoadStatus addonLoadResult)
		{
			DeactivateResultTexts();
			if(addonLoadResult == LoadStatus.Success)
			{
				_view.LoadingSuccessText.gameObject.SetActive(true);
				_view.OpenAddonButton.DeactivateDisabler();
			}
			else
			{
				_view.ReloadButton.gameObject.SetActive(true);
				_view.LoadingUnsuccessText.gameObject.SetActive(true);
			}

			OnLoadingProgressUpdate(1);
			ShowNativeMessageBoxIfError(addonLoadResult);
		}

		private async void ShowNativeMessageBoxIfError(LoadStatus result)
		{
			if(result == LoadStatus.Success || result == LoadStatus.Cancel) 
			{
				return;
			}

			LocalizedString error = result switch
			{
				LoadStatus.NoAccess => _view.MessageBoxLocalizedMessages.NoAccess,
				LoadStatus.DiskIsFull => _view.MessageBoxLocalizedMessages.DiskIsFull,
				LoadStatus.ConnectionError => _view.MessageBoxLocalizedMessages.ConnectionError,
				LoadStatus.InternalError => _view.MessageBoxLocalizedMessages.InternalError,
				LoadStatus.UnknownError => _view.MessageBoxLocalizedMessages.UnknownError,
				_ => throw new ArgumentOutOfRangeException(nameof(result), result, null)
			};

			var errorLocalization = await error.GetLocalizedStringAsync().Task;
			NativeMessageBoxWrapper.Show(errorLocalization);
		}

		private async void OpenAddon()
		{
			OpenAddonResult result = _model.OpenAddon();
			LocalizedString errorLocalization = result switch
			{
				OpenAddonResult.Success => null,
				OpenAddonResult.Deleted => _view.MessageBoxLocalizedMessages.FileMissing,
				OpenAddonResult.NotInstalled => _view.MessageBoxLocalizedMessages.MineNotInstalled,
				OpenAddonResult.Unknown => _view.MessageBoxLocalizedMessages.UnknownError,
				_ => throw new ArgumentOutOfRangeException(nameof(result), result, null)
			};

			if(result == OpenAddonResult.Success)
			{
				EventExecutorOnApplicationFocus.OnApplicationFocusEvent += ShowRateBoxOnBackToApp;
			}

			if(errorLocalization != null)
			{
				var error = await errorLocalization.GetLocalizedStringAsync().Task;
				NativeMessageBoxWrapper.Show(error);
			}
		}

		private void ShowRateBoxOnBackToApp(bool focus)
		{
			if(focus)
			{
				EventExecutorOnApplicationFocus.OnApplicationFocusEvent -= ShowRateBoxOnBackToApp;
				Debug.Log("RateInApp");
				InAppReview.RateInApp();
			}
		}
	}
}
