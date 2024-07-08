using DataBase.Commands;
using DataBase.DataBase.ColumnsNames;
using System;

namespace Assets.Scripts.DataBase.Commands.LocalizationCommands
{
	public struct SelectLocalizationCommand : ICommand
	{
		public int LocalizationId;
		public string LocaleCode;

		public string Command()
		{
			return $"SELECT {GetLocaleColumnName(LocaleCode)} " +
				$"FROM {Localization.TableName} " +
				$"WHERE {Localization.Id} = {LocalizationId}";
		}

		private static string GetLocaleColumnName(string localeCode)
		{
			switch(localeCode)
			{
				case "en":
					return Localization.EnLocale;
				case "fr":
					return Localization.FrLocale;
				case "de":
					return Localization.DeLocale;
				case "id":
					return Localization.IdLocale;
				case "pl":
					return Localization.PlLocale;
				case "pt":
					return Localization.PtLocale;
				case "ru":
					return Localization.RuLocale;
				case "es":
					return Localization.EsLocale;
				case "it":
					return Localization.ItLocale;
				case "tr":
					return Localization.TrLocale;
				case "uk":
					return Localization.UkLocale;
				default:
					throw new ArgumentException($"Неизвестная локаль {localeCode}");
			}
		}
	}
}
