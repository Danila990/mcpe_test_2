using System;
using UnityEngine;

namespace Scripts.Audio
{
	public class AudioMuteChanger
	{
		private const string PlayerPrefsIsMuteKey = "IsMute";

		public static readonly AudioMuteChanger Instance = new AudioMuteChanger();

		private AudioMuteChanger()
		{
			IsMute = PlayerPrefs.HasKey(PlayerPrefsIsMuteKey) && PlayerPrefs.GetInt(PlayerPrefsIsMuteKey) == 1;
		}

		public event Action<bool> OnVolumeChange;

		public bool IsMute
		{
			get
			{
				return Mathf.Approximately(AudioListener.volume, 0);
			}

			set
			{
				AudioListener.volume = value ? 0 : 1;
				PlayerPrefs.SetInt(PlayerPrefsIsMuteKey, value ? 1 : 0);
				OnVolumeChange?.Invoke(value);
			}
		}
	}
}