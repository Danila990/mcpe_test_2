using Assets.Scripts.UI.UIStates.UICore.PageStacks;
using UnityEngine;
using UnityEngine.UI;
#if LOCAL_ENABLED
using Scripts.UI.UIStates.AddonsListScripts;
#elif SERVER_ENABLED
using Assets.Scripts.UI.UIStates.CategoriesListScripts;
#endif

namespace Assets.Scripts.UI.UIPages.PagesCommonButtons
{
	[RequireComponent(typeof(Button))]
	public class OpenAddonsListPageButton : MonoBehaviour
	{
		private void Awake()
		{
			var button = GetComponent<Button>();
			button.onClick.AddListener(ShowAddonsList);
		}

		private void ShowAddonsList()
		{
			SimplePageStack pageStack = SimplePageStack.PageStack;
#if LOCAL_ENABLED
			var addonsListPage = new AddonsListPage(null, pageStack);
			pageStack.ShowLast(addonsListPage);
#elif SERVER_ENABLED
			var categoriesListPage = new CategoriesListPage(null, pageStack);
			pageStack.ShowLast(categoriesListPage);
#else
			throw new ArgumentException("Unknown format");
#endif
		}
	}
}
