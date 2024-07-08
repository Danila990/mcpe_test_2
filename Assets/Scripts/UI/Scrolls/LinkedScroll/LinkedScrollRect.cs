using Assets.Scripts.UI.Scrolls.LinkedScroll;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.UI.LinkedScroll
{
	public class LinkedScrollRect : ScrollRect
	{
		[SerializeField] private float minMagnitudeToDrag = 10;

		private ScrollRect _linkedScrollRect;
		private NowScrollRectDragging _nowDragging;

		protected override void Awake()
		{
			base.Awake();
			_linkedScrollRect = GetLinkedScrollRect();
		}

		public override void OnBeginDrag(PointerEventData eventData)
		{
			_nowDragging = NowScrollRectDragging.Undetected;
		}

		public override void OnDrag(PointerEventData eventData)
		{
			if(_nowDragging == NowScrollRectDragging.This)
			{
				base.OnDrag(eventData);
				return;
			}

			if(_nowDragging == NowScrollRectDragging.Linked)
			{
				_linkedScrollRect.OnDrag(eventData);
				return;
			}

			Vector2 delta = eventData.position - eventData.pressPosition;
			int axisIndex = horizontal ? 0 : 1;
			int linkedAxisIndex = (axisIndex + 1) % 2;
			if(Math.Abs(delta[axisIndex]) >= minMagnitudeToDrag)
			{
				_nowDragging = NowScrollRectDragging.This;
				base.OnBeginDrag(eventData);
			}
			else if(Math.Abs(delta[linkedAxisIndex]) >= minMagnitudeToDrag)
			{
				_nowDragging = NowScrollRectDragging.Linked;
				_linkedScrollRect.OnBeginDrag(eventData);
			}
		}

		public override void OnEndDrag(PointerEventData eventData)
		{
			if(_nowDragging == NowScrollRectDragging.Linked)
			{
				_linkedScrollRect.OnEndDrag(eventData);
			}
			else if(_nowDragging == NowScrollRectDragging.This)
			{
				base.OnEndDrag(eventData);
			}

			_nowDragging = NowScrollRectDragging.Undetected;
		}

		private ScrollRect GetLinkedScrollRect()
		{
			ScrollRect linkedScrollRect = null;
			Transform lastParent = transform;
			while(linkedScrollRect == null)
			{
				lastParent = lastParent.parent;
				linkedScrollRect = lastParent.GetComponent<ScrollRect>();
			}

			return linkedScrollRect;
		}
	}
}
