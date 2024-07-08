using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.UIPages.Pages.RateAddonBoxScripts.UI.Elements
{
	[RequireComponent(typeof(Slider))]
	public class RateSlider : MonoBehaviour
	{
		private Slider _slider;

		public event Action<int> OnRateChanged;

		public int Rate
		{
			get;
			private set;
		}

		private void Awake()
		{
			_slider = GetComponent<Slider>();
			_slider.onValueChanged.AddListener(UpdateStarsCount);
		}

		public void SetRate(int rate)
		{
			_slider.value = (float)rate / 5;
		}

		private void UpdateStarsCount(float value)
		{
			var nowRate = Mathf.Max(Mathf.CeilToInt(value * 5), 1);
			if(Rate == nowRate)
			{
				return;
			}

			Rate = nowRate;
			OnRateChanged?.Invoke(Rate);
		}
	}
}
