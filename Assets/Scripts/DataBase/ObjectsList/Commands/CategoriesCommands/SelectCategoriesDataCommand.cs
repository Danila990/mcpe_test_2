using DataBase.Commands;
using DataBase.DataBase.ColumnsNames;

namespace Assets.Scripts.DataBase.Commands.CategoriesCommands
{
	/// <summary>
	/// Возвращает CategoryLocalizationId и PreviewImageKey с id = CategoryId
	/// </summary>
	public struct SelectCategoriesDataCommand : ICommand
	{
		public int CategoryId;

		public string Command()
		{
			return $"SELECT {Categories.CategoryNameLocalizationId}, {Categories.PreviewImageKey} " +
				$"FROM {Categories.TableName} " +
				$"WHERE {Categories.Id} = {CategoryId}";
		}
	}
}
