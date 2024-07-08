using Assets.Scripts.DataBase.SavedData.ColumnsNames;
using DataBase.Commands;

namespace DataBase.DataBase.SavedData.Commands.FavoriteAddonsCommands
{
	public struct SelectFavoriteAddonCommand : ICommand
	{
		public int AddonId;

		public string Command()
		{
			return $"SELECT {FavoriteAddons.AddonId} " +
				$"FROM {FavoriteAddons.TableName} " +
				$"WHERE {FavoriteAddons.AddonId} = {AddonId}";
		}
	}
}
