using Assets.Scripts.UI.UIStates.SideMenuScripts.UI;
using Scripts.ObjectPoolPattern;
using UnityEngine;
using Scripts.UI.UIStates.UICore;
using Assets.Scripts.UI.UIStates.UICore.ElementContainers;
using Assets.Scripts.UI.UIPages.UICore.SidePageScripts;

namespace Assets.Scripts.UI.UIStates.SideMenuScripts
{
	public class SideMenu : UISidePage
	{
		public static ICreation<SideMenuView> Creator;

		private readonly SideMenuView _view;

		public SideMenu(UISimplePage parent) : base(parent)
		{
			_view = Creator.Create();
			_view.Parent = Parent.Transform;
			_view.FavoritesButton.onClick.AddListener(MainPageStack.HideLast);
			_view.InstructionDropdown.onValueChanged.AddListener((value) => MainPageStack.HideLast());
			_view.PrivacyPolicyButton.onClick.AddListener(MainPageStack.HideLast);
			_view.SettingsButton.onClick.AddListener(MainPageStack.HideLast);
			_view.ContactUsButton.onClick.AddListener(MainPageStack.HideLast);
			_view.BackgroundButton.onClick.AddListener(MainPageStack.HideLast);
			_view.SideMenu.OnEndHideSwipe += OnEscapePressed;
		}

		public override void Dispose()
		{
			base.Dispose();
			GameObject.Destroy(_view.gameObject);
		}

		protected override OverlayPageView GetOverlayPageView()
		{
			return _view;
		}
	}
}
