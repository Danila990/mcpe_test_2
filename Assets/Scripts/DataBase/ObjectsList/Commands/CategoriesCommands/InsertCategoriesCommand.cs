using DataBase.Commands;
using DataBase.DataBase.ColumnsNames;

namespace DataBase.DataBase.Commands.CategoriesCommands
{
	public struct InsertCategoriesCommand : ICommand
	{
		public int Id;
		public int CategoryNameLocalizationId;
		public string PreviewImageKey;
		public int? ParentCategoryId;

		public string Command()
		{
			string parentCategoryIdValueInDB = ParentCategoryId == null ? "NULL" : $"{ParentCategoryId}";
			string previewImageKeyValueInDB = PreviewImageKey == null ? "NULL" : $"'{PreviewImageKey}'";

			return $"INSERT INTO {Categories.TableName}({Categories.Id}, {Categories.CategoryNameLocalizationId}, " +
				$"{Categories.PreviewImageKey}, {Categories.ParentCategoryId})" +
				$"VALUES({Id}, '{CategoryNameLocalizationId}', {previewImageKeyValueInDB}, {parentCategoryIdValueInDB})";
		}
	}
}
