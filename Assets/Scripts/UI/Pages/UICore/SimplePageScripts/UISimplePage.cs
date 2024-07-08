using Assets.Scripts.UI.UIStates.UICore.BaseStacks;
using Assets.Scripts.UI.UIStates.UICore.ElementContainers;
using Assets.Scripts.UI.UIStates.UICore.PageStacks;
using UnityEngine;

namespace Scripts.UI.UIStates.UICore
{
	public abstract class UISimplePage : UIBasePage
	{
		public readonly SimplePageStack MainPageStack;
		public readonly LocalPageStack LocalPageStack;

		private SimplePageAnimator _animator;

		public UISimplePage(SimplePageStack mainPageStack)
		{
			MainPageStack = mainPageStack;
			LocalPageStack = new LocalPageStack();
		}

		public override Transform Transform => GetSimplePageView().transform;

		public bool IsShowInterAdAfterHide => GetSimplePageView().IsShowInterAdAfterHide;

		private SimplePageAnimator Animator
		{
			get
			{
				if(_animator == null)
				{
					_animator = GetSimplePageView().GetComponent<SimplePageAnimator>();
				}

				return _animator;
			}
		}

		public virtual void MoveIn()
		{
			Animator.MoveIn();
		}

		public virtual void MoveOut()
		{
			Animator.MoveOut();
		}

		public override void OnEscapePressed()
		{
			if(GetSimplePageView().IsHideByEscape)
			{
				MainPageStack.HideLast();
			}
		}

		public override void Dispose()
		{
			foreach(UIBasePage localPage in LocalPageStack)
			{
				localPage.Dispose();
			}

			base.Dispose();
		}

		protected override BasePageView GetBasePageView()
		{
			return GetSimplePageView();
		}

		protected abstract SimplePageView GetSimplePageView();
	}
}
