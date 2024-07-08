using Scripts.UI.UIStates.UICore.ObjectPoolPattern;

namespace Scripts.UI.UIStates.MessageBoxScripts
{
	public class MessageBoxCreator : BaseCreator<MessageBoxView>
	{
		private void Awake()
		{
			MessageBox.Creator = this;
		}
	}
}
