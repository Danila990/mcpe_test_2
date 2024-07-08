using DataBase.Commands;
using DataBase.DataBase.ColumnsNames;

namespace Assets.Scripts.DataBase.Commands.AddonsCommands
{
	/// <summary>
	/// Возвращает <=Count аддонов c CategoryId
	/// </summary>
	public struct SelectAddonsIdCommand : ICommand
	{
		public int? CategoryId;

		public string Command()
		{
			string whereCategory = "";
			if(CategoryId != null)
			{
				whereCategory = $" JOIN {AddonsCategories.TableName} " +
				$"ON {AddonsCategories.TableName}.{AddonsCategories.AddonId} = {Addons.Id} " +
				$"WHERE {AddonsCategories.TableName}.{AddonsCategories.CategoryId} = {CategoryId}";
			}

			return $"SELECT {Addons.Id} " +
				$"FROM {Addons.TableName}" + whereCategory;
		}
	}
}
