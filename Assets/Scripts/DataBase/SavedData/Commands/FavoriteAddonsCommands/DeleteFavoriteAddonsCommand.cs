using Assets.Scripts.DataBase.SavedData.ColumnsNames;
using DataBase.Commands;

namespace DataBase.DataBase.SavedData.Commands.FavoriteAddonsCommands
{
	public struct DeleteFavoriteAddonsCommand : ICommand
	{
		public int AddonId;

		public string Command()
		{
			return $"DELETE FROM {FavoriteAddons.TableName} " +
				$"WHERE {FavoriteAddons.AddonId} = {AddonId}";
		}
	}
}
