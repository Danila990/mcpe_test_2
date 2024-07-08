using DataBase.Commands;
using DataBase.DataBase.ColumnsNames;

namespace Assets.Scripts.DataBase.Commands.AddonFilesCommands
{
	/// <summary>
	/// Возвращает AddonKey для аддона с id = AddonId
	/// </summary>
	public struct SelectAddonFilesCommand : ICommand
	{
		public int AddonId;

		public string Command()
		{
			return $"SELECT {AddonFiles.AddonKey} " +
				$"FROM {AddonFiles.TableName} " +
				$"WHERE {AddonFiles.AddonId} = {AddonId}";
		}
	}
}
