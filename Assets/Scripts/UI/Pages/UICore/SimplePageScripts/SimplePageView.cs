using Assets.Scripts.UI.UIPages.CanvasPoolScripts;
using Scripts.UI.UIStates.UICore;
using UnityEngine;

namespace Assets.Scripts.UI.UIStates.UICore.ElementContainers
{
	[RequireComponent(typeof(SimplePageAnimator))]
	public abstract class SimplePageView : BasePageView
	{
		private CanvasForPage _canvas;

		[field: SerializeField]
		public bool IsHideByEscape
		{
			get;
			private set;
		}

		[field: SerializeField]
		public bool IsShowInterAdAfterHide
		{
			get;
			private set;
		}

		private CanvasForPage Canvas
		{
			get
			{
				return _canvas;
			}

			set
			{
				_canvas = value;
				Parent = _canvas == null ? null : _canvas.Canvas.transform;
			}
		}

		protected override void Awake()
		{
			base.Awake();
			Canvas = new CanvasForPage();
			var animator = GetComponent<SimplePageAnimator>();
			animator.OnMoveOutAnimationStart += () => EnableInteraction(false);
			animator.OnMoveInAnimationStart += () => EnableInteraction(true);
		}

		protected void OnDestroy()
		{
			Canvas.Dispose();
		}
	}
}
