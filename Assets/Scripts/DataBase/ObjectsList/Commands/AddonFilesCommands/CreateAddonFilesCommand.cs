using DataBase.Commands;
using DataBase.DataBase.ColumnsNames;

namespace DataBase.DataBase.Commands.AddonFilesCommands
{
	public struct CreateAddonFilesCommand : ICommand
	{
		public string Command()
		{
			return $"CREATE TABLE IF NOT EXISTS {AddonFiles.TableName}" +
				$"({AddonFiles.AddonId} INTEGER NOT NULL," +
				$"{AddonFiles.AddonKey} TEXT NOT NULL," +
				$"UNIQUE({AddonFiles.AddonId})," +
				$"FOREIGN KEY ({AddonFiles.AddonId})" +
				$"REFERENCES {Addons.TableName} ({Addons.Id}))";
		}
	}
}
