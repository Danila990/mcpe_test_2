using Assets.Scripts.DataBase.SavedData.ColumnsNames;
using DataBase.Commands;

namespace Assets.Scripts.DataBase.SavedData.Commands.RatingCommands
{
	public struct CreateRatingCommand : ICommand
	{
		public string Command()
		{
			return $"CREATE TABLE IF NOT EXISTS {Rating.TableName}" +
				$"({Rating.AddonId} INTEGER NOT NULL," +
				$"{Rating.Rate} INTEGER NOT NULL CHECK({Rating.Rate} > 0 AND {Rating.Rate} <= 5)," +
				$"UNIQUE({Rating.AddonId}))";
		}
	}
}
