using UnityEngine;
using Assets.Scripts.UI.UIStates.CategoriesListScripts.UI.ScrollRects;
using Assets.Scripts.UI.UIStates.UICore.ElementContainers;
using Scripts.UI.UIStates.AddonsListScripts.ScrollRects;

namespace Assets.Scripts.UI.UIStates.CategoriesListScripts.UI
{
	public class CategoriesListPageView : SimplePageView
	{
		[field: SerializeField]
		public CategoriesListScrollRect CategoriesScrollRect
		{
			get;
			private set;
		}

		[field: SerializeField]
		public AddonsListScrollRect AddonsScrollRect
		{
			get;
			private set;
		}
	}
}
