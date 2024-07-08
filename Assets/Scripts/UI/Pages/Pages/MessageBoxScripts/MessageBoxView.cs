using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Localization.Components;
using Assets.Scripts.UI.UIStates.UICore.ElementContainers;

namespace Scripts.UI.UIStates.MessageBoxScripts
{
	public class MessageBoxView : OverlayPageView
	{
		[field: SerializeField]
		public LocalizeStringEvent Text
		{
			get;
			private set;
		}

		[field: SerializeField]
		public ScrollRect TextScrollRect
		{
			get;
			private set;
		}

		[field: SerializeField]
		public Button OkButton
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
