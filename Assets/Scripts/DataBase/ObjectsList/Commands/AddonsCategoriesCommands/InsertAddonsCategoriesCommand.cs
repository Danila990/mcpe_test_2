using DataBase.Commands;
using DataBase.DataBase.ColumnsNames;
using System.Text;

namespace DataBase.DataBase.Commands.AddonsCategoriesCommands
{
	public struct InsertAddonsCategoriesCommand : ICommand
	{
		public int AddonId;
		public int[] Categories;

		public string Command()
		{
			StringBuilder valuesToAdd = new StringBuilder();
			foreach(int category in Categories)
			{
				string valueToAdd = $"({AddonId}, {category}),";
				valuesToAdd.Append(valueToAdd);
			}
			valuesToAdd = valuesToAdd.Remove(valuesToAdd.Length - 1, 1);

			return $"INSERT INTO {AddonsCategories.TableName}({AddonsCategories.AddonId}, " +
				$"{AddonsCategories.CategoryId})" +
				$"VALUES{valuesToAdd}";
		}
	}
}
