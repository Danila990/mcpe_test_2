using Assets.Scripts.DataBase.SavedData.Commands.RatingCommands;
using BucketObjectsListUpdater.DB.DBExecuters;
using DataBase.DataBase.SavedData.Commands.FavoriteAddonsCommands;
using System.Threading.Tasks;

namespace Assets.Scripts.DataBase
{
	public class DataBaseExecuters
	{
		public static readonly DataBaseExecuters Instance = new DataBaseExecuters();

		public DataBaseCommandExecuter ObjectsListCommandExecuter
		{
			get;
			private set;
		}

		public DataBaseCommandExecuter SavedDataCommandExecuter
		{
			get;
			private set;
		}

		private DataBaseExecuters()
		{

		}

		public async Task OpenObjectsListDB(string path)
		{
			ObjectsListCommandExecuter = new DataBaseCommandExecuter(path);
			await ObjectsListCommandExecuter.OpenConnection();
		}

		public async Task OpenSavedDataDB(string path)
		{
			SavedDataCommandExecuter = new DataBaseCommandExecuter(path);
			await SavedDataCommandExecuter.OpenConnection();

			var createFavoriteAddonsCommand = new CreateFavoriteAddonsCommand();
			await SavedDataCommandExecuter.ExecuteNonQuery(createFavoriteAddonsCommand);

			var createRatingCommand = new CreateRatingCommand();
			await SavedDataCommandExecuter.ExecuteNonQuery(createRatingCommand);
		}

		public void Close()
		{
			ObjectsListCommandExecuter?.Dispose();
			SavedDataCommandExecuter?.Dispose();
		}
	}
}
