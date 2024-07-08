using Scripts.ObjectPoolPattern;
using UnityEngine;

namespace Assets.Scripts.UI.UIStates.CategoriesListScripts.Card.UI
{
	public class CategoriesListCardCreator : MonoBehaviour, ICreation<CategoriesListCardView>
	{
		[SerializeField] private CategoriesListCardView cardPrefab;

		public CategoriesListCardView Create()
		{
			return Instantiate(cardPrefab);
		}
	}
}
