using Scripts.UI.UIStates.UICore.ObjectPoolPattern;

namespace Assets.Scripts.UI.UIStates.SideMenuScripts.UI
{
	public class SideMenuCreator : BaseCreator<SideMenuView>
	{
		private void Awake()
		{
			SideMenu.Creator = this;
		}
	}
}
