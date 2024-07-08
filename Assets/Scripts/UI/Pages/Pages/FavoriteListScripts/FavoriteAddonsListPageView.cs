using Assets.Scripts.UI.UIStates.UICore.ElementContainers;
using Scripts.UI.UIStates.AddonsListScripts.ScrollRects;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.UIPages.Pages.AddonsListScripts.FavoriteList.UI
{
	public class FavoriteAddonsListPageView : SimplePageView
	{
		[field: SerializeField]
		public TextMeshProUGUI HasNoFavoritesText
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
