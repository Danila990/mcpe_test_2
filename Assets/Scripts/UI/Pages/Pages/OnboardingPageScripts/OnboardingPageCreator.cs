using Scripts.UI.UIStates.UICore.ObjectPoolPattern;

namespace Assets.Scripts.UI.UIPages.Pages.OnboardingPageScripts.UI
{
	public class OnboardingPageCreator : BaseCreator<OnboardingPageView>
	{
		private void Awake()
		{
			OnboardingPage.Creator = this;
		}
	}
}
