using UnityEngine;

namespace Scripts.UI.UIStates.UICore.Animations.AnimationScriptableObjects.AnimationParametersByPosition
{
	[CreateAssetMenu(fileName = "New AnimationParameters", menuName = "Animation parameters/Simple page/Position animation parameters")]
	public class SimplePagePositionAnimationParameters : ScriptableObject
	{
		[field: SerializeField]
		public PositionAnimation ShowAnimation
		{
			get;
			private set;
		}

		[field: SerializeField]
		public PositionAnimation MoveOutAnimation
		{
			get;
			private set;
		}

		[field: SerializeField]
		public PositionAnimation MoveInAnimation
		{
			get;
			private set;
		}

		[field: SerializeField] 
		public PositionAnimation HideAnimation
		{
			get;
			private set;
		}
	}
}