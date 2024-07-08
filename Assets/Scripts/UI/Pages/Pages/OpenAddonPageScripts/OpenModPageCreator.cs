using Scripts.UI.UIStates.UICore.ObjectPoolPattern;

namespace Scripts.UI.UIStates.OpenAddonPageScripts
{
	public class OpenModPageCreator : BaseCreator<OpenModPageView>
	{
		private void Awake()
		{
			OpenModPage.Creator = this;
		}
	}
}
