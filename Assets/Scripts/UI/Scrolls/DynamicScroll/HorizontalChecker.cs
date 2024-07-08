using Assets.Scripts.UI.Scrolls.DynamicScroll.New;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Scrolls
{
	public class HorizontalChecker : BaseChecker
	{
		public HorizontalChecker(ScrollRect scrollRect) : base(scrollRect)
		{
		}

		public override bool NeedAddLast(Vector3[] scrollWorldCorners, Vector3[] lastChildWorldCorners)
		{
			return lastChildWorldCorners[1].x < scrollWorldCorners[2].x ||
						lastChildWorldCorners[0].x < scrollWorldCorners[3].x;
		}
	}
}
