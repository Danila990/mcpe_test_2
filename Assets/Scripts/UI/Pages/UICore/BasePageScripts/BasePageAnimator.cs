using DG.Tweening;
using System;
using UnityEngine;

namespace Scripts.UI.UIStates.UICore.Animations
{
	public abstract class BasePageAnimator : MonoBehaviour
	{
		public event Action OnShowAnimationStart;
		public event Action OnShowAnimationEnd;
		public event Action OnHideAnimationStart;
		public event Action OnHideAnimationEnd;

		public abstract bool IsNowPlayingAnyAnimation 
		{
			get; 
		}

		public abstract void Show();

		public abstract void Hide();

		protected void InvokeOnShowAnimationStart()
		{
			OnShowAnimationStart?.Invoke();
		}

		protected void InvokeOnShowAnimationEnd()
		{
			OnShowAnimationEnd?.Invoke();
		}

		protected void InvokeOnHideAnimationStart()
		{
			OnHideAnimationStart?.Invoke();
		}

		protected void InvokeOnHideAnimationEnd()
		{
			OnHideAnimationEnd?.Invoke();
		}
	}
}