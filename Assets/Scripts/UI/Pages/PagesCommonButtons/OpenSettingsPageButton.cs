using Assets.Scripts.UI.UIStates.SettingsPageScripts;
using Assets.Scripts.UI.UIStates.UICore.PageStacks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.UIPages.PagesCommonButtons
{
	[RequireComponent(typeof(Button))]
	public class OpenSettingsPageButton : MonoBehaviour
	{
		private void Awake()
		{
			var button = GetComponent<Button>();
			button.onClick.AddListener(ShowSettings);
		}

		private void ShowSettings()
		{
			SimplePageStack pageStack = SimplePageStack.PageStack;
			var settingsPage = new SettingsPage(pageStack);
			pageStack.ShowLast(settingsPage);
		}
	}
}
