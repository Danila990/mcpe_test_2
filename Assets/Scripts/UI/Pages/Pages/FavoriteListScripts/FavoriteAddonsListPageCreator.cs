using Assets.Scripts.UI.UIPages.Pages.AddonsListScripts.FavoriteList.UI;
using Scripts.UI.UIStates.UICore.ObjectPoolPattern;

namespace Assets.Scripts.UI.UIPages.Pages.AddonsListScripts.FavoriteList
{
	public class FavoriteAddonsListPageCreator : BaseCreator<FavoriteAddonsListPageView>
	{
		private void Awake()
		{
			FavoriteAddonsListPage.Creator = this;
		}
	}
}
