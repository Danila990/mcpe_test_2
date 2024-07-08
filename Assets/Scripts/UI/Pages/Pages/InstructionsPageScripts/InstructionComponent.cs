using System;
using UnityEngine;
using UnityEngine.Localization;

namespace Assets.Scripts.UI.UIStates.InstructionsPageScripts
{
	[Serializable]
	public class InstructionComponent
	{
		public LocalizedString Text;
		public Sprite Image;

		public bool IsText => !Text.IsEmpty;

		public bool IsImage => Image != null;
	}
}
