using Scripts.UI.UIStates.UICore.ObjectPoolPattern;

namespace Scripts.UI.UIStates.AddonPageScripts
{
	public class AddonPageCreator : BaseCreator<AddonPageView>
	{
		private void Awake()
		{
			AddonPage.Creator = this;
		}
	}
}
