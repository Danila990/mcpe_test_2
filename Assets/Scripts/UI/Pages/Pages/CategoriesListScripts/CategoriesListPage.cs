using Assets.Scripts.UI.UIStates.CategoriesListScripts.UI;
using Scripts.ObjectPoolPattern;
using Scripts.UI.UIStates.UICore;
using System.Threading;
using System;
using Assets.Scripts.UI.UIStates.UICore.PageStacks;
using Assets.Scripts.UI.UIStates.UICore.ElementContainers;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assets.Scripts.Extensions;

namespace Assets.Scripts.UI.UIStates.CategoriesListScripts
{
	public class CategoriesListPage : UISimplePage
	{
		public static ICreation<CategoriesListPageView> Creator;

		private readonly CategoriesListPageView _view;
		private readonly CategoriesListPageModel _model;

		private CancellationTokenSource _categoryCancellationToken;
		private Task _getAddonsIdTask = Task.CompletedTask;
		private CancellationTokenSource _addonsCancellationToken;

		public CategoriesListPage(int? parentCategory, SimplePageStack mainPageStack) : base(mainPageStack)
		{
			_view = Creator.Create();
			_view.CategoriesScrollRect.OnCardClick += SetChoosenCategoty;
			_view.AddonsScrollRect.OnNeedAddAddons += NeedAddAddons;
			_model = new CategoriesListPageModel(parentCategory);
			FillCategoryScrollRect();
		}

		public override void Dispose()
		{
			base.Dispose();
			_categoryCancellationToken?.Cancel();
			_addonsCancellationToken?.Cancel();
			GameObject.Destroy(_view.gameObject);
		}

		protected override SimplePageView GetSimplePageView()
		{
			return _view;
		}

		private async void FillCategoryScrollRect()
		{
			_categoryCancellationToken = new CancellationTokenSource();
			CancellationToken token = _categoryCancellationToken.Token;
			try
			{
				List<int> categoriesId = await _model.GetCategoriesId(token);
				token.ThrowIfCancellationRequested();
				_view.CategoriesScrollRect.AddCards(categoriesId);
				SetChoosenCategoty(categoriesId.First());
			}
			catch(OperationCanceledException)
			{
			}
			finally
			{
				_categoryCancellationToken.Dispose();
				_categoryCancellationToken = null;
			}
		}

		private void SetChoosenCategoty(int categoryId)
		{
			_model.SetCategoryToLoadAddons(categoryId);
			_addonsCancellationToken?.Cancel();
			_view.AddonsScrollRect.ToBaseState();
		}

		private void NeedAddAddons()
		{
			if(!_getAddonsIdTask.IsCompleted)
			{
				return;
			}

			_getAddonsIdTask = LoadNextAddons();
			_getAddonsIdTask.LogException();
		}

		private async Task LoadNextAddons()
		{
			_addonsCancellationToken = new CancellationTokenSource();
			CancellationToken token = _addonsCancellationToken.Token;
			try
			{
				var addonsId = await _model.LoadNextAddonsId(token);
				token.ThrowIfCancellationRequested();
				_view.AddonsScrollRect.AddAddons(addonsId);
			}
			catch(OperationCanceledException)
			{
			}
			finally
			{
				_addonsCancellationToken.Dispose();
				_addonsCancellationToken = null;
			}
		}
	}
}
