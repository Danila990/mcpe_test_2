using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
	[RequireComponent(typeof(Image))]
	public class ImageSpriteController : MonoBehaviour
	{
		private Image _image;

		public event Action<Sprite> SpriteChanged;

		public Sprite Sprite
		{
			get
			{
				return Image.sprite;
			}

			set
			{
				Image.sprite = value;
				SpriteChanged?.Invoke(value);
			}
		}

		private Image Image
		{
			get
			{
				if(_image == null)
				{
					_image = GetComponent<Image>();
				}

				return _image;
			}
		}
	}
}
