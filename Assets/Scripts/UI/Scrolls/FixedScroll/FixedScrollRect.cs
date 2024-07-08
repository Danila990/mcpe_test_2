using Assets.Scripts.Extensions;
using Assets.Scripts.UI.Scrolls;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.UI.ScrollRect;

namespace Scripts.UI.FixedScroll
{
	// Content pivot должен быть с x или y равным 0.5 для vertical и horizontal соответственно
	// Content child pivot должен быть равным (0.5, 0.5)
	[RequireComponent(typeof(ScrollRectWithEvents))]
	public class FixedScrollRect : MonoBehaviour
	{
		[SerializeField] private float _decelerationRate = 10f;
		[SerializeField] private float _minLerpedMagnitude = 0.25f;
		[Range(0, 1)]
		[SerializeField] private float _newChildDistanceInPercents = 0.25f;

		private Vector2? _lerpTo;
		private bool _isDragging;
		private int _nowChild = 0;

		public event Action<int> OnNowChildChanged;

		public ScrollRectWithEvents ScrollRect
		{
			get;
			private set;
		}

		public int NowChild
		{
			get
			{
				return _nowChild;
			}

			private set
			{
				if(_nowChild == value)
				{
					return;
				}

				if((value >= ScrollRect.content.childCount && value != 0) || value < 0) 
				{
					throw new IndexOutOfRangeException();
				}
				
				_nowChild = value;
				OnNowChildChanged?.Invoke(value);
			}
		}

		public bool HasChilds => ScrollRect.content.childCount > 0;

		private void Awake()
		{
			ScrollRect = GetComponent<ScrollRectWithEvents>();
			ScrollRect.OnBeginDragEvent += OnBeginDrag;
			ScrollRect.OnDragEvent += UpdateNowChild;
			ScrollRect.OnEndDragEvent += OnEndDrag;
			var contentGameObject = ScrollRect.content.gameObject;
			var contentChildrensListChangedNotifier =
				contentGameObject.GetOrAddComponent<OnChildrensListChangedNotifier>();
			contentChildrensListChangedNotifier.OnChildrensListChanged += UpdatePositionOfLerpTo;
			var contentSizeChangedNotifier =
				contentGameObject.GetOrAddComponent<OnRectTransformSizeChangedNotifier>();
			contentSizeChangedNotifier.OnSizeChanged += UpdatePositionOfLerpTo;
#if UNITY_EDITOR
			contentChildrensListChangedNotifier.OnChildrensListChanged += CheckPivotInChilds;
			CheckPivotInContent();
			CheckPivotInChilds();
			CheckInertia();
			CheckMovementType();
#endif
		}

		private void LateUpdate()
		{
			if(!_lerpTo.HasValue || !HasChilds)
			{
				return;
			}

			float decelerate = Mathf.Min(_decelerationRate * Time.deltaTime, 1f);
			Vector2 newPosition = Vector2.Lerp(ScrollRect.content.localPosition, _lerpTo.Value, decelerate);
			if(Vector2.SqrMagnitude(newPosition - _lerpTo.Value) > _minLerpedMagnitude)
			{
				ScrollRect.content.localPosition = newPosition;
			}
			else
			{
				ScrollRect.content.localPosition = _lerpTo.Value;
				_lerpTo = null;
			}
		}

		public void ToBaseState()
		{
			NowChild = 0;
			_lerpTo = null;
		}

		public void SetChild(int index)
		{
			NowChild = index;
			ScrollRect.content.localPosition = GetContentPositionOfLerpTo(NowChild);
		}

		public void SetChildWithLerp(int index)
		{
			NowChild = index;
			_lerpTo = GetContentPositionOfLerpTo(NowChild);
		}

		public void SetPreviousChildWithLerp()
		{
			var newChild = NowChild - 1;
			if(newChild < 0) 
			{
				return;
			}

			SetChildWithLerp(newChild);
		}

		public void SetNextChildWithLerp()
		{
			var newChild = NowChild + 1;
			if(newChild >= ScrollRect.content.childCount)
			{
				return;
			}

			SetChildWithLerp(newChild);
		}

		public void UpdatePositionOfLerpTo()
		{
			if(_isDragging)
			{
				return;
			}

			if(NowChild >= ScrollRect.content.childCount) 
			{
				NowChild = ScrollRect.content.childCount - 1;
			}

			LayoutRebuilder.ForceRebuildLayoutImmediate(ScrollRect.content);
			_lerpTo = GetContentPositionOfLerpTo(NowChild);
		}

