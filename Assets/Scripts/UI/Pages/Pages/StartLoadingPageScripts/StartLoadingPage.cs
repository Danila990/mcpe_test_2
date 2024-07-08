using Assets.Scripts.UI.Pages.Pages.StartLoadingPageScripts;
using Assets.Scripts.UI.UIStates.UICore.ElementContainers;
using Assets.Scripts.UI.UIStates.UICore.PageStacks;
using Scripts.FileLoaders;
using Scripts.ObjectPoolPattern;
using Scripts.UI.UIStates.MainMenuScripts;
using Scripts.UI.UIStates.UICore;
using System;
using System.Threading;
using UnityEngine;

namespace Scripts.UI.UIStates.StartLoadingPageScripts
{
	public class StartLoadingPage : UISimplePage
	{
		public static ICreation<StartLoadingPageView> Creator;

		private readonly StartLoadingPageView _view;
		private readonly StartLoadingPageModel _model;

		private CancellationTokenSource _cancellationToken;

		public StartLoadingPage(SimplePageStack mainPageStack) : base(mainPageStack)
		{
			_view = Creator.Create();
			_view.RetryButton.onClick.AddListener(Load);
			_view.LoadedButton.onClick.AddListener(OpenMainMenuPage);
			_model = new StartLoadingPageModel();
		}

		public override void Dispose()
		{
			base.Dispose();
			_cancellationToken?.Cancel();
			GameObject.Destroy(_view.gameObject);
		}

		public async void Load()
		{
			_cancellationToken = new CancellationTokenSource();
			var token = _cancellationToken.Token;
			try
			{
				OnLoadingStart();
				var result = await _model.Load(token);
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

		protected override SimplePageView GetSimplePageView()
		{
			return _view;
		}

		private void OnLoadingStart()
		{
			_view.PercentsText?.gameObject.SetActive(true);
			_view.RetryButton.gameObject.SetActive(false);
			_view.LoadedButton.gameObject.SetActive(false);
			DeactivateStatusTexts();
			_view.LoadingText.gameObject.SetActive(true);
			OnLoadingProgress(0);
		}

		private void DeactivateStatusTexts()
		{
			_view.LoadingText.gameObject.SetActive(false);
			_view.LoadedText.gameObject.SetActive(false);
			_view.FailedText.gameObject.SetActive(false);
		}

		private void OnLoadingProgress(float value)
		{
			_view.LoadingBar?.UpdateProgress(value);
			_view.PercentsText?.UpdateProgress(value);
		}

		private void OnLoadingEnd(LoadStatus result)
		{
			OnLoadingProgress(1);
			_view.PercentsText?.gameObject.SetActive(false);
			DeactivateStatusTexts();
			if(result == LoadStatus.Success)
			{
				_view.LoadedText.gameObject.SetActive(true);
				_view.LoadedButton.gameObject.SetActive(true);
			}
			else
			{
				_view.FailedText.gameObject.SetActive(true);
				_view.RetryButton.gameObject.SetActive(true);
			}
		}

		private void OpenMainMenuPage()
		{
			SimplePageStack.PageStack.HideLast();
			ShowMainPage();
		}

		private void ShowMainPage()
		{
			var mainPage = new MainMenuPage(SimplePageStack.PageStack);
			SimplePageStack.PageStack.ShowLast(mainPage);
		}
	}
}
