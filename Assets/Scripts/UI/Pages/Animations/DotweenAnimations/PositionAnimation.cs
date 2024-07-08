using Assets.Scripts.UI.UIStates.UICore.Animations.AnimationScriptableObjects.AnimationParametersByPosition;
using System;
using UnityEngine;

namespace Scripts.UI.UIStates.UICore.Animations.AnimationScriptableObjects.AnimationParametersByPosition
{
	[Serializable]
	public class PositionAnimation
	{
		[field: SerializeField]
		public Vector2 AnimationStartPosition
		{
			get;
			private set;
		}

		[field: SerializeField]
		public Vector2 AnimationEndPosition
		{
			get;
			private set;
		}

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