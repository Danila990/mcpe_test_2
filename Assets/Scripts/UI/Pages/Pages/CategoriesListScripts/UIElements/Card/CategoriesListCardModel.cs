using System.Collections.Generic;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Assets.Scripts.DataBase.Commands.CategoriesCommands;
using Assets.Scripts.DataBase;
using Assets.Scripts.FillData.Categories;

namespace Assets.Scripts.UI.Pages.Pages.CategoriesListScripts.UIElements.Card
{
	public class CategoriesListCardModel
	{
		public readonly int CategoryId;

		public CategoriesListCardModel(int categoryId)
		{
			CategoryId = categoryId;
		}

		public async Task<CategoryData> GetCategoryData(CancellationToken token)
		{
			var command = new SelectCategoriesDataCommand();
			command.CategoryId = CategoryId;
			var commandExecuter = DataBaseExecuters.Instance.ObjectsListCommandExecuter;
			using(DbDataReader reader = await commandExecuter.ExecuteReader(command, token: token).ConfigureAwait(false))
			{
				while(reader.Read())
				{
					CategoryData result = new CategoryData();
					result.CategoryNameLocalizationId = reader.GetInt32(0);
					if(!reader.IsDBNull(1))
					{
						result.PreviewImageKey = reader.GetString(1);
					}

					return result;
				}
			}

			throw new KeyNotFoundException("Данных не оказалось");
		}
	}
}
