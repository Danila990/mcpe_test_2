using Assets.Scripts.UI.UIStates.AddonsListScripts.AddonsData;
using Scripts.ObjectPoolPattern;
using UnityEngine;

namespace Scripts.UI.UIStates.AddonsListScripts.AddonsData
{
	public class AddonsListCardCreator : MonoBehaviour, ICreation<AddonsListCardView>
	{
		[SerializeField] private AddonsListCardView cardPrefab;

		public AddonsListCardView Create()
		{
			return Instantiate(cardPrefab);
		}
	}
}
