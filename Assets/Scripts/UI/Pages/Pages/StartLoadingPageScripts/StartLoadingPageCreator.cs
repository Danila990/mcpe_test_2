using Scripts.UI.UIStates.UICore.ObjectPoolPattern;

namespace Scripts.UI.UIStates.StartLoadingPageScripts
{
	public class StartLoadingPageCreator : BaseCreator<StartLoadingPageView>
	{
		private void Awake()
		{
			StartLoadingPage.Creator = this;
		}
	}
}
