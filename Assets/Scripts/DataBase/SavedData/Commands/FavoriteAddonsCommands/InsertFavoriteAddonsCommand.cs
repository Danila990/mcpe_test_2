using Assets.Scripts.DataBase.SavedData.ColumnsNames;
using DataBase.Commands;

namespace DataBase.DataBase.SavedData.Commands.FavoriteAddonsCommands
{
	public struct InsertFavoriteAddonsCommand : ICommand
	{
		public int AddonId;

		public string Command()
		{
			return $"INSERT INTO {FavoriteAddons.TableName}({FavoriteAddons.AddonId}) " +
				$"VALUES({AddonId})";
		}
	}
}
