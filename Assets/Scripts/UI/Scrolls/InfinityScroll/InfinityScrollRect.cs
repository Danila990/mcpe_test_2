using Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Scrolls.InfinityScroll
{
	[RequireComponent(typeof(ScrollRectWithEvents))]
	public class InfinityScrollRect : MonoBehaviour
	{
		public bool InititalizeOnStart;

		protected float _scrollbarBaseValue;
		protected Vector2 _childsPivot = Vector2.one / 2;
		protected ScrollRect _scrollRect;

		private BaseInfinityScrollRect _infinityScrollRect; 

		private void Awake()
		{
			_scrollRect = GetComponent<ScrollRectWithEvents>();
			if(_scrollRect.horizontal)
			{
				_scrollbarBaseValue = 0;
				_infinityScrollRect = new HorizontalInfinityScrollRect(_scrollRect);
			}
			else if(_scrollRect.vertical)
			{
				_scrollbarBaseValue = 1;
				_infinityScrollRect = new VerticalInfinityScrollRect(_scrollRect);
			}
		}

		private void Start()
		{
			if(InititalizeOnStart)
			{
				var items = _scrollRect.content.GetChildsRect();
				AddItems(items);
			}
		}

		/// <returns>Old items</returns>
		public void AddItems(RectTransform[] newItems)
		{
			foreach(RectTransform newItem in newItems)
			{
				newItem.pivot = _childsPivot;
				newItem.SetParent(_infinityScrollRect.ScrollRect.content);
			}
		}
	}
}