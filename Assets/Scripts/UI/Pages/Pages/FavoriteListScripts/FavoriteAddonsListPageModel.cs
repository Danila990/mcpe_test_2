using Assets.Scripts.DataBase;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using System.Threading;
using DataBase.DataBase.SavedData.Commands.FavoriteAddonsCommands;
using Assets.Scripts.DataBase.GeneralCommands;
using DataBase.Commands;

namespace Assets.Scripts.UI.UIPages.Pages.AddonsListScripts.FavoriteList
{
	public class FavoriteAddonsListPageModel
	{
		public int LoadPerRequest = 5;

		private int _offset = 0;

		public async Task<List<int>> LoadNextAddonsPageData(CancellationToken token)
		{
			List<int> addonsId = await GetAddonsIdFromDataBase(token);
			token.ThrowIfCancellationRequested();
			_offset += addonsId.Count;
			return addonsId;
		}

		private async Task<List<int>> GetAddonsIdFromDataBase(CancellationToken token)
		{
			var commandExecuter = DataBaseExecuters.Instance.SavedDataCommandExecuter;
			var command = SelectAddonsIdCommand();
			using(DbDataReader reader = await commandExecuter.ExecuteReader(command, token: token).ConfigureAwait(false))
			{
				var result = new List<int>(LoadPerRequest);
				while(reader.Read())
				{
					int addonId = reader.GetInt32(0);
					result.Add(addonId);
				}

				return result;
			}
		}

		private UniteCommand SelectAddonsIdCommand()
		{
			var selectFavoriteAddonsIdCommand = new SelectFavoriteAddonsCommand();
			var limitCommand = new LimitCommand();
			limitCommand.Limit = LoadPerRequest;
			var offsetCommand = new OffsetCommand();
			offsetCommand.Offset = _offset;
			var commandsList = new List<ICommand>
			{
				selectFavoriteAddonsIdCommand,
				limitCommand,
				offsetCommand,
			};

			var command = new UniteCommand();
			command.Commands = commandsList;
			return command;
		}
	}
}
