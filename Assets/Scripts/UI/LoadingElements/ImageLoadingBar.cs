using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.LoadingElements
{
	public class ImageLoadingBar : LoadingBar
	{
		[SerializeField] private Image _image;

		public override void UpdateProgress(float value)
		{
			_image.fillAmount = value;
		}
	}
}
