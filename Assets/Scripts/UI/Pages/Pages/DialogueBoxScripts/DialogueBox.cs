using System;
using Assets.Scripts.UI.UIStates.UICore.ElementContainers;
using Scripts.ObjectPoolPattern;
using Scripts.UI.UIStates.UICore;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Scripts.UI.UIStates.DialogueBoxScripts
{
	public class DialogueBox : UIOverlayPage
	{
		public static ICreation<DialogueBoxView> Creator;

		private readonly DialogueBoxView _view;

		public DialogueBox(UISimplePage parent) : base(parent)
		{
			_view = Creator.Create();
			_view.Parent = Parent.Transform;
			_view.BackgroundButton.onClick.AddListener(OnEscapePressed);
			_view.YesButton.onClick.AddListener(() => OnYesButtonClick?.Invoke());
			_view.NoButton.onClick.AddListener(() => OnNoButtonClick?.Invoke());
			OnYesButtonClick += OnEscapePressed;
			OnNoButtonClick += OnEscapePressed;
		}

		public event Action OnYesButtonClick;
		public event Action OnNoButtonClick;

		public LocalizeStringEvent Text => _view.Text;

		public override void Dispose()
		{
			base.Dispose();
			GameObject.Destroy(_view.gameObject);
		}

		protected override OverlayPageView GetOverlayPageView()
		{
			return _view;
		}
	}
}