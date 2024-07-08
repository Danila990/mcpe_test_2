using Scripts.UI.UIStates.UICore.ObjectPoolPattern;

namespace Scripts.UI.UIStates.AddonsListScripts
{
	public class AddonsListPageCreator : BaseCreator<AddonsListPageView>
	{
		private void Awake()
		{
			AddonsListPage.Creator = this;
		}
	}
}
