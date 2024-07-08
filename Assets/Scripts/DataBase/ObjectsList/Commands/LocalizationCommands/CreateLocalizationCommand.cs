using DataBase.Commands;
using DataBase.DataBase.ColumnsNames;

namespace DataBase.DataBase.Commands.LocalizationCommands
{
	public struct CreateLocalizationCommand : ICommand
	{
		public string Command()
		{
			return $"CREATE TABLE IF NOT EXISTS {Localization.TableName}" +
			$"({AddonImages.Id} INTEGER PRIMARY KEY AUTOINCREMENT," +
			$"{Localization.EnLocale} TEXT," + 
			$"{Localization.FrLocale} TEXT," +
			$"{Localization.DeLocale} TEXT," +
			$"{Localization.IdLocale} TEXT," +
			$"{Localization.PlLocale} TEXT," +
			$"{Localization.PtLocale} TEXT," +
			$"{Localization.RuLocale} TEXT," +
			$"{Localization.EsLocale} TEXT," +
			$"{Localization.ItLocale} TEXT," +
			$"{Localization.TrLocale} TEXT," +
			$"{Localization.UkLocale} TEXT)";
		}
	}
}
