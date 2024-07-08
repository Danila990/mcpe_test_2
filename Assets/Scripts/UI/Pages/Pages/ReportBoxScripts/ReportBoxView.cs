using Assets.Scripts.UI.UIStates.UICore.ElementContainers;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.Localization;

namespace Assets.Scripts.UI.UIPages.Pages.ReportBoxScripts.UI
{
	public class ReportBoxView : OverlayPageView
	{
		[field: SerializeField]
		public TMP_InputField InputField
		{
			get;
			private set;
		}

		[field: SerializeField]
		public Button SendButton
		{
			get;
			private set;
		}

		[field: SerializeField]
		public LocalizedString MessageSent
		{
			get;
			private set;
		}

		[field: SerializeField]
		public Button BackgroundButton
		{
			get;
			private set;
		}
	}
}
