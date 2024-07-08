using DataBase.Commands;
using DataBase.DataBase.ColumnsNames;

namespace Assets.Scripts.DataBase.Commands.AddonsCommands
{
	/// <summary>
	/// Возвращает AddonNameLocalizationId и PreviewImageKey для аддона с id = AddonId
	/// </summary>
	public struct SelectAddonsPreviewDataCommand : ICommand
	{
		public int AddonId;

		public string Command()
		{
			return $"SELECT {Addons.AddonNameLocalizationId}, {Addons.PreviewImageKey} " +
				$"FROM {Addons.TableName} " +
				$"WHERE {Addons.Id} = {AddonId}";
		}		
	}
}
