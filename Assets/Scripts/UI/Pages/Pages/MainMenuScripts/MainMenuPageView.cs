using Assets.Scripts.UI.UIStates.UICore.ElementContainers;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.UIStates.MainMenuScripts
{
	public class MainMenuPageView : SimplePageView
	{
		[field: SerializeField]
		public Button TestButton 
		{
			get;
			private set;
		}
	}
}
