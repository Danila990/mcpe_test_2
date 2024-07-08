using Assets.Scripts.UI.UIPages.Pages.ReportBoxScripts;
using Assets.Scripts.UI.UIStates.UICore.PageStacks;
using Scripts.UI.UIStates.UICore;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.UIPages.PagesCommonButtons
{
	[RequireComponent(typeof(Button))]
	public class OpenReportBoxButton : MonoBehaviour
	{
		private void Awake()
		{
			var button = GetComponent<Button>();
			button.onClick.AddListener(ShowReportBox);
		}

		private void ShowReportBox()
		{
			SimplePageStack pageStack = SimplePageStack.PageStack;
			UISimplePage parentPage = pageStack.Last();
			var reportBox = new ReportBox(parentPage);
			parentPage.LocalPageStack.ShowLast(reportBox);
		}
	}
}
