using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.UIPages.Pages.FileLoaderPageScripts.UI.Elements
{
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class LoadingPercentsText : MonoBehaviour
	{
		private TextMeshProUGUI _text;

		private int _lastProgress = -1;

		private void Awake()
		{
			_text = GetComponent<TextMeshProUGUI>();
		}

		public void UpdateProgress(float value)
		{
			int progress = (int)(value * 100);
			if(progress == _lastProgress)
			{
				return;
			}

			_text.text = $"{progress} %";
			_lastProgress = progress;
		}
	}
}
