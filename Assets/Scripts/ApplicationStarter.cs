using Assets.Scripts.UI.UIStates.UICore.PageStacks;
using Scripts.UI.UIStates.StartLoadingPageScripts;
using UnityEngine;

namespace Scripts
{
	public class ApplicationStarter : MonoBehaviour
	{
		private void Start()
		{
			var startScreen = new StartLoadingPage(SimplePageStack.PageStack);
			SimplePageStack.PageStack.ShowLast(startScreen);
			startScreen.Load();
		}
	}
}
