using Assets.Scripts.UI.UIStates.UICore.PageStacks;
using UnityEngine;

namespace Scripts.UI.UIStates.UICore
{
	public class UIEscapeKeyHandler : MonoBehaviour
	{
		private void Update()
		{
			if(Input.GetKeyDown(KeyCode.Escape) && Time.timeScale != 0) 
			{
				SimplePageStack.PageStack.PressEscapeForLastPage();
			}
		}
	}
}
