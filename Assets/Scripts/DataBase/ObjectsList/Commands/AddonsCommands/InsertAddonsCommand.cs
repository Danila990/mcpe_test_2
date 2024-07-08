using DataBase.Commands;
using DataBase.DataBase.ColumnsNames;

namespace DataBase.DataBase.Commands.AddonsCommands
{
	public struct InsertAddonsCommand : ICommand
	{
		public int? AddonNameLocalizationId;
		public string PreviewImageKey;
		public int? DescrtiptionLocalizationId;
		public string Versions;

		public string Command()
		{
			string addonNameLocalizationIdValueInDB = 
				AddonNameLocalizationId == null ? "NULL" : $"'{AddonNameLocalizationId}'";
			string previewImageKeyValueInDB = PreviewImageKey == null ? "NULL" : $"'{PreviewImageKey}'";
			string descrtiptionLocalizationIdValueInDB =
				DescrtiptionLocalizationId == null ? "NULL" : $"'{DescrtiptionLocalizationId}'";
			string versionsValueInDB = Versions == null ? "NULL" : $"'{Versions}'";

			return $"INSERT INTO {Addons.TableName}({Addons.AddonNameLocalizationId}, " +
				$"{Addons.PreviewImageKey}, {Addons.DescrtiptionLocalizationId}, {Addons.Versions})" +
				$"VALUES({addonNameLocalizationIdValueInDB}, {previewImageKeyValueInDB}, " +
				$"{descrtiptionLocalizationIdValueInDB}, {versionsValueInDB})";
		}
	}
}
