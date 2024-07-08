using System;
using Assets.Scripts.UI.UIStates.UICore.Animations.OverlayPage;
using Assets.Scripts.UI.UIStates.UICore.ElementContainers;
using Scripts.UI.UIStates.UICore.Animations;
using UnityEngine;

namespace Scripts.UI.UIStates.UICore
{
	public abstract class UIBasePage : IDisposable
	{
		private BasePageAnimator _animator;

		public event Action OnHide;

		public abstract Transform Transform 
		{
			get;
		}

		public bool IsInStack
		{
			get;
			private set;
		}

		private BasePageAnimator Animator
		{
			get
			{
				if(_animator == null)
				{
					_animator = GetBasePageView().GetComponent<BasePageAnimator>();
				}

				return _animator;
			}
		}

		public virtual void Show()
		{
			IsInStack = true;
			Animator.Show();
		}

		public virtual void Hide(bool disposeAfterHide = true)
		{
			IsInStack = false;
			OnHide?.Invoke();
			if(disposeAfterHide)
			{
				Animator.OnHideAnimationEnd += Dispose;
			}

			Animator.Hide();
		}

		public virtual void OnEscapePressed()
		{

		}

		public virtual void Dispose()
		{
		}

		protected abstract BasePageView GetBasePageView();
	}
}