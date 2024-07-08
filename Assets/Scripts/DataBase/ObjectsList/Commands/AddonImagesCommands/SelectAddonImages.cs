using DataBase.Commands;
using DataBase.DataBase.ColumnsNames;

namespace Assets.Scripts.DataBase.Commands.AddonImagesCommands
{
	/// <summary>
	/// Возвращает ImageKeys для аддона с id = AddonId
	/// </summary>
	public struct SelectAddonImages : ICommand
	{
		public int AddonId;

		public string Command()
		{
			return $"SELECT {AddonImages.ImageKey} " +
				$"FROM {AddonImages.TableName} " +
				$"WHERE {AddonImages.AddonId} = {AddonId}";
		}
	}
}
