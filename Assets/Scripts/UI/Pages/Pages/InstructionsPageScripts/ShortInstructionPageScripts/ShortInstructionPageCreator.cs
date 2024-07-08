using Scripts.UI.UIStates.UICore.ObjectPoolPattern;

namespace Scripts.UI.UIStates.InstructionsPageScripts.ShortInstructionPageScripts
{
	public class ShortInstructionPageCreator : BaseCreator<ShortInstructionPageView>
	{
		private void Awake()
		{
			ShortInstructionPage.Creator = this;
		}
	}
}
