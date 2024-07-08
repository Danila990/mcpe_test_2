using Assets.Scripts.UI.UIStates.UICore.ElementContainers;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace Scripts.UI.UIStates.DialogueBoxScripts
{
	public class DialogueBoxView : OverlayPageView
	{
		[field: SerializeField]
		public LocalizeStringEvent Text
		{
			get;
			private set;
		}

		[field: SerializeField]
		public Button YesButton
		{
			get;
			private set;
		}

		[field: SerializeField]
		public Button NoButton
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
