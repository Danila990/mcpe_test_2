using Assets.Scripts.UI.UIStates.UICore.ElementContainers;
using Assets.Scripts.UI.UIStates.UICore.PageStacks;
using Scripts.ObjectPoolPattern;
using Scripts.UI.UIStates.UICore;
using UnityEngine;

namespace Scripts.UI.UIStates.PrivacyPolicyPageScripts
{
	public class PrivacyPolicyPage : UISimplePage
	{
		public static ICreation<PrivacyPolicyPageView> Creator;

		private readonly PrivacyPolicyPageView _view;

		public PrivacyPolicyPage(SimplePageStack mainPageStack) : base(mainPageStack)
		{
			_view = Creator.Create();
			_view.ApplyButton.onClick.AddListener(OnEscapePressed);
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
	}
}
