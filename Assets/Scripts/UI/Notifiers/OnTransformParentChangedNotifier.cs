using System;
using UnityEngine;

namespace Assets.Scripts.UI.Notifiers
{
	public class OnTransformParentChangedNotifier : MonoBehaviour
	{
		public event Action OnParentChanged;

		private void OnTransformParentChanged()
		{
			OnParentChanged?.Invoke();
		}
	}
}
