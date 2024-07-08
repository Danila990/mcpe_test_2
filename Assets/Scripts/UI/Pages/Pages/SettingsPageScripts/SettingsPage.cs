using Scripts.ObjectPoolPattern;
using Scripts.UI.UIStates.SettingsPageScripts;
using Scripts.UI.UIStates.UICore;
using Assets.Scripts.UI.UIStates.UICore.PageStacks;
using Assets.Scripts.UI.UIStates.UICore.ElementContainers;
using UnityEngine;

namespace Assets.Scripts.UI.UIStates.SettingsPageScripts
{
	public class SettingsPage : UISimplePage
	{
		public static ICreation<SettingsPageView> Creator;

		private SettingsPageView _view;

		public SettingsPage(SimplePageStack mainPageStack) : base(mainPageStack)
		{
			_view = Creator.Create();
		}

		public override void Dispose()
		{
			base.Dispose();
			GameObject.Destroy(_view.gameObject);
		}

		protected override SimplePageView GetSimplePageView()
		{
			return _view;
		}
	}
}