using Assets.Scripts.UI.UIStates.UICore.PageStacks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.UIPages.PagesCommonButtons
{
	[RequireComponent(typeof(Button))]
	public class EscapeButton : MonoBehaviour
	{
		private void Awake()
		{
			var button = GetComponent<Button>();
			button.onClick.AddListener(PressEscapeForLastPage);
		}

		private void PressEscapeForLastPage()
		{
			SimplePageStack.PageStack.PressEscapeForLastPage();
		}
	}
}
