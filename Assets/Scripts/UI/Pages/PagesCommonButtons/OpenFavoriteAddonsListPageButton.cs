using Assets.Scripts.UI.UIPages.Pages.AddonsListScripts.FavoriteList;
using Assets.Scripts.UI.UIStates.UICore.PageStacks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.UIPages.PagesCommonButtons
{
	[RequireComponent(typeof(Button))]
	public class OpenFavoriteAddonsListPageButton : MonoBehaviour
	{
		private void Awake()
		{
			var button = GetComponent<Button>();
			button.onClick.AddListener(ShowFavoriteAddonsList);
		}

		private void ShowFavoriteAddonsList()
		{
			SimplePageStack pageStack = SimplePageStack.PageStack;
			var favoriteAddonsListPage = new FavoriteAddonsListPage(pageStack);
			pageStack.ShowLast(favoriteAddonsListPage);
		}
	}
}
