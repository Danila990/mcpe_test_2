using Assets.Scripts.DataBase.SavedData.ColumnsNames;
using DataBase.Commands;

namespace Assets.Scripts.DataBase.SavedData.Commands.RatingCommands
{
	public struct SelectRatingCommand : ICommand
	{
		public int AddonId;

		public string Command()
		{
			return $"SELECT {Rating.Rate} " +
				$"FROM {Rating.TableName} " +
				$"WHERE {Rating.AddonId} = {AddonId}";
		}
	}
}
