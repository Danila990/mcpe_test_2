using System;
using UnityEngine;

namespace Scripts.UI.UIStates.UICore.Animations.AnimationScriptableObjects.AnimationParametersByCanvasGroup
{
	[Serializable]
	public class AlphaAnimation
	{
		[field: SerializeField]
		public float AnimationStartAlpha
		{
			get;
			private set;
		}

		[field: SerializeField]
		public float AnimationEndAlpha
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
		public AnimationCurve AnimationCurve
		{
			get;
			private set;
		}
	}
}