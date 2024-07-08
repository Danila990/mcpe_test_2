using Assets.Scripts.UI.LoadingElements;
using Assets.Scripts.UI.UIPages.Pages.FileLoaderPageScripts.UI.Elements;
using Assets.Scripts.UI.UIPages.Pages.OpenAddonPageScripts.UI;
using Assets.Scripts.UI.UIStates.UICore.ElementContainers;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace Scripts.UI.UIStates.OpenAddonPageScripts
{
	public class OpenModPageView : SimplePageView
	{
		[field: SerializeField]
		public LoadingBar LoadingBar
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
		public Button ReloadButton
		{
			get;
			private set;
		}

		[field: SerializeField]
		public ButtonWithDisabler OpenAddonButton
		{
			get;
			private set;
		}

		[field: SerializeField]
		public GameObject WaitText
		{
			get;
			private set;
		}

		[field: SerializeField]
		public GameObject LoadingSuccessText
		{
			get;
			private set;
		}

		[field: SerializeField]
		public GameObject LoadingUnsuccessText
		{
			get;
			private set;
		}

		[field: SerializeField]
		public LocalizedString AddonInstallationIncompleteText
		{
			get;
			private set;
		}

		[field: SerializeField]
		public NativeMessageBoxLocalizedMessages MessageBoxLocalizedMessages
		{
			get;
			private set;
		}
	}
}