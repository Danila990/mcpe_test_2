using DataBase.Commands;
using DataBase.DataBase.ColumnsNames;

namespace DataBase.DataBase.Commands.AddonsCommands
{
	public struct CreateAddonsCommand : ICommand
	{
		public string Command()
		{
			return $"CREATE TABLE IF NOT EXISTS {Addons.TableName}" +
				$"({Addons.Id} INTEGER PRIMARY KEY AUTOINCREMENT," +
				$"{Addons.AddonNameLocalizationId} INTEGER," +
				$"{Addons.PreviewImageKey} TEXT," +
				$"{Addons.DescrtiptionLocalizationId} INTEGER," +
				$"{Addons.Versions} TEXT," +
				$"FOREIGN KEY ({Addons.AddonNameLocalizationId})" +
				$"REFERENCES {Localization.TableName} ({Localization.Id}),"	+
				$"FOREIGN KEY ({Addons.DescrtiptionLocalizationId})" +
				$"REFERENCES {Localization.TableName} ({Localization.Id}))";
		}
	}
}
