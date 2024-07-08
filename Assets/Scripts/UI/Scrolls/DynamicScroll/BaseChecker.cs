using UnityEngine.UI;
using UnityEngine;

namespace Assets.Scripts.UI.Scrolls.DynamicScroll.New
{
	public abstract class BaseChecker
	{
		public readonly ScrollRect ScrollRect;
		public readonly RectTransform Transform;

		public BaseChecker(ScrollRect scrollRect)
		{
			ScrollRect = scrollRect;
			Transform = (RectTransform)scrollRect.transform;
		}

		public abstract bool NeedAddLast(Vector3[] scrollWorldCorners, Vector3[] lastChildWorldCorners);
	}
}
