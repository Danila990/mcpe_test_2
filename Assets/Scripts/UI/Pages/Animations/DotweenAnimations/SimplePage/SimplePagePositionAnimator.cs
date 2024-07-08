using Assets.Scripts.UI.UIStates.UICore.ElementContainers;
using DG.Tweening;
using Scripts.UI.UIStates.UICore.Animations.AnimationScriptableObjects.AnimationParametersByPosition;
using UnityEngine;

namespace Scripts.UI.UIStates.UICore.Animations
{
	public class SimplePagePositionAnimator : SimplePageAnimator
	{
		[SerializeField] private SimplePagePositionAnimationParameters _animations;

		private Sequence _nowAnimation;
		private RectTransform _transform;

		public override bool IsNowPlayingAnyAnimation => _nowAnimation.IsPlaying();

		private void Awake()
		{
			_nowAnimation = DOTween.Sequence();
			// сразу "убиваю" анимацию, потому что по умолчанию она IsPlaying = true
			_nowAnimation.Kill(true);
			_transform = GetComponent<RectTransform>();
		}

		private void OnDestroy()
		{
			_nowAnimation.Kill(true);
		}

		public override void Show()
		{
			_nowAnimation.Kill();
			_transform.localPosition = _animations.ShowAnimation.AnimationStartPosition * GetCanvasSize();
			_nowAnimation = StartAnimation(_animations.ShowAnimation);
			InvokeOnShowAnimationStart();
			_nowAnimation.onKill += InvokeOnShowAnimationEnd;
		}

		public override void MoveOut()
		{
			_nowAnimation.Kill();
			_nowAnimation = StartAnimation(_animations.MoveOutAnimation);
			InvokeOnMoveOutAnimationStart();
			_nowAnimation.onKill += () => gameObject.SetActive(false);
			_nowAnimation.onKill += InvokeOnMoveOutAnimationEnd;
		}

		public override void MoveIn()
		{
			if(!IsNowPlayingAnyAnimation)
			{
				_transform.localPosition = _animations.MoveInAnimation.AnimationStartPosition * GetCanvasSize();
			}

			_nowAnimation.Kill();
			gameObject.SetActive(true);
			_nowAnimation = StartAnimation(_animations.MoveInAnimation);
			InvokeOnMoveInAnimationStart();
			_nowAnimation.onKill += InvokeOnMoveInAnimationEnd;
		}

		public override void Hide()
		{
			_nowAnimation.Kill();
			_nowAnimation = StartAnimation(_animations.HideAnimation);
			InvokeOnHideAnimationStart();
			_nowAnimation.onKill += InvokeOnHideAnimationEnd;
		}

		private Sequence StartAnimation(PositionAnimation animationParameters)
		{
			Vector2 endPosition = animationParameters.AnimationEndPosition * GetCanvasSize();
			float duration = GetDuration(animationParameters);
			Tween animationByX = _transform
				.DOLocalMoveX(endPosition.x, duration)
				.SetEase(animationParameters.AnimationXCurve);
			Tween animationByY = _transform
				.DOLocalMoveY(endPosition.y, duration)
				.SetEase(animationParameters.AnimationYCurve);
			Sequence result = DOTween.Sequence();
			result.Append(animationByX);
			result.Append(animationByY);
			return result;
		}

		private float GetDuration(PositionAnimation animationParameters)
		{
			Vector2 startPosition = animationParameters.AnimationStartPosition * GetCanvasSize();
			Vector2 endPosition = animationParameters.AnimationEndPosition * GetCanvasSize();
			float fromStartToEndMagnitude = (endPosition - startPosition).magnitude;
			float fromNowPositionToEndMagnitude = (endPosition - (Vector2)_transform.localPosition).magnitude;
			float duration = fromNowPositionToEndMagnitude / fromStartToEndMagnitude *  
				animationParameters.AbsolutDuration;
			return duration;
		}

		private Vector2 GetCanvasSize()
		{
			return ((RectTransform)transform.parent).GetSizeWithCurrentAnchors();
		}
	}
}