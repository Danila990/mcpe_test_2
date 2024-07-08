using DataBase.Commands;
using DataBase.DataBase.ColumnsNames;

namespace DataBase.DataBase.Commands.AddonFilesCommands
{
	public struct InsertAddonFilesCommand : ICommand
	{
		public int AddonId;
		public string AddonKey;

		public string Command()
		{
			return $"INSERT INTO {AddonFiles.TableName}({AddonFiles.AddonId}, {AddonFiles.AddonKey}) "+
				$"VALUES('{AddonId}', '{AddonKey}')";
		}
	}
}
