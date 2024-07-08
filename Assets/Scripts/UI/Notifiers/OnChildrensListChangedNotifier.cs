using System;
using UnityEngine;

namespace Assets.Scripts.UI.Scrolls
{
	public class OnChildrensListChangedNotifier : MonoBehaviour
	{
		public event Action OnChildrensListChanged;

		private void OnTransformChildrenChanged()
		{
			OnChildrensListChanged?.Invoke();
		}
	}
}
