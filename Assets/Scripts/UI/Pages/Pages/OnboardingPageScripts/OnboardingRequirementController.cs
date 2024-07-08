using UnityEngine;

namespace Assets.Scripts
{
	public static class OnboardingRequirementController
	{
		private const string _isNeedOnboardingPlayerPrefsKey = "IsNeedOnboarding";

		public static bool IsNecessary
		{
			get
			{
				return !PlayerPrefs.HasKey(_isNeedOnboardingPlayerPrefsKey);
			}
		}

		public static void SetNotNecessary()
		{
			PlayerPrefs.SetInt(_isNeedOnboardingPlayerPrefsKey, 0);
		}
	}
}
