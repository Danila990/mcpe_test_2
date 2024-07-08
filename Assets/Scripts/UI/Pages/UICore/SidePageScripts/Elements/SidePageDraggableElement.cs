using UnityEngine.EventSystems;
using UnityEngine;
using System;
using Scripts;

namespace Assets.Scripts.UI.UIPages.UICore.SidePageScripts.Elements
{
	public class SidePageDraggableElement : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		[SerializeField] private Side _side;

		private RectTransform _transform;
		private Vector2 _beginDragPosition;

		public event Action<float> OnProgress;
		public event Action OnEndShowSwipe;
		public event Action OnEndHideSwipe;

		public Vector2 ShowPositionBorder
		{
			get
			{
				Vector2 parentSize = ((RectTransform)_transform.parent).GetSizeWithCurrentAnchors();
				Vector2 elementSize = _transform.GetSizeWithCurrentAnchors();
				Vector2 result = -parentSize / 2 + elementSize / 2;
				result.y = 0;
				return result;
			}
		}

		public Vector2 HidePositionBorder
		{
			get
			{
				Vector2 parentSize = ((RectTransform)_transform.parent).GetSizeWithCurrentAnchors();
				Vector2 elementSize = _transform.GetSizeWithCurrentAnchors();
				Vector2 result = -parentSize / 2 - elementSize / 2;
				result.y = 0;
				return result;
			}
		}

		private void Awake()
		{
			_transform = (RectTransform)transform;
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			_beginDragPosition = _transform.localPosition;
		}

		public void OnDrag(PointerEventData eventData)
		{
			Vector2 ponterPressPosition = _transform.InverseTransformPoint(eventData.pointerPressRaycast.worldPosition);
			Vector2 ponterCurrentPosition = _transform.InverseTransformPoint(eventData.pointerCurrentRaycast.worldPosition);
			Vector2 newPosition = _beginDragPosition + ponterCurrentPosition - ponterPressPosition;
			_transform.localPosition = GetNewPositionWithBorders(newPosition);
			ReportProgress();
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			if(IsHideSwipe(eventData.pressPosition, eventData.position))
			{
				OnEndHideSwipe?.Invoke();
			}
			else
			{
				OnEndShowSwipe?.Invoke();
			}
		}

		public void ReportProgress()
		{
			float fullPathLength = (ShowPositionBorder - HidePositionBorder).magnitude;
			float nowPathLength = ((Vector2)transform.localPosition - HidePositionBorder).magnitude;
			float progress = nowPathLength / fullPathLength;
			OnProgress?.Invoke(progress);
		}

		private Vector2 GetNewPositionWithBorders(Vector2 newPosition)
		{
			if(_side == Side.Left && newPosition.x > ShowPositionBorder.x ||
				_side == Side.Right && newPosition.x < ShowPositionBorder.x ||
				_side == Side.Top && newPosition.y > ShowPositionBorder.y ||
				_side == Side.Bottom && newPosition.y < ShowPositionBorder.y)
			{
				return ShowPositionBorder;
			}
			else if(_side == Side.Left && newPosition.x < HidePositionBorder.x ||
				_side == Side.Right && newPosition.x > HidePositionBorder.x ||
				_side == Side.Top && newPosition.y < HidePositionBorder.y ||
				_side == Side.Bottom && newPosition.y > HidePositionBorder.y)
			{
				return HidePositionBorder;
			}

			if(_side == Side.Left || _side == Side.Right)
			{
				newPosition.y = 0;
			}

			if(_side == Side.Top || _side == Side.Bottom)
			{
				newPosition.x = 0;
			}

			return newPosition;
		}

		private bool IsHideSwipe(Vector2 pressPosition, Vector2 nowPosition)
		{
			return _side == Side.Left && pressPosition.x > nowPosition.x ||
				_side == Side.Right && pressPosition.x < nowPosition.x ||
				_side == Side.Top && pressPosition.y > nowPosition.y ||
				_side == Side.Bottom && pressPosition.y < nowPosition.y;
		}
	}
}
