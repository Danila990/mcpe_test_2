using DataBase.Commands;
using DataBase.DataBase.ColumnsNames;
using System.Text;

namespace DataBase.DataBase.Commands.AddonImagesCommands
{
	public struct InsertAddonImagesCommand : ICommand
	{
		public string[] ImageKeys;
		public int AddonId;

		public string Command()
		{
			string commandInString = $"INSERT INTO {AddonImages.TableName}({AddonImages.ImageKey}, {AddonImages.AddonId})" +
				$"VALUES";
			StringBuilder command = new StringBuilder(commandInString);
			foreach(var imageKey in ImageKeys)
			{
				command.Append($"('{imageKey}', {AddonId}),");
			}
			command = command.Remove(command.Length - 1, 1);

			return command.ToString();
		}
	}
}
