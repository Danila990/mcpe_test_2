using Assets.Scripts.DataBase;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using System.Threading;
using Assets.Scripts.DataBase.Commands.CategoriesCommands;
using Assets.Scripts.UI.Pages.Pages.AddonsListScripts;

namespace Assets.Scripts.UI.UIStates.CategoriesListScripts
{
	public class CategoriesListPageModel
	{
		private readonly int? _parentCategory;

		private AddonsIdGetter _addonsIdGetter;

		public CategoriesListPageModel(int? parentCategory)
		{
			_parentCategory = parentCategory;
		}

		public int LoadAddonsPerRequest
		{
			get => _addonsIdGetter.LoadPerRequest;
			set => _addonsIdGetter.LoadPerRequest = value;
		}

		public async Task<List<int>> GetCategoriesId(CancellationToken token)
		{
			var commandExecuter = DataBaseExecuters.Instance.ObjectsListCommandExecuter;
			var command = new SelectCategoriesIdCommand();
			command.ParentCategory = _parentCategory;
			using(DbDataReader reader = await commandExecuter.ExecuteReader(command, token: token).ConfigureAwait(false))
			{
				var result = new List<int>();
				while(reader.Read())
				{
					int categoryId = reader.GetInt32(0);
					result.Add(categoryId);
				}

				return result;
			}
		}

		public void SetCategoryToLoadAddons(int categoryId)
		{
			_addonsIdGetter = new AddonsIdGetter(categoryId);
		}

		public Task<List<int>> LoadNextAddonsId(CancellationToken token)
		{
			return _addonsIdGetter.LoadNextAddonsId(token);
		}
	}
}
