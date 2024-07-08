using Scripts.ObjectPoolPattern;
using UnityEngine;

namespace Scripts.UI.UIStates.CanvasPoolScripts
{
	public class CanvasCreator : MonoBehaviour, ICreation<CanvasGameObject>
	{
		[SerializeField] private CanvasGameObject canvasPrefab;
		[SerializeField] private Camera cameraForCanvas;

		public IPool<CanvasGameObject> Pool
		{
			get;
			set;
		}

		private void Awake()
		{
			Pool = GetComponent<IPool<CanvasGameObject>>();
		}

		public CanvasGameObject Create()
		{
			var canvas = Instantiate(canvasPrefab);
			canvas.Pool = Pool;
			canvas.Canvas.worldCamera = cameraForCanvas;
			return canvas;
		}
	}
}
