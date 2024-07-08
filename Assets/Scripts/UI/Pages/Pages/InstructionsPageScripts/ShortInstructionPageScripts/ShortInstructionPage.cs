using Assets.Scripts.UI.UIStates.InstructionsPageScripts;
using Assets.Scripts.UI.UIStates.UICore.ElementContainers;
using Assets.Scripts.UI.UIStates.UICore.PageStacks;
using Scripts.ObjectPoolPattern;
using Scripts.UI.UIStates.InstructionsPageScripts.LongInstructionPageScripts;
using Scripts.UI.UIStates.UICore;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.UI.UIStates.InstructionsPageScripts.ShortInstructionPageScripts
{
	public class ShortInstructionPage : UISimplePage
	{
		public static ICreation<ShortInstructionPageView> Creator;

		private readonly ShortInstructionPageView _view;

		private InstructionType _instructionType;

		public ShortInstructionPage(SimplePageStack mainPageStack) 
			: base(mainPageStack)
		{
			_view = Creator.Create();
			_view.OnLongInstructionButton.onClick.AddListener(ShowLongInstruction);
		}

		public InstructionType InstructionType
		{
			get
			{
				return _instructionType;
			}

			set
			{
				_instructionType = value;
				switch(_instructionType)
				{
					case InstructionType.Addon:
						SetInstructionContent(_view.McaddonContentElements);
						break;
					case InstructionType.World:
						SetInstructionContent(_view.McworldContentElements);
						break;
					case InstructionType.Skins:
						SetInstructionContent(_view.McskinsContentElements);
						break;
					default:
						throw new ArgumentOutOfRangeException(nameof(_instructionType), "Not expected instruction value");
				}
			}
		}

		public override void Dispose()
		{
			base.Dispose();
			GameObject.Destroy(_view.gameObject);
		}

		protected override SimplePageView GetSimplePageView()
		{
			return _view;
		}

		private void ShowLongInstruction()
		{
			LongInstructionPage longInstruction = new LongInstructionPage(MainPageStack);
			longInstruction.InstructionType = InstructionType;
			MainPageStack.ShowLast(longInstruction);
		}

		private void SetInstructionContent(List<InstructionComponent> instructionContent)
		{
			Transform contentTransform = _view.TextScrollRect.content;
			foreach(var instructionElement in instructionContent)
			{
				if(instructionElement.IsText)
				{
					var element = GameObject.Instantiate(_view.TextPrefab);
					element.transform.SetParent(contentTransform, false);
					element.StringReference = instructionElement.Text;
				}
				else if(instructionElement.IsImage)
				{
					var element = GameObject.Instantiate(_view.ImagePrefab);
					element.transform.SetParent(contentTransform, false);
					element.sprite = instructionElement.Image;
				}
			}
		}
	}
}
