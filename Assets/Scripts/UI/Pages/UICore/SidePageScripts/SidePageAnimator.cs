using Assets.Scripts.UI.UIPages.UICore.SidePageScripts.Elements;
using Assets.Scripts.UI.UIStates.UICore.Animations.OverlayPage;
using UnityEngine;

namespace Assets.Scripts.UI.UIPages.UICore.SidePageScripts
{
	public abstract class SidePageAnimator : OverlayPageAnimator
	{
		[SerializeField] protected SidePageDraggableElement _sideMenuElement;
	}
}
