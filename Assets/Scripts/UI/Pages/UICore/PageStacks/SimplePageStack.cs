using Scripts.AD;
using Scripts.UI.UIStates.UICore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.UI.UIStates.UICore.PageStacks
{
	public class SimplePageStack : IEnumerable<UISimplePage>
	{
		public static readonly SimplePageStack PageStack = new SimplePageStack();

		protected readonly LinkedList<UISimplePage> _pageStack = new LinkedList<UISimplePage>();

		public void ShowLast(UISimplePage pageToShow)
		{
			UISimplePage previousPage = _pageStack.LastOrDefault();
			previousPage?.MoveOut();
			_pageStack.AddLast(pageToShow);
			pageToShow.Show();
		}

		public void HideLast()
		{
			UISimplePage pageToHide = _pageStack.Last();
			var needShowInter = pageToHide.IsShowInterAdAfterHide;
			_pageStack.RemoveLast();
			pageToHide.Hide();
			UISimplePage previousPage = _pageStack.LastOrDefault();
			previousPage?.MoveIn();
			if(needShowInter)
			{
				AdsController.Instance.ShowInterAd();
			}
		}

		public void HideToPage<T>() where T : UISimplePage
		{
			UISimplePage pageToHide = _pageStack.Last();
			var needShowInter = pageToHide.IsShowInterAdAfterHide;
			_pageStack.RemoveLast();
			pageToHide.Hide();
			var countToRemove = GetPageCountToTypeFromLast<T>();
			RemovePagesFromLast(countToRemove);
			UISimplePage previousPage = _pageStack.LastOrDefault();
			previousPage?.MoveIn();
			if(needShowInter)
			{
				AdsController.Instance.ShowInterAd();
			}
		}

		private int GetPageCountToTypeFromLast<T>() where T : UISimplePage
		{
			int countFromEnd = 0;
			foreach(var page in _pageStack.Reverse())
			{
				if(page is T)
				{
					break;
				}

				countFromEnd++;
			}

			return countFromEnd;
		}

		private void RemovePagesFromLast(int countToDispose) 
		{
			for(int i = 0; i < countToDispose; i++)
			{
				var last = _pageStack.Last.Value;
				_pageStack.RemoveLast();
				last.Dispose();
			}
		}

		public void PressEscapeForLastPage() 
		{
			if(_pageStack.Count() < 1)
			{
				return;
			}

			UISimplePage lastSimplePage = _pageStack.Last();
			if(lastSimplePage.LocalPageStack.Any())
			{
				UIBasePage lastLocalPage = lastSimplePage.LocalPageStack.Last();
				lastLocalPage.OnEscapePressed();
			}
			else
			{
				lastSimplePage.OnEscapePressed();
			}			
		}

		public IEnumerator<UISimplePage> GetEnumerator()
		{
			return _pageStack.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
