using UnityEngine;

namespace Assets.Scripts.UI.UIStates.UICore.Animations.AnimationScriptableObjects.AnimationParametersByPosition
{
	[CreateAssetMenu(fileName = "New AnimationParameters", menuName = "Animation parameters/Overlay page/Side Menu animation Parameters")]
	public class SideMenuAnimationParameters : ScriptableObject
	{
		[field: Min(0)]
		[field: SerializeField]
		public float AbsolutDuration
		{
			get;
			private set;
		}

		[field: SerializeField]
		public AnimationCurve AnimationXCurve
		{
			get;
			private set;
		}

		[field: SerializeField]
		public AnimationCurve AnimationYCurve
		{
			get;
			private set;
		}
	}
}
