using Scripts.UI.UIStates.UICore.ObjectPoolPattern;

namespace Assets.Scripts.UI.UIStates.CategoriesListScripts.UI
{
	public class CategoriesListPageCreator : BaseCreator<CategoriesListPageView>
	{
		private void Awake()
		{
			CategoriesListPage.Creator = this;
		}
	}
}
