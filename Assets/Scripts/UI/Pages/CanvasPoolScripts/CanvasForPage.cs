using Scripts.ObjectPoolPattern;
using Scripts.UI.UIStates.CanvasPoolScripts;
using System;

namespace Assets.Scripts.UI.UIPages.CanvasPoolScripts
{
	public class CanvasForPage : IDisposable
	{
		public static IPool<CanvasGameObject> Pool;

		public readonly CanvasGameObject Canvas;

		public CanvasForPage()
		{
			Canvas = Pool.GetFree();
		}

		public void Dispose()
		{
			Pool.SetFree(Canvas);
		}
	}
}
