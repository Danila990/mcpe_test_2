using System;
using Scripts.UI.UIStates.UICore.Animations.AnimationScriptableObjects.AnimationParametersByCanvasGroup;
using UnityEngine;

namespace Assets.Scripts.UI.Pages.Animations.DotweenAnimations.SimplePage
{
	[CreateAssetMenu(fileName = "New AnimationParameters", menuName = "Animation parameters/Simple page/Alpha animation parameters")]
	public class SimplePageAlphaAnimationParameters : ScriptableObject
	{
		[field: SerializeField]
		public AlphaAnimation ShowAnimation
		{
			get;
			private set;
		}

		[field: SerializeField]
		public AlphaAnimation MoveOutAnimation
		{
			get;
			private set;
		}

		[field: SerializeField]
		public AlphaAnimation MoveInAnimation
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
