using DataBase.Commands;

namespace DataBase.DataBase.Commands.DataBaseCommands
{
	public struct DropTableCommand : ICommand
	{
		public string TableName;

		public string Command()
		{
			return $"DROP TABLE IF EXISTS {TableName}";
		}
	}
}
