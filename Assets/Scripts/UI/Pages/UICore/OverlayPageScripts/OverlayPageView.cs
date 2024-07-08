using Assets.Scripts.UI.UIStates.UICore.Animations.OverlayPage;
using Scripts.UI.UIStates.UICore;
using UnityEngine;

namespace Assets.Scripts.UI.UIStates.UICore.ElementContainers
{
	[RequireComponent(typeof(OverlayPageAnimator))]
	public abstract class OverlayPageView : BasePageView
	{
		[field: SerializeField]
		public bool IsHideByEscape
		{
			get;
			private set;
		}
	}
}
