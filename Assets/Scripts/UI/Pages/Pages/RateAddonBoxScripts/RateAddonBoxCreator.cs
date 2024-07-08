using Scripts.UI.UIStates.UICore.ObjectPoolPattern;

namespace Assets.Scripts.UI.UIPages.Pages.RateAddonBoxScripts.UI
{
	public class RateAddonBoxCreator : BaseCreator<RateAddonBoxView>
	{
		private void Awake()
		{
			RateAddonBox.Creator = this;
		}
	}
}
