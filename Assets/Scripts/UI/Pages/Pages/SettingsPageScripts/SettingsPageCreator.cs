using Assets.Scripts.UI.UIStates.SettingsPageScripts;
using Scripts.UI.UIStates.UICore.ObjectPoolPattern;

namespace Scripts.UI.UIStates.SettingsPageScripts
{
	public class SettingsPageCreator : BaseCreator<SettingsPageView>
	{
		private void Awake()
		{
			SettingsPage.Creator = this;
		}
	}
}