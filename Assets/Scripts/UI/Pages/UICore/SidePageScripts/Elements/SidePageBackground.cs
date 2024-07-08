using UnityEngine;

namespace Assets.Scripts.UI.UIPages.UICore.SidePageScripts.Elements
{
	[RequireComponent(typeof(CanvasGroup))]
	public class SidePageBackground : MonoBehaviour
	{
		[SerializeField] private SidePageDraggableElement _sideDraggableElement;

		private CanvasGroup _canvasGroup;

		private void Awake()
		{
			_canvasGroup = GetComponent<CanvasGroup>();
			_sideDraggableElement.OnProgress += UpdateAlpha;
		}

		public void UpdateAlpha(float alpha)
		{
			_canvasGroup.alpha = alpha;
		}
	}
}
