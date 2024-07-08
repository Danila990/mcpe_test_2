using Assets.Scripts.UI.UIPages.UICore.SidePageScripts;
using Assets.Scripts.UI.UIPages.UICore.SidePageScripts.Elements;
using Assets.Scripts.UI.UIStates.UICore.Animations.OverlayPage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.UIStates.SideMenuScripts.UI
{
	[RequireComponent(typeof(SidePagePositionAnimator))]
	public class SideMenuView : SidePageView
	{
		[field: SerializeField]
		public Button FavoritesButton
		{
			get;
			private set;
		}

		[field: SerializeField]
		public TMP_Dropdown InstructionDropdown
		{
			get;
			private set;
		}

		[field: SerializeField]
		public Button PrivacyPolicyButton
		{
			get;
			private set;
		}

		[field: SerializeField]
		public Button SettingsButton
		{
			get;
			private set;
		}

		[field: SerializeField]
		public Button ContactUsButton
		{
			get;
			private set;
		}

		[field: SerializeField]
		public Button BackgroundButton
		{
			get;
			private set;
		}

		[field: SerializeField]
		public SidePageDraggableElement SideMenu
		{
			get;
			private set;
		}

		public override void EnableInteraction(bool isEnable)
		{
			base.EnableInteraction(isEnable);
			if(!isEnable)
			{
				InstructionDropdown.onValueChanged.RemoveAllListeners();
			}
		}
	}
}
