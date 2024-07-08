using Scripts.UI.UIStates.UICore.ObjectPoolPattern;

namespace Assets.Scripts.UI.UIPages.Pages.ReportBoxScripts.UI
{
	public class ReportBoxCreator : BaseCreator<ReportBoxView>
	{
		private void Awake()
		{
			ReportBox.Creator = this;
		}
	}
}
