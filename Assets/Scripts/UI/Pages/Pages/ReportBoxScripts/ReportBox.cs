using Assets.Scripts.UI.UIStates.UICore.ElementContainers;
using Scripts.ObjectPoolPattern;
using Scripts.UI.UIStates.UICore;
using UnityEngine;
using Assets.Scripts.UI.UIPages.Pages.ReportBoxScripts.UI;
using Scripts.UI.NativeMessageBoxScripts;
using System.Threading.Tasks;

namespace Assets.Scripts.UI.UIPages.Pages.ReportBoxScripts
{
	public class ReportBox : UIOverlayPage
	{
		public static ICreation<ReportBoxView> Creator;

		private readonly ReportBoxView _view;

		public ReportBox(UISimplePage parent) : base(parent)
		{
			_view = Creator.Create();
			_view.Parent = Parent.Transform;
			_view.BackgroundButton.onClick.AddListener(OnEscapePressed);
			_view.SendButton.onClick.AddListener(OnSendButtonClick);
		}

		public override void Dispose()
		{
			base.Dispose();
			GameObject.Destroy(_view.gameObject);
		}

		protected override OverlayPageView GetOverlayPageView()
		{
			return _view;
		}

		private async void OnSendButtonClick()
		{
			string message = _view.InputField.text;
			if(!string.IsNullOrEmpty(message))
			{
				ReportMessage(message);
				await ShowNativeMessageSent();
			}

			OnEscapePressed();
		}

		private void ReportMessage(string message)
		{
			AppMetrica.Instance.ReportEvent(message);
		}

		private async Task ShowNativeMessageSent()
		{
			string nativeMessage = await _view.MessageSent.GetLocalizedStringAsync().Task;
			NativeMessageBoxWrapper.Show(nativeMessage);
		}
	}
}
