using DG.Tweening;
using Scripts.UI.UIStates.UICore.Animations;
using System;

namespace Assets.Scripts.UI.UIStates.UICore.ElementContainers
{
	public abstract class SimplePageAnimator : BasePageAnimator
	{
		public event Action OnMoveOutAnimationStart;
		public event Action OnMoveOutAnimationEnd;
		public event Action OnMoveInAnimationStart;
		public event Action OnMoveInAnimationEnd;

		public abstract void MoveOut();

		public abstract void MoveIn();

		protected void InvokeOnMoveOutAnimationStart()
		{
			OnMoveOutAnimationStart?.Invoke();
		}

		protected void InvokeOnMoveOutAnimationEnd()
		{
			OnMoveOutAnimationEnd?.Invoke();
		}

		protected void InvokeOnMoveInAnimationStart()
		{
			OnMoveInAnimationStart?.Invoke();
		}

		protected void InvokeOnMoveInAnimationEnd()
		{
			OnMoveInAnimationEnd?.Invoke();
		}
	}
}
