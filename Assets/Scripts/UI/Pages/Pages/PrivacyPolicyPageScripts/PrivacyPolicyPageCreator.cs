using Scripts.UI.UIStates.UICore.ObjectPoolPattern;

namespace Scripts.UI.UIStates.PrivacyPolicyPageScripts
{
	public class PrivacyPolicyPageCreator : BaseCreator<PrivacyPolicyPageView>
	{
		private void Awake()
		{
			PrivacyPolicyPage.Creator = this;
		}
	}
}
