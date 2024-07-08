using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.UIPages.Pages.RateAddonBoxScripts.UI.Elements
{
	[RequireComponent(typeof(Image))]
	public class StarsFiller : MonoBehaviour
	{
		[SerializeField] private Slider _rateScroll;

		private Image _image;

		private void Awake()
		{
			_image = GetComponent<Image>();
			_image.fillAmount = 0;
			_rateScroll.onValueChanged.AddListener(UpdateStarsCount);
		}

		private void UpdateStarsCount(float value)
		{
			float starsCount = Mathf.Max(Mathf.CeilToInt(_rateScroll.value * 5), 1);
			_image.fillAmount = starsCount / 5;
		}
	}
}
