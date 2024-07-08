using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Audio
{
	[RequireComponent(typeof(Toggle))]
	public class AudioToggleButton : MonoBehaviour
	{
		private Toggle _volumeToggle;

		private void Awake()
		{
			_volumeToggle = GetComponent<Toggle>();
		}

		private void Start()
		{
			OnVolumeChange(AudioMuteChanger.Instance.IsMute);
			AudioMuteChanger.Instance.OnVolumeChange += OnVolumeChange;
			_volumeToggle.onValueChanged.AddListener(ChangeVolume);
		}

		public void ChangeVolume(bool isMute)
		{
			AudioMuteChanger.Instance.IsMute = isMute;
		}

		private void OnVolumeChange(bool isMute)
		{
			_volumeToggle.isOn = isMute;
		}
	}
}
