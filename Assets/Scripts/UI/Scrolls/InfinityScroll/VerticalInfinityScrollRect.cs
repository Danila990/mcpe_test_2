using Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Scrolls.InfinityScroll
{
	public class VerticalInfinityScrollRect : BaseInfinityScrollRect
	{
		private VerticalLayoutGroup _contentLayoutGroup;

		public VerticalInfinityScrollRect(ScrollRect scrollRect) : base(scrollRect)
		{
			_contentLayoutGroup = scrollRect.content.GetComponent<VerticalLayoutGroup>();
			scrollRect.content.pivot = new Vector2(0.5f, 1);
			scrollRect.onValueChanged.AddListener((pos) => OnScroll());
		}

		public override void OnScroll()
		{
			if(TryMoveLastElementToFirst())
			{
				return;
			}

			if(TryMoveFirstElementToLast())
			{
				return;
			}
		}

		private bool TryMoveFirstElementToLast()
		{
			var lastChild = ScrollRect.content.GetChildRect(ScrollRect.content.childCount - 1);
			var lastChildCorners = lastChild.GetWorldCorners();
			var scrollRectCorners = _scrollRectTransform.GetWorldCorners();
			if(lastChildCorners[0].y > scrollRectCorners[0].y || lastChildCorners[3].y > scrollRectCorners[3].y)
			{
				var firstChild = ScrollRect.content.GetChildRect(0);
				firstChild.transform.SetAsLastSibling();
				int firstChildDelta = (int)(_contentLayoutGroup.spacing + firstChild.GetSizeWithCurrentAnchors().y);
				_contentLayoutGroup.padding.top += firstChildDelta;
				return true;
			}

			return false;
		}

		private bool TryMoveLastElementToFirst()
		{
			var firstChild = ScrollRect.content.GetChildRect(0);
			var firstChildCorners = firstChild.GetWorldCorners();
			var scrollRectCorners = _scrollRectTransform.GetWorldCorners();
			if(firstChildCorners[1].y < scrollRectCorners[1].y || firstChildCorners[2].y < scrollRectCorners[2].y)
			{
				var lastChild = ScrollRect.content.GetChildRect(ScrollRect.content.childCount - 1);
				lastChild.transform.SetAsFirstSibling();
				int lastChildDelta = (int)(_contentLayoutGroup.spacing + lastChild.GetSizeWithCurrentAnchors().y);
				_contentLayoutGroup.padding.top -= lastChildDelta;
				return true;
			}

			return false;
		}
	}
}
