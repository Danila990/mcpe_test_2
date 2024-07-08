using DataBase.Commands;

namespace DataBase.DataBase.Commands.DataBaseCommands
{
	public struct LastInsertRowIdCommand : ICommand
	{
		public string Command()
		{
			return "SELECT last_insert_rowid()";
		}
	}
}
