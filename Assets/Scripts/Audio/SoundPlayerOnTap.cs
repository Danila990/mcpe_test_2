using UnityEngine;

namespace Scripts.Audio
{
	[RequireComponent(typeof(AudioSource))]
	public class SoundPlayerOnTap : MonoBehaviour
	{
		[SerializeField] private AudioClip sound;

		private AudioSource _audioSource;

		private void Awake()
		{
			// Это костыль, потому что в синглтоне устанавливается текущий звук,
			// а сам синглтон только после загрузочного экрана инициализируется. Исправь, если знаешь как
			AudioMuteChanger.Instance.IsMute = AudioMuteChanger.Instance.IsMute;
			_audioSource = GetComponent<AudioSource>();
		}

		private void Update()
		{
			if(Input.GetMouseButtonDown(0))
			{
				_audioSource.PlayOneShot(sound);
			}
		}
	}
}
