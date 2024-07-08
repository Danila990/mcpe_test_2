using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Scrolls.InfinityScroll
{
	public abstract class BaseInfinityScrollRect
	{
		public readonly ScrollRect ScrollRect;

		protected readonly RectTransform _scrollRectTransform;

		public BaseInfinityScrollRect(ScrollRect scrollRect)
		{
			ScrollRect = scrollRect;
			_scrollRectTransform = (RectTransform)ScrollRect.transform;
		}

		public abstract void OnScroll();
	}
}
