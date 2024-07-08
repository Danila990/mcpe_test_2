using Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Scrolls.InfinityScroll
{
	public class HorizontalInfinityScrollRect : BaseInfinityScrollRect
	{
		private HorizontalLayoutGroup _contentLayoutGroup;

		public HorizontalInfinityScrollRect(ScrollRect scrollRect) : base(scrollRect)
		{
			_contentLayoutGroup = scrollRect.content.GetComponent<HorizontalLayoutGroup>();
			scrollRect.content.pivot = new Vector2(0, 0.5f);
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
			if(lastChildCorners[2].x < scrollRectCorners[2].x || lastChildCorners[3].x < scrollRectCorners[3].x)
			{
				var firstChild = ScrollRect.content.GetChildRect(0);
				firstChild.transform.SetAsLastSibling();
				int firstChildDelta = (int)(_contentLayoutGroup.spacing + firstChild.GetSizeWithCurrentAnchors().x);
				_contentLayoutGroup.padding.left += firstChildDelta;
				return true;
			}

			return false;
		}

		private bool TryMoveLastElementToFirst()
		{
			var firstChild = ScrollRect.content.GetChildRect(0);
			var firstChildCorners = firstChild.GetWorldCorners();
			var scrollRectCorners = _scrollRectTransform.GetWorldCorners();
			if(firstChildCorners[0].x > scrollRectCorners[0].x || firstChildCorners[1].x > scrollRectCorners[1].x)
			{
				var lastChild = ScrollRect.content.GetChildRect(ScrollRect.content.childCount - 1);
				lastChild.transform.SetAsFirstSibling();
				int lastChildDelta = (int)(_contentLayoutGroup.spacing + lastChild.GetSizeWithCurrentAnchors().x);
				_contentLayoutGroup.padding.left -= lastChildDelta;
				return true;
			}

			return false;
		}
	}
}
