using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Audio
{
	[RequireComponent(typeof(Image))]
	public class OnAudioMuteSpriteChanger : MonoBehaviour
	{
		[SerializeField] private Sprite volumeOn;
		[SerializeField] private Sprite volumeOff;

		private Image _image;

		private void Awake()
		{
			_image = GetComponent<Image>();
		}

		private void Start()
		{
			ChangeVolume(AudioMuteChanger.Instance.IsMute);
			AudioMuteChanger.Instance.OnVolumeChange += ChangeVolume;
		}

		private void ChangeVolume(bool isMute)
		{
			_image.sprite = isMute ? volumeOff : volumeOn;
		}
	}
}