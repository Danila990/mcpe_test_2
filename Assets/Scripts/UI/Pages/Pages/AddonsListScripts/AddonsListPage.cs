using Assets.Scripts.Extensions;
using Assets.Scripts.UI.UIPages.Pages.AddonsListScripts;
using Assets.Scripts.UI.UIStates.UICore.ElementContainers;
using Assets.Scripts.UI.UIStates.UICore.PageStacks;
using Scripts.ObjectPoolPattern;
using Scripts.UI.UIStates.UICore;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Scripts.UI.UIStates.AddonsListScripts
{
	public class AddonsListPage : UISimplePage
	{
		public static ICreation<AddonsListPageView> Creator;

		private readonly AddonsListPageView _view;
		private readonly AddonsListPageModel _model;

		private Task _getAddonsIdTask = Task.CompletedTask;
		private CancellationTokenSource _cancellationToken;

		public AddonsListPage(int? category, SimplePageStack mainPageStack) : base(mainPageStack)
		{
			_view = Creator.Create();
			_view.AddonsScrollRect.OnNeedAddAddons += NeedAddAddons;
			_model = new AddonsListPageModel(category);
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
			_getAddonsIdTask.LogException();
		}

		private async Task LoadNextAddons()
		{
			_cancellationToken = new CancellationTokenSource();
			CancellationToken token = _cancellationToken.Token;
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
				_cancellationToken.Dispose();
				_cancellationToken = null;
			}
		}
	}
}
