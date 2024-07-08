using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
	[RequireComponent(typeof(Image), typeof(ImageSpriteController), typeof(AspectRatioFitter))]
	public class ImageARFRatioChanger : MonoBehaviour
	{
		private AspectRatioFitter _aspectRatioFitter;

		private void Awake()
		{
			_aspectRatioFitter = GetComponent<AspectRatioFitter>();
			var image = GetComponent<Image>();
			var spriteController = GetComponent<ImageSpriteController>();
			OnSpriteChanged(image.sprite);
			spriteController.SpriteChanged += OnSpriteChanged;
		}

		private void OnSpriteChanged(Sprite sprite)
		{
			if(sprite == null)
			{
				_aspectRatioFitter.aspectRatio = 2;
			}
			else
			{
				_aspectRatioFitter.aspectRatio = sprite.rect.width / sprite.rect.height;
			}
		}
	}
}
