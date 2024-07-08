using DataBase.Commands;
using DataBase.DataBase.ColumnsNames;

namespace DataBase.DataBase.Commands.LocalizationCommands
{
	public struct InsertLocalizationCommand : ICommand
	{
		public string EnLocale;
		public string FrLocale;
		public string DeLocale;
		public string IdLocale;
		public string PlLocale;
		public string PtLocale;
		public string RuLocale;
		public string EsLocale;
		public string ItLocale;
		public string TrLocale;
		public string UkLocale;

		public string Command()
		{
			string enLocaleValueInDB = EnLocale == null ? "NULL" : $"'{EnLocale}'";
			string frLocaleValueInDB = FrLocale == null ? "NULL" : $"'{FrLocale}'";
			string deLocaleValueInDB = DeLocale == null ? "NULL" : $"'{DeLocale}'";
			string idLocaleValueInDB = IdLocale == null ? "NULL" : $"'{IdLocale}'";
			string plLocaleValueInDB = PlLocale == null ? "NULL" : $"'{PlLocale}'";
			string ptLocaleValueInDB = PtLocale == null ? "NULL" : $"'{PtLocale}'";
			string ruLocaleValueInDB = RuLocale == null ? "NULL" : $"'{RuLocale}'";
			string esLocaleValueInDB = EsLocale == null ? "NULL" : $"'{EsLocale}'";
			string itLocaleValueInDB = ItLocale == null ? "NULL" : $"'{ItLocale}'";
			string trLocaleValueInDB = TrLocale == null ? "NULL" : $"'{TrLocale}'";
			string ukLocaleValueInDB = UkLocale == null ? "NULL" : $"'{UkLocale}'";

			return $"INSERT INTO {Localization.TableName}({Localization.EnLocale}, {Localization.FrLocale}, " +
				$"{Localization.DeLocale}, {Localization.IdLocale}, {Localization.PlLocale}, " +
				$"{Localization.PtLocale}, {Localization.RuLocale}, {Localization.EsLocale}, " +
				$"{Localization.ItLocale}, {Localization.TrLocale}, {Localization.UkLocale})" +
				$"VALUES({enLocaleValueInDB}, {frLocaleValueInDB}, {deLocaleValueInDB}, {idLocaleValueInDB}, " +
				$"{plLocaleValueInDB}, {ptLocaleValueInDB}, {ruLocaleValueInDB}, {esLocaleValueInDB}, " +
				$"{itLocaleValueInDB}, {trLocaleValueInDB}, {ukLocaleValueInDB})";
		}
	}
}
