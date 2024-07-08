using Scripts.UI.UIStates.UICore.ObjectPoolPattern;

namespace Scripts.UI.UIStates.MainMenuScripts
{
	public class MainMenuPageCreator : BaseCreator<MainMenuPageView>
	{
		private void Awake()
		{
			MainMenuPage.Creator = this;
		}
	}
}