		public Vector2 GetContentPositionOfLerpTo(int childIndex)
		{
			RectTransform child = ScrollRect.content.GetChildRect(childIndex);
			Vector2 lerpTo = -ScrollRect.content.InverseTransformPoint(child.position);
			return lerpTo;
		}

		private void OnBeginDrag(PointerEventData eventData)
		{
			_lerpTo = null;
			_isDragging = true;
		}

		private void UpdateNowChild(PointerEventData eventData)
		{
			NowChild = GetClosestIndexToNowChild();
		}

		private int GetClosestIndexToNowChild()
		{
			Vector2 nowChildContentPosition = GetContentPositionOfLerpTo(NowChild);
			Vector2 nowChildDelta = (Vector2)ScrollRect.content.localPosition - nowChildContentPosition;
			float minDistance = nowChildDelta.magnitude * _newChildDistanceInPercents;
			int leftChildIndex = NowChild - 1;
			if(leftChildIndex >= 0)
			{
				Vector2 leftChildPosition = GetContentPositionOfLerpTo(leftChildIndex);
				Vector2 leftChildDelta = (Vector2)ScrollRect.content.localPosition - leftChildPosition;
				if(leftChildDelta.magnitude < minDistance)
				{
					return leftChildIndex;
				}
			}

			int rightChildIndex = NowChild + 1;
			if(rightChildIndex < ScrollRect.content.childCount)
			{
				Vector2 rightChildPosition = GetContentPositionOfLerpTo(rightChildIndex);
				Vector2 rightChildDelta = (Vector2)ScrollRect.content.localPosition - rightChildPosition;
				if(rightChildDelta.magnitude < minDistance)
				{
					return rightChildIndex;
				}
			}

			return NowChild;
		}

		private void OnEndDrag(PointerEventData eventData)
		{
			_isDragging = false;
			if(ScrollRect.content.childCount == 0)
			{
				return;
			}

			int moveDirection = GetMoveDirection(eventData);
			int newIndex;
			if(moveDirection == -1)
			{
				int previousIndex = NowChild - 1;
				newIndex = previousIndex < 0 ? NowChild : previousIndex;
			}
			else if(moveDirection == 1)
			{
				int nextIndex = NowChild + 1;
				newIndex = nextIndex >= ScrollRect.content.childCount ? NowChild : nextIndex;
			}
			else
			{
				newIndex = NowChild;
			}

			SetChildWithLerp(newIndex);
		}

		private int GetMoveDirection(PointerEventData eventData)
		{
			Vector3 dragVectorDirection = (eventData.position - eventData.pressPosition).normalized;
			if(ScrollRect.horizontal && dragVectorDirection.x > 0 ||
				ScrollRect.vertical && dragVectorDirection.y < 0)
			{
				return -1;
			}
			else if(ScrollRect.horizontal && dragVectorDirection.x < 0 ||
				ScrollRect.vertical && dragVectorDirection.y > 0)
			{
				return 1;
			}

			return 0;
		}

#if UNITY_EDITOR
		private void CheckMovementType()
		{
			if(ScrollRect.movementType != MovementType.Unrestricted)
			{
				Debug.LogError("MovementType shoud be unrestricted");
			}
		}

		private void CheckInertia()
		{
			if(ScrollRect.inertia) 
			{
				Debug.LogError("Inertia shoud be false");
			}
		}

		private void CheckPivotInContent()
		{
			Vector2 pivot = ScrollRect.content.pivot;
			if(!(ScrollRect.horizontal && Mathf.Approximately(pivot.y, 0.5f) || 
				ScrollRect.vertical && Mathf.Approximately(pivot.x, 0.5f)))
			{
				Debug.LogError("Content pivot must be equal to 0.5 by choosen axis");
			}
		}

		private void CheckPivotInChilds()
		{
			Vector2 correctChildPivot = new Vector2(0.5f, 0.5f);
			int childsCount = ScrollRect.content.childCount;
			for(int i = 0; i < childsCount; i++)
			{
				RectTransform child = ScrollRect.content.GetChildRect(i);
				Vector2 childPivot = child.pivot;
				if(!Mathf.Approximately(childPivot.y, correctChildPivot.y) ||
					!Mathf.Approximately(childPivot.x, correctChildPivot.x))
				{
					Debug.LogError($"Child pivot must be equal to 0.5. Name: {child.gameObject.name}, child index: {i}");
				}
			}
		}
#endif
	}
}