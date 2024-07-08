using Scripts.UI.UIStates.UICore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.UI.UIStates.UICore.BaseStacks
{
	public class LocalPageStack : IEnumerable<UIBasePage>
	{
		private readonly LinkedList<UIBasePage> _pageStack = new LinkedList<UIBasePage>();

		public void ShowLast(UIBasePage pageToShow)
		{
			_pageStack.AddLast(pageToShow);
			pageToShow.Show();
		}

		public void HideLast()
		{
			UIBasePage pageToHide = _pageStack.Last();
			_pageStack.RemoveLast();
			pageToHide.Hide();
		}

		public IEnumerator<UIBasePage> GetEnumerator()
		{
			return _pageStack.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
