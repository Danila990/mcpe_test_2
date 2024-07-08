using Assets.Scripts.UI.UIStates.UICore.Animations.OverlayPage;
using Assets.Scripts.UI.UIStates.UICore.BaseStacks;
using Assets.Scripts.UI.UIStates.UICore.ElementContainers;
using UnityEngine;

namespace Scripts.UI.UIStates.UICore
{
	public abstract class UIOverlayPage : UIBasePage
	{
		public readonly UISimplePage Parent;

		public UIOverlayPage(UISimplePage parent)
		{
			Parent = parent;
		}

		public LocalPageStack MainPageStack => Parent.LocalPageStack;

		public override Transform Transform => GetOverlayPageView().transform;

		public override void OnEscapePressed()
		{
			if(GetOverlayPageView().IsHideByEscape)
			{
				MainPageStack.HideLast();
			}
		}

		protected override BasePageView GetBasePageView()
		{
			return GetOverlayPageView();
		}

		protected abstract OverlayPageView GetOverlayPageView();
	}
}
