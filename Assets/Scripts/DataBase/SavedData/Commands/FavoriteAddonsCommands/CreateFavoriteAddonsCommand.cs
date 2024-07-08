using Assets.Scripts.DataBase.SavedData.ColumnsNames;
using DataBase.Commands;

namespace DataBase.DataBase.SavedData.Commands.FavoriteAddonsCommands
{
	public struct CreateFavoriteAddonsCommand : ICommand
	{
		public string Command()
		{
			return $"CREATE TABLE IF NOT EXISTS {FavoriteAddons.TableName}" +
				$"({FavoriteAddons.AddonId} INTEGER NOT NULL," +
				$"UNIQUE({FavoriteAddons.AddonId}))";
		}
	}
}
