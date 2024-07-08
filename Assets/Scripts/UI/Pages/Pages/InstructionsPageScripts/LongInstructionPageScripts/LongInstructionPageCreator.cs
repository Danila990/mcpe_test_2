using Scripts.UI.UIStates.UICore.ObjectPoolPattern;

namespace Scripts.UI.UIStates.InstructionsPageScripts.LongInstructionPageScripts
{
	public class LongInstructionPageCreator : BaseCreator<LongInstructionPageView>
	{
		private void Awake()
		{
			LongInstructionPage.Creator = this;
		}
	}
}
