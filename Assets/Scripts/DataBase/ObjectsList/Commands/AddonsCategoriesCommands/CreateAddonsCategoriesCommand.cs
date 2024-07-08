using DataBase.Commands;
using DataBase.DataBase.ColumnsNames;

namespace DataBase.DataBase.Commands.AddonsCategoriesCommands
{
	public struct CreateAddonsCategoriesCommand : ICommand
	{
		public string Command()
		{
			return $"CREATE TABLE IF NOT EXISTS {AddonsCategories.TableName}" +
				$"({AddonsCategories.AddonId} INTEGER NOT NULL," +
				$"{AddonsCategories.CategoryId} INTEGER NOT NULL," +
				$"UNIQUE({AddonsCategories.AddonId}, {AddonsCategories.CategoryId})," +
				$"FOREIGN KEY ({AddonsCategories.AddonId})" +
				$"REFERENCES {Addons.TableName} ({Addons.Id})," +
				$"FOREIGN KEY ({AddonsCategories.CategoryId})" +
				$"REFERENCES {Categories.TableName} ({Categories.Id}))";
		}
	}
}
