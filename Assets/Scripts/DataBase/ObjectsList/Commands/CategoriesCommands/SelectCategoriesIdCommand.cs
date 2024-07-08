using DataBase.Commands;
using DataBase.DataBase.ColumnsNames;

namespace Assets.Scripts.DataBase.Commands.CategoriesCommands
{
	/// <summary>
	/// Возвращает категории c ParentCategory(опционально, т.е. можно без него)
	/// </summary>
	public struct SelectCategoriesIdCommand : ICommand
	{
		public int? ParentCategory;

		public string Command()
		{
			string whereParentCategory = "";
			if(ParentCategory != null)
			{
				whereParentCategory = $" WHERE {Categories.ParentCategoryId} = {ParentCategory}";
			}

			return $"SELECT {Categories.Id} " +
				$"FROM {Categories.TableName}" +
				whereParentCategory;
		}
	}
}
