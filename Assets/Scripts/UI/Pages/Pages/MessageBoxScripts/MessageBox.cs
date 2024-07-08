using Assets.Scripts.UI.UIStates.UICore.ElementContainers;
using Scripts.ObjectPoolPattern;
using Scripts.UI.UIStates.UICore;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Scripts.UI.UIStates.MessageBoxScripts
{
	public class MessageBox : UIOverlayPage
	{
		public static ICreation<MessageBoxView> Creator;

		private readonly MessageBoxView _view;

		public MessageBox(UISimplePage parent) : base(parent)
		{
			_view = Creator.Create();
			_view.Parent = Parent.Transform;
			_view.BackgroundButton.onClick.AddListener(OnEscapePressed);
			_view.OkButton.onClick.AddListener(OnEscapePressed);
		}

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