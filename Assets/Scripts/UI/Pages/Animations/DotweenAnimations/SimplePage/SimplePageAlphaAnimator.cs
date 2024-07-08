using Assets.Scripts.UI.Pages.Animations.DotweenAnimations.SimplePage;
using Assets.Scripts.UI.UIStates.UICore.ElementContainers;
using DG.Tweening;
using Scripts.UI.UIStates.UICore.Animations.AnimationScriptableObjects.AnimationParametersByCanvasGroup;
using UnityEngine;

namespace Assets.Scripts.UI.UIStates.UICore.Animations.SimplePage
{
	[RequireComponent(typeof(CanvasGroup))]
	public class SimplePageAlphaAnimator : SimplePageAnimator
	{
		[SerializeField] private SimplePageAlphaAnimationParameters _parameters;

		private Sequence _nowAnimation;
		private CanvasGroup _canvasGroup;

		public override bool IsNowPlayingAnyAnimation => _nowAnimation.IsPlaying();

		private void Awake()
		{
			_canvasGroup = GetComponent<CanvasGroup>();
		}

		public override void Show()
		{
			_nowAnimation.Kill();
			_canvasGroup.alpha = _parameters.ShowAnimation.AnimationStartAlpha;
			_nowAnimation = StartAnimation(_parameters.ShowAnimation);
			InvokeOnShowAnimationStart();
			_nowAnimation.onKill += InvokeOnShowAnimationEnd;
		}

		public override void MoveOut()
		{
			_nowAnimation.Kill();
			_nowAnimation = StartAnimation(_parameters.MoveOutAnimation);
			InvokeOnMoveOutAnimationStart();
			_nowAnimation.onKill += () => gameObject.SetActive(false);
			_nowAnimation.onKill += InvokeOnMoveOutAnimationEnd;
		}

		public override void MoveIn()
		{
			if(!IsNowPlayingAnyAnimation)
			{
				_canvasGroup.alpha = _parameters.MoveInAnimation.AnimationStartAlpha;
			}

			_nowAnimation.Kill();
			gameObject.SetActive(true);
			_nowAnimation = StartAnimation(_parameters.MoveInAnimation);
			InvokeOnMoveInAnimationStart();
			_nowAnimation.onKill += InvokeOnMoveInAnimationEnd;
		}

		public override void Hide()
		{
			_nowAnimation.Kill();
			_nowAnimation = StartAnimation(_parameters.HideAnimation);
			InvokeOnHideAnimationStart();
			_nowAnimation.onKill += InvokeOnHideAnimationEnd;
		}

		private Sequence StartAnimation(AlphaAnimation animationParameters)
		{
			Sequence animation = DOTween.Sequence();
			float duration = GetDuration(animationParameters);
			Tween animationCanvasGroup = _canvasGroup
				.DOFade(animationParameters.AnimationEndAlpha, duration)
				.SetEase(animationParameters.AnimationCurve);
			animation.Append(animationCanvasGroup);
			return animation;
		}

		private float GetDuration(AlphaAnimation animationParameters)
		{
			float startAlpha = animationParameters.AnimationStartAlpha;
			float endAlpha = animationParameters.AnimationEndAlpha;
			float fromStartAlphaToEndMagnitude = Mathf.Abs(endAlpha - startAlpha);
			float fromNowAlphaToEndMagnitude = Mathf.Abs(endAlpha - _canvasGroup.alpha);
			float duration = fromNowAlphaToEndMagnitude / fromStartAlphaToEndMagnitude * 
				animationParameters.AbsolutDuration;
			return duration;
		}
	}
}
