using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Scrolls.DynamicScroll.New
{
	public class VerticalChecker : BaseChecker
	{
		public VerticalChecker(ScrollRect scrollRect) : base(scrollRect)
		{
		}

		public override bool NeedAddLast(Vector3[] scrollWorldCorners, Vector3[] lastChildWorldCorners)
		{
			return lastChildWorldCorners[1].y > scrollWorldCorners[0].y ||
						lastChildWorldCorners[2].y > scrollWorldCorners[3].y;
		}
	}
}
