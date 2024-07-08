using Assets.Scripts.UI.UIPages.Pages.RateAddonBoxScripts.UI.Elements;
using Assets.Scripts.UI.UIStates.UICore.ElementContainers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.UIPages.Pages.RateAddonBoxScripts.UI
{
	public class RateAddonBoxView : OverlayPageView
	{
		[field: SerializeField]
		public Button CrossButton
		{
			get;
			private set;
		}

		[field: SerializeField]
		public RateSlider RateScroll
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
