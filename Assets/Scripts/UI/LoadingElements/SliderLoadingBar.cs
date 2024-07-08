using Assets.Scripts.UI.LoadingElements;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.UIStates.StartLoadingPageScripts
{
	public class SliderLoadingBar : LoadingBar
	{
		[SerializeField] private Slider _loadingBar;

		public override void UpdateProgress(float value)
		{
			_loadingBar.value = value;
		}
	}
}
