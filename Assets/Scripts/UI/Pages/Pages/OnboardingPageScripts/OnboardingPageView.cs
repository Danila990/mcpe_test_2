using Assets.Scripts.UI.UIStates.UICore.ElementContainers;
using Scripts.UI.FixedScroll;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.UIPages.Pages.OnboardingPageScripts.UI
{
	public class OnboardingPageView : OverlayPageView
	{
		[field: SerializeField]
		public FixedScrollRect ScrollRect
		{
			get;
			private set;
		}

		[field: SerializeField]
		public Button PreviousButton
		{
			get;
			private set;
		}

		[field: SerializeField]
		public Button NextButton
		{
			get;
			private set;
		}
	}
}
