using Assets.Scripts.DataBase.SavedData.ColumnsNames;
using DataBase.Commands;

namespace DataBase.DataBase.SavedData.Commands.FavoriteAddonsCommands
{
	public struct SelectFavoriteAddonsCommand : ICommand
	{
		public string Command()
		{
			return $"SELECT {FavoriteAddons.AddonId} " +
				$"FROM {FavoriteAddons.TableName}";
		}
	}
}
