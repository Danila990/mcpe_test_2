using DG.Tweening;
using UnityEngine;

namespace Scripts.UI.LoadingElements
{
	public class StaticRoundingLoadingElement : RoundingLoadingElement
	{
		[Range(0, 1)]
		[SerializeField] private float maxFillAmount = 0.8f;

		protected override void Start()
		{
			base.Start();
			StartFillToMax();
		}

		private void StartFillToMin()
		{
			_fillAnimation.Kill();
			_fillAnimation = DOTween.Sequence();
			Tween tween = image.DOFillAmount(minFillAmount, fillSpeed).OnComplete(StartFillToMax).SetEase(Ease.Linear);
			_fillAnimation.Append(tween);
		}

		private void StartFillToMax()
		{
			_fillAnimation.Kill();
			_fillAnimation = DOTween.Sequence();
			Tween tween = image.DOFillAmount(maxFillAmount, fillSpeed).OnComplete(StartFillToMin).SetEase(Ease.Linear);
			_fillAnimation.Append(tween);
		}
	}
}
