using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.LoadingElements
{
	public class RoundingLoadingElement : MonoBehaviour
	{
		[SerializeField] protected Image image;

		[Tooltip("Скорость в градусах в секунду")]
		[SerializeField] protected float roundingSpeed = 60;
		[SerializeField] protected float fillSpeed = 0.5f;
		[Range(0, 1)]
		[SerializeField] protected float minFillAmount = 0.05f;

		protected RectTransform _imageTransform;
		protected Sequence _fillAnimation;

		private void Awake()
		{
			_imageTransform = image.GetComponent<RectTransform>();
			_fillAnimation = DOTween.Sequence();
		}

		protected virtual void Start()
		{
			ToBaseState();
		}

		private void Update()
		{
			_imageTransform.Rotate(Vector3.forward, roundingSpeed / 60);
		}

		public void UpdateProgress(float value)
		{
			value = value > minFillAmount ? value : minFillAmount;

			_fillAnimation.Kill();
			_fillAnimation = DOTween.Sequence();
			_fillAnimation.Append(image.DOFillAmount(value, fillSpeed).SetEase(Ease.Linear));
		}

		public void ToBaseState()
		{
			image.fillAmount = minFillAmount;
		}

		private void OnDestroy()
		{
			_fillAnimation.Kill();
		}
	}
}
