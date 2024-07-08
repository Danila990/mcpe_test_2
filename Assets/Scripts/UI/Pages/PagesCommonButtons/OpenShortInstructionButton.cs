using Assets.Scripts.UI.UIStates.UICore.PageStacks;
using Scripts.UI.UIStates.InstructionsPageScripts.ShortInstructionPageScripts;
using Scripts.UI.UIStates.InstructionsPageScripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.UIPages.PagesCommonButtons.InstructionsButton
{
	[RequireComponent(typeof(Button))]
	public class OpenShortInstructionButton : MonoBehaviour
	{
		[SerializeField]
		private InstructionType _instructionType;

		private void Awake()
		{
			var dropdown = GetComponent<Button>();
			dropdown.onClick.AddListener(ShowShortInstruction);
		}

		private void ShowShortInstruction()
		{
			SimplePageStack pageStack = SimplePageStack.PageStack;
			var shortInstruction = new ShortInstructionPage(pageStack);
			shortInstruction.InstructionType = _instructionType;
			pageStack.ShowLast(shortInstruction);
		}
	}
}
