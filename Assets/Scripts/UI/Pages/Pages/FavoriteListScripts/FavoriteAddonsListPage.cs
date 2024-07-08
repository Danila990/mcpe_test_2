using Assets.Scripts.UI.UIPages.Pages.AddonsListScripts.FavoriteList.UI;
using Assets.Scripts.UI.UIStates.UICore.ElementContainers;
using Assets.Scripts.UI.UIStates.UICore.PageStacks;
using Scripts.ObjectPoolPattern;
using Scripts.UI.UIStates.UICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.UI.UIPages.Pages.AddonsListScripts.FavoriteList
{
	public class FavoriteAddonsListPage : UISimplePage
	{
		public static ICreation<FavoriteAddonsListPageView> Creator;

		private readonly FavoriteAddonsListPageView _view;
		private readonly FavoriteAddonsListPageModel _model;

		private Task _getAddonsIdTask = Task.CompletedTask;
		private CancellationTokenSource _cancellationToken;
		private bool _hasFavorites = false;

		public FavoriteAddonsListPage(SimplePageStack mainPageStack) : base(mainPageStack)
		{
			_view = Creator.Create();
			_view.HasNoFavoritesText.gameObject.SetActive(false);
			_view.AddonsScrollRect.OnNeedAddAddons += NeedAddAddons;
			_model = new FavoriteAddonsListPageModel();
		}

		public override void Dispose()
		{
			base.Dispose();
			_cancellationToken?.Cancel();
			GameObject.Destroy(_view.gameObject);
		}

		protected override SimplePageView GetSimplePageView()
		{
			return _view;
		}

		private void NeedAddAddons()
		{
			if(!_getAddonsIdTask.IsCompleted)
			{
				return;
			}

			_getAddonsIdTask = LoadNextAddons();
		}

		private async Task LoadNextAddons()
		{
			_cancellationToken = new CancellationTokenSource();
			CancellationToken token = _cancellationToken.Token;
			try
			{
				List<int> addonsId = await _model.LoadNextAddonsPageData(token);
				token.ThrowIfCancellationRequested();
				_view.AddonsScrollRect.AddAddons(addonsId);
				ShowNoFavoritesTextIfNeed(addonsId);
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

		private void ShowNoFavoritesTextIfNeed(IEnumerable<int> addonsId) 
		{
			_hasFavorites = _hasFavorites || addonsId.Any();
			if(!_hasFavorites)
			{
				_view.HasNoFavoritesText.gameObject.SetActive(true);
			}
		}
	}
}