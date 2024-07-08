using Assets.Scripts.UI.UIStates.UICore.PageStacks;
using Scripts.UI.UIStates.PrivacyPolicyPageScripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.UIPages.PagesCommonButtons
{
	[RequireComponent(typeof(Button))]
	public class OpenPrivacyPolicyPageButton : MonoBehaviour
	{
		private void Awake()
		{
			var button = GetComponent<Button>();
			button.onClick.AddListener(ShowPrivacyPolicy);
		}

		private void ShowPrivacyPolicy()
		{
			SimplePageStack pageStack = SimplePageStack.PageStack;
			var privacyPolicy = new PrivacyPolicyPage(pageStack);
			pageStack.ShowLast(privacyPolicy);
		}
	}
}
