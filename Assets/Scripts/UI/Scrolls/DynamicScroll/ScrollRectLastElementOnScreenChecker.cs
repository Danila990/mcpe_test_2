using Scripts;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Scrolls.DynamicScroll.New
{
	[RequireComponent(typeof(ScrollRect))]
	public class ScrollRectLastElementOnScreenChecker : MonoBehaviour
	{
		private BaseChecker _checker;

		public event Action LastElementOnScreenCallback;

		private void Start()
		{
			ScrollRect scrollRect = GetComponent<ScrollRect>();
			if(scrollRect.horizontal)
			{
				_checker = new HorizontalChecker(scrollRect);
			}
			else if(scrollRect.vertical)
			{
				_checker = new VerticalChecker(scrollRect);
			}
		}

		private void LateUpdate()
		{
			int childCount = _checker.ScrollRect.content.childCount;
			if(childCount == 0)
			{
				LastElementOnScreenCallback?.Invoke();
				return;
			}

			Vector3[] scrollWorldCorners = _checker.Transform.GetWorldCorners();
			Vector3[] lastChildWorldCorners = _checker.ScrollRect.content.GetChildRect(childCount - 1).GetWorldCorners();
			if(_checker.NeedAddLast(scrollWorldCorners, lastChildWorldCorners))
			{
				LastElementOnScreenCallback?.Invoke();
			}
		}
	}
}
