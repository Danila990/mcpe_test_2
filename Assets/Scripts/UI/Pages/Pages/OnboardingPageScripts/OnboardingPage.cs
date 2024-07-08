using Assets.Scripts.UI.UIStates.UICore.ElementContainers;
using Scripts.ObjectPoolPattern;
using UnityEngine;
using Scripts.UI.UIStates.UICore;
using Assets.Scripts.UI.UIPages.Pages.OnboardingPageScripts.UI;

namespace Assets.Scripts.UI.UIPages.Pages.OnboardingPageScripts
{
	public class OnboardingPage : UIOverlayPage
	{
		public static ICreation<OnboardingPageView> Creator;

		private readonly OnboardingPageView _view;

		public OnboardingPage(UISimplePage parent) : base(parent)
		{
			_view = Creator.Create();
			_view.Parent = Parent.Transform;
			_view.PreviousButton.onClick.AddListener(_view.ScrollRect.SetPreviousChildWithLerp);
			_view.NextButton.onClick.AddListener(ShowNextScreenOrHidePage);
			_view.ScrollRect.SetChild(0);
		}

		public override void Dispose()
		{
			base.Dispose();
			OnboardingRequirementController.SetNotNecessary();
			GameObject.Destroy(_view.gameObject);
		}

		protected override OverlayPageView GetOverlayPageView()
		{
			return _view;
		}

		private void ShowNextScreenOrHidePage()
		{
			if(_view.ScrollRect.NowChild + 1 == _view.ScrollRect.ScrollRect.content.childCount)
			{
				MainPageStack.HideLast();
			}
			else
			{
				_view.ScrollRect.SetNextChildWithLerp();
			}
		}
	}
}
