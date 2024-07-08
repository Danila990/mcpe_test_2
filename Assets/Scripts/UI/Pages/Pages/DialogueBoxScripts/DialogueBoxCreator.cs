using Scripts.UI.UIStates.UICore.ObjectPoolPattern;

namespace Scripts.UI.UIStates.DialogueBoxScripts
{
	public class DialogueBoxCreator : BaseCreator<DialogueBoxView>
	{
		private void Awake()
		{
			DialogueBox.Creator = this;
		}
	}
}
