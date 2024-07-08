using DataBase.Commands;
using DataBase.DataBase.ColumnsNames;

namespace DataBase.DataBase.Commands.AddonImagesCommands
{
	public struct CreateAddonImagesCommand : ICommand
	{
		public string Command()
		{
			return $"CREATE TABLE IF NOT EXISTS {AddonImages.TableName}" +
			$"({AddonImages.Id} INTEGER PRIMARY KEY AUTOINCREMENT," +
			$"{AddonImages.ImageKey} TEXT NOT NULL," +
			$"{AddonImages.AddonId} INTEGER NOT NULL," +
			$"FOREIGN KEY ({AddonImages.AddonId})" +
			$"REFERENCES {Addons.TableName} ({Addons.Id}))";
		}
	}
}
