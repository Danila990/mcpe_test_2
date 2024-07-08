using Assets.Scripts;
using Assets.Scripts.UI.UIPages.Pages.OnboardingPageScripts;
using Assets.Scripts.UI.UIStates.UICore.ElementContainers;
using Assets.Scripts.UI.UIStates.UICore.PageStacks;
using Scripts.ObjectPoolPattern;
using Scripts.UI.UIStates.UICore;
using UnityEngine;

namespace Scripts.UI.UIStates.MainMenuScripts
{
	public class MainMenuPage : UISimplePage
	{
		public static ICreation<MainMenuPageView> Creator;

		private readonly MainMenuPageView _view;

		public MainMenuPage(SimplePageStack mainPageStack) : base(mainPageStack)
		{
			_view = Creator.Create();
#if SERVER_ENABLED
			ShowOnboardingPage();
#endif
		}

		public override void Dispose()
		{
			base.Dispose();
			GameObject.Destroy(_view.gameObject);
		}

		protected override SimplePageView GetSimplePageView()
		{
			return _view;
		}

		private void ShowOnboardingPage()
		{
			if(OnboardingRequirementController.IsNecessary)
			{
				var onboardingPage = new OnboardingPage(this);
				LocalPageStack.ShowLast(onboardingPage);
			}
		}
	}
}
