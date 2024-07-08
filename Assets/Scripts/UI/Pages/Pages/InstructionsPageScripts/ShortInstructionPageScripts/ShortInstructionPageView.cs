using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.UI.UIStates.InstructionsPageScripts;
using UnityEngine.Localization.Components;
using Assets.Scripts.UI.UIStates.UICore.ElementContainers;

namespace Scripts.UI.UIStates.InstructionsPageScripts.ShortInstructionPageScripts
{
	public class ShortInstructionPageView : SimplePageView
	{
		[field: SerializeField]
		public Button OnLongInstructionButton
		{
			get;
			private set;
		}

		[field: SerializeField]
		public ScrollRect TextScrollRect
		{
			get;
			private set;
		}

		[field: SerializeField]
		public LocalizeStringEvent TextPrefab
		{
			get;
			private set;
		}

		[field: SerializeField]
		public Image ImagePrefab
		{
			get;
			private set;
		}

		[field: SerializeField]
		public List<InstructionComponent> McaddonContentElements
		{
			get;
			private set;
		}

		[field: SerializeField]
		public List<InstructionComponent> McworldContentElements
		{
			get;
			private set;
		}

		[field: SerializeField]
		public List<InstructionComponent> McskinsContentElements
		{
			get;
			private set;
		}
	}
}
