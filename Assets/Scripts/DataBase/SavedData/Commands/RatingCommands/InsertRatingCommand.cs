using Assets.Scripts.DataBase.SavedData.ColumnsNames;
using DataBase.Commands;

namespace Assets.Scripts.DataBase.SavedData.Commands.RatingCommands
{
	public struct InsertRatingCommand : ICommand
	{
		public int AddonId;
		public int Rate;

		public string Command()
		{
			return $"INSERT OR REPLACE INTO {Rating.TableName}({Rating.AddonId}, {Rating.Rate}) " +
				$"VALUES({AddonId}, {Rate})";
		}
	}
}
