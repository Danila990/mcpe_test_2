using Assets.Scripts.UI.LoadingElements;
using Assets.Scripts.UI.UIPages.Pages.FileLoaderPageScripts.UI.Elements;
using Assets.Scripts.UI.UIStates.UICore.ElementContainers;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.UIStates.StartLoadingPageScripts
{
	public class StartLoadingPageView : SimplePageView
	{
		[field: SerializeField]
		public GameObject LoadingText
		{
			get;
			private set;
		}

		[field: SerializeField]
		public GameObject LoadedText
		{
			get;
			private set;
		}

		[field: SerializeField]
		public GameObject FailedText
		{
			get;
			private set;
		}

		[field: SerializeField]
		public Button RetryButton
		{
			get;
			private set;
		}

		[field: SerializeField]
		public LoadingPercentsText PercentsText
		{
			get;
			private set;
		}

		[field: SerializeField]
		public LoadingBar LoadingBar
		{
			get;
			private set;
		}

		[field: SerializeField]
		public Button LoadedButton
		{
			get;
			private set;
		}

		[field: SerializeField, Min(0)]
		public int ExtraDelay
		{
			get;
			private set;
		} = 5;
	}
}
