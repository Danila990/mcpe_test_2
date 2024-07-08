using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Assets.Scripts.DataBase;
using DataBase.DataBase.SavedData.Commands.FavoriteAddonsCommands;

namespace Assets.Scripts.UI.Pages.Pages.AddonPageScripts
{
	public class FavoriteAddonController
	{
		public readonly int AddonId;

		public FavoriteAddonController(int addonId)
		{
			AddonId = addonId;
		}

		public async Task<bool> IsAddonFavorite(CancellationToken token)
		{
			var command = new SelectFavoriteAddonCommand();
			command.AddonId = AddonId;
			var commandExecuter = DataBaseExecuters.Instance.SavedDataCommandExecuter;
			using(DbDataReader reader = await commandExecuter.ExecuteReader(command, token: token).ConfigureAwait(false))
			{
				while(reader.Read())
				{
					return true;
				}

				return false;
			}
		}

		public async Task SetAddonFavirite(bool isFavorite, CancellationToken token)
		{
			await (isFavorite ? AddAddonToFavorite(token) : RemoveAddonFromFavorite(token));
		}

		private async Task AddAddonToFavorite(CancellationToken token)
		{
			var command = new InsertFavoriteAddonsCommand();
			command.AddonId = AddonId;
			var commandExecuter = DataBaseExecuters.Instance.SavedDataCommandExecuter;
			await commandExecuter.ExecuteNonQuery(command, token: token).ConfigureAwait(false);
		}

		private async Task RemoveAddonFromFavorite(CancellationToken token)
		{
			var command = new DeleteFavoriteAddonsCommand();
			command.AddonId = AddonId;
			var commandExecuter = DataBaseExecuters.Instance.SavedDataCommandExecuter;
			await commandExecuter.ExecuteNonQuery(command, token: token).ConfigureAwait(false);
		}
	}
}
