using Assets.Scripts.UI.UIStates.UICore.Animations.OverlayPage;
using DG.Tweening;
using Scripts.UI.UIStates.UICore.Animations.AnimationScriptableObjects.AnimationParametersByCanvasGroup;
using UnityEngine;

namespace Scripts.UI.UIStates.UICore.Animations
{
	[RequireComponent(typeof(CanvasGroup))]
	public class OverlayPageAlphaAnimator : OverlayPageAnimator
	{
		[SerializeField] private OverlayPageAlphaAnimationParameters animations;

		private Sequence _nowAnimation;
		private CanvasGroup _canvasGroup;

		public override bool IsNowPlayingAnyAnimation => _nowAnimation.IsPlaying();

		private void Awake()
		{
			_nowAnimation = DOTween.Sequence();
			// сразу "убиваю" анимацию, потому что по умолчанию она IsPlaying = true
			_nowAnimation.Kill(true);
			_canvasGroup = GetComponent<CanvasGroup>();
		}

		private void OnDestroy()
		{
			_nowAnimation.Kill(true);
		}

		public override void Show()
		{
			_nowAnimation.Kill();
			_canvasGroup.alpha = animations.ShowAnimation.AnimationStartAlpha;
			_nowAnimation = StartAnimation(animations.ShowAnimation);
			InvokeOnShowAnimationStart();
			_nowAnimation.onKill += InvokeOnShowAnimationEnd;
		}

		public override void Hide()
		{
			_nowAnimation.Kill();
			_nowAnimation = StartAnimation(animations.HideAnimation);
			InvokeOnHideAnimationStart();
			_nowAnimation.onKill += InvokeOnHideAnimationEnd;
		}

		private Sequence StartAnimation(AlphaAnimation animationParameters)
		{
			float duration = GetDuration(animationParameters);
			Tween animationCanvasGroup = _canvasGroup
				.DOFade(animationParameters.AnimationEndAlpha, duration)
				.SetEase(animationParameters.AnimationCurve);
			Sequence result = DOTween.Sequence();
			result.Append(animationCanvasGroup);
			return result;
		}

		private float GetDuration(AlphaAnimation animationParameters)
		{
			float startAlpha = animationParameters.AnimationStartAlpha;
			float endAlpha = animationParameters.AnimationEndAlpha;
			float fromStartAplhaToEndMagnitude = Mathf.Abs(endAlpha - startAlpha);
			float fromNowAlphaToEndMagnitude = Mathf.Abs(endAlpha - _canvasGroup.alpha);
			float duration = fromNowAlphaToEndMagnitude / fromStartAplhaToEndMagnitude * animationParameters.AbsolutDuration;
			return duration;
		}
	}
}