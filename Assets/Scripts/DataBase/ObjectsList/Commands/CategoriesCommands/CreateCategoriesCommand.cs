using DataBase.Commands;
using DataBase.DataBase.ColumnsNames;

namespace DataBase.DataBase.Commands.CategoriesCommands
{
	public struct CreateCategoriesCommand : ICommand
	{
		public string Command()
		{
			return $"CREATE TABLE IF NOT EXISTS {Categories.TableName}" +
			$"({Categories.Id} INTEGER PRIMARY KEY AUTOINCREMENT," +
			$"{Categories.CategoryNameLocalizationId} INTEGER NOT NULL," +
			$"{Categories.PreviewImageKey} TEXT," +
			$"{Categories.ParentCategoryId} INTEGER," +
			$"FOREIGN KEY ({Categories.CategoryNameLocalizationId})" +
			$"REFERENCES {Localization.TableName} ({Localization.Id}),"+ 
			$"FOREIGN KEY ({Categories.ParentCategoryId})" +
			$"REFERENCES {Categories.TableName} ({Categories.Id}))";
		}
	}
}
