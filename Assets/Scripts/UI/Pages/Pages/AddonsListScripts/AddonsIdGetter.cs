using System.Collections.Generic;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Assets.Scripts.DataBase.Commands.AddonsCommands;
using Assets.Scripts.DataBase.GeneralCommands;
using Assets.Scripts.DataBase;
using DataBase.Commands;

namespace Assets.Scripts.UI.Pages.Pages.AddonsListScripts
{
	public class AddonsIdGetter
	{
		public readonly int? CategoryId;

		public int LoadPerRequest = 5;

		private int _offset = 0;

		public AddonsIdGetter(int? categoryId)
		{
			CategoryId = categoryId;
		}

		public async Task<List<int>> LoadNextAddonsId(CancellationToken token)
		{
			List<int> addonsId = await GetAddonsIdFromDataBase(token);
			_offset += addonsId.Count;
			token.ThrowIfCancellationRequested();
			return addonsId;
		}

		private async Task<List<int>> GetAddonsIdFromDataBase(CancellationToken token)
		{
			var commandExecuter = DataBaseExecuters.Instance.ObjectsListCommandExecuter;
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
			var selectAddonsIdCommand = new SelectAddonsIdCommand();
			selectAddonsIdCommand.CategoryId = CategoryId;
			var limitCommand = new LimitCommand();
			limitCommand.Limit = LoadPerRequest;
			var offsetCommand = new OffsetCommand();
			offsetCommand.Offset = _offset;
			var commandsList = new List<ICommand>
			{
				selectAddonsIdCommand,
				limitCommand,
				offsetCommand,
			};

			var command = new UniteCommand();
			command.Commands = commandsList;
			return command;
		}
	}
}
