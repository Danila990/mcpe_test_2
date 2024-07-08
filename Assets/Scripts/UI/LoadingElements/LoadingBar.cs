using UnityEngine;

namespace Assets.Scripts.UI.LoadingElements
{
	public abstract class LoadingBar : MonoBehaviour
	{
		public abstract void UpdateProgress(float value);
	}
}
