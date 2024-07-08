//using Assets.Scripts.Extensions;
//using Scripts.UI.FixedScroll;
//using UnityEngine;

//namespace Assets.Scripts.UI.Scrolls.FixedScroll
//{
//	[RequireComponent(typeof(FixedScrollRect))]
//	public class FixedScrollRectProgressNotifier : MonoBehaviour
//	{
//		private FixedScrollRect _scrollRect;
//		private Vector2? _nowChildLerpTo = null;
//		private Vector2? _previousChildLerpTo = null;
//		private Vector2? _nextChildLerpTo = null;

//		private void Awake()
//		{
//			_scrollRect = GetComponent<FixedScrollRect>();
//			_scrollRect.OnNowChildChanged += UpdateLerpPositions;
//		}

//		private void Start()
//		{
//			var onChildrensListChangedNotifier =
//				_scrollRect.ScrollRect.content.gameObject.GetOrAddComponent<OnChildrensListChangedNotifier>();
//			onChildrensListChangedNotifier.OnChildrensListChanged += UpdateLerpPositions;
//			UpdateLerpPositions();
//		}

//		private void LateUpdate()
//		{
//			Vector2 contentPosition = _scrollRect.ScrollRect.content.localPosition;
//			if(!_nowChildLerpTo.HasValue) 
//			{
//				return;
//			}

//			var distanceToNowChild = _nowChildLerpTo.Value - contentPosition;
//		}

//		private void UpdateLerpPositions(int nowChild) 
//		{
//			_nowChildLerpTo = _scrollRect.GetContentPositionLerpTo(nowChild);
//			int previousChildIndex = nowChild - 1;
//			if(previousChildIndex >= 0) 
//			{
//				_previousChildLerpTo = _scrollRect.GetContentPositionLerpTo(previousChildIndex);
//			}
//			else 
//			{
//				_previousChildLerpTo = null;
//			}

//			int nextChildIndex = nowChild + 1;
//			if(nextChildIndex < _scrollRect.ScrollRect.content.childCount) 
//			{
//				_nextChildLerpTo = _scrollRect.GetContentPositionLerpTo(nextChildIndex);
//			}
//			else
//			{
//				_nextChildLerpTo = null;
//			}
//		}

//		private void UpdateLerpPositions() 
//		{
//			int childCount = _scrollRect.ScrollRect.content.childCount;
//			if(childCount == 0 || _scrollRect.NowChild >= childCount)
//			{
//				_nowChildLerpTo = null;
//				_previousChildLerpTo = null;
//				_nextChildLerpTo = null;
//				return;
//			}

//			UpdateLerpPositions(_scrollRect.NowChild);
//		}
//	}
//}
