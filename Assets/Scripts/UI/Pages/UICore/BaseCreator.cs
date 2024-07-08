using Scripts.ObjectPoolPattern;
using UnityEngine;

namespace Scripts.UI.UIStates.UICore.ObjectPoolPattern
{
	public abstract class BaseCreator<T> : MonoBehaviour, ICreation<T> where T : BasePageView
	{
		[SerializeField] protected T pagePrefab;

		public virtual T Create()
		{
			return Instantiate(pagePrefab);
		}
	}
}
