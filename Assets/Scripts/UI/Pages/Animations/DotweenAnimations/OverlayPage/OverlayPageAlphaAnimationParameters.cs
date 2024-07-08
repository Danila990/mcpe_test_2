using UnityEngine;

namespace Scripts.UI.UIStates.UICore.Animations.AnimationScriptableObjects.AnimationParametersByCanvasGroup
{
	[CreateAssetMenu(fileName = "New AnimationParameters", menuName = "Animation parameters/Overlay page/Alpha animation parameters")]
	public class OverlayPageAlphaAnimationParameters : ScriptableObject
	{
		[field: SerializeField]
		public AlphaAnimation ShowAnimation
		{
			get;
			private set;
		}

		[field: SerializeField]
		public AlphaAnimation HideAnimation
		{
			get;
			private set;
		}
	}
}