using Scripts.ObjectPoolPattern;
using UnityEngine;

namespace Scripts.UI.UIStates.CanvasPoolScripts
{
	[RequireComponent(typeof(Canvas))]
	public class CanvasGameObject : MonoBehaviour
	{
		public IPool<CanvasGameObject> Pool;

		public Canvas Canvas
		{
			get;
			private set;
		}

		private void Awake()
		{
			Canvas = GetComponent<Canvas>();
		}
	}
}
