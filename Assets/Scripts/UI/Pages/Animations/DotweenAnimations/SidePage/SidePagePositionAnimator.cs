using Assets.Scripts.UI.UIPages.UICore.SidePageScripts;
using Assets.Scripts.UI.UIStates.UICore.Animations.AnimationScriptableObjects.AnimationParametersByPosition;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.UI.UIStates.UICore.Animations.OverlayPage
{
	[RequireComponent(typeof(RectTransform))]
	public class SidePagePositionAnimator : SidePageAnimator
	{
		[SerializeField] private SideMenuAnimationParameters _animationParameters;

		private Sequence _nowAnimation;

		public override bool IsNowPlayingAnyAnimation => _nowAnimation.IsPlaying();

		private void Awake()
		{
			_nowAnimation = DOTween.Sequence();
			// сразу "убиваю" анимацию, потому что по умолчанию она IsPlaying = true
			_nowAnimation.Kill(true);
			_sideMenuElement.OnEndShowSwipe += ShowSideMenu;
		}

		private void OnDestroy()
		{
			_nowAnimation.Kill(true);
		}

		public override void Show()
		{
			_nowAnimation.Kill();
			_sideMenuElement.enabled = true;
			_sideMenuElement.transform.localPosition = _sideMenuElement.HidePositionBorder;
			_nowAnimation = StartAnimation(_sideMenuElement.ShowPositionBorder, 
				_animationParameters.AbsolutDuration);
			InvokeOnShowAnimationStart();
			_nowAnimation.onUpdate += _sideMenuElement.ReportProgress;
			_nowAnimation.onKill += InvokeOnShowAnimationEnd;
		}

		public override void Hide()
		{
			_nowAnimation.Kill();
			_sideMenuElement.enabled = false;
			_nowAnimation = StartAnimation(_sideMenuElement.HidePositionBorder,
				_animationParameters.AbsolutDuration);
			InvokeOnHideAnimationStart();
			_nowAnimation.onUpdate += _sideMenuElement.ReportProgress;
			_nowAnimation.onKill += InvokeOnHideAnimationEnd;
		}

		private void ShowSideMenu()
		{
			_nowAnimation = StartAnimation(_sideMenuElement.ShowPositionBorder,
				_animationParameters.AbsolutDuration);
			_nowAnimation.onUpdate += _sideMenuElement.ReportProgress;
		}

		private Sequence StartAnimation(Vector2 endPosition, float duration)
		{
			Tween animationByX = _sideMenuElement.transform
				.DOLocalMoveX(endPosition.x, duration)
				.SetEase(_animationParameters.AnimationXCurve);
			Tween animationByY = _sideMenuElement.transform
				.DOLocalMoveY(endPosition.y, duration)
				.SetEase(_animationParameters.AnimationYCurve);
			Sequence result = DOTween.Sequence();
			result.Append(animationByX);
			result.Append(animationByY);
			return result;
		}
	}
}
