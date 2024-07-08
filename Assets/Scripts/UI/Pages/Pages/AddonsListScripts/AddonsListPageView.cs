using UnityEngine;
using Scripts.UI.UIStates.AddonsListScripts.ScrollRects;
using Assets.Scripts.UI.UIStates.UICore.ElementContainers;

namespace Scripts.UI.UIStates.AddonsListScripts
{
	public class AddonsListPageView : SimplePageView
	{
		[field: SerializeField]
		public AddonsListScrollRect AddonsScrollRect
		{
			get;
			private set;
		}
	}
}
