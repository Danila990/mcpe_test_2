using Assets.Scripts.UI.UIStates.UICore.ElementContainers;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.UIStates.PrivacyPolicyPageScripts
{
	public class PrivacyPolicyPageView : SimplePageView
	{
		[field: SerializeField]
		public Button ApplyButton
		{
			get;
			private set;
		}

		[field: SerializeField]
		public ScrollRect TextScrollRect
		{
			get;
			private set;
		}
	}
}
