using DataBase.Commands;
using DataBase.DataBase.ColumnsNames;

namespace DataBase.DataBase.Commands.SeedsCommands
{
	public struct CreateSeedsCommand : ICommand
	{
		public string Command()
		{
			return $"CREATE TABLE IF NOT EXISTS {Seeds.TableName}" +
				$"({Seeds.AddonId} INTEGER NOT NULL," +
				$"{Seeds.Seed} TEXT NOT NULL," +
				$"UNIQUE({Seeds.AddonId})," +
				$"FOREIGN KEY ({Seeds.AddonId})" +
				$"REFERENCES {Addons.TableName} ({Addons.Id}))";
		}
	}
}
