using DataBase.Commands;
using DataBase.DataBase.ColumnsNames;

namespace Assets.Scripts.DataBase.Commands.AddonsCommands
{
	/// <summary>
	/// Возвращает AddonNameLocalizationId, DescriptionLocalizationId и Verions для аддона с id = AddonId
	/// </summary>
	public struct SelectAddonTextCommand : ICommand
	{
		public int AddonId;

		public string Command()
		{
			return $"SELECT {Addons.AddonNameLocalizationId}, {Addons.DescrtiptionLocalizationId}, {Addons.Versions} " +
				$"FROM {Addons.TableName} " +
				$"WHERE {Addons.Id} = {AddonId}";
		}
	}
}
