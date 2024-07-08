using DataBase.Commands;

namespace DataBase.DataBase.Commands
{
	public struct StringCommand : ICommand
	{
		public string CommandText;

		public string Command()
		{
			return CommandText;
		}
	}
}
