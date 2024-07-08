using Scripts.UI.UIStates.UICore.Animations;
using UnityEngine;

namespace Scripts.UI.UIStates.UICore
{
	[RequireComponent(typeof(BasePageAnimator), typeof(CanvasGroup))]
	public abstract class BasePageView : MonoBehaviour
	{
		protected CanvasGroup _canvasGroup;

		public Transform Parent
		{
			get
			{
				return transform.parent;
			}

			set
			{
				transform.SetParent(value, false);
			}
		}

		protected virtual void Awake()
		{
			_canvasGroup = GetComponent<CanvasGroup>();
			var animator = GetComponent<BasePageAnimator>();
			animator.OnShowAnimationStart += () => EnableInteraction(true);
			animator.OnHideAnimationStart += () => EnableInteraction(false);
		}

		public virtual void EnableInteraction(bool isEnable)
		{
			_canvasGroup.blocksRaycasts = isEnable;
		}
	}
}
