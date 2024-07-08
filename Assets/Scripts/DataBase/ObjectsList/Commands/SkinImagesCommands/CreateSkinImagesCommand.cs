using DataBase.Commands;
using DataBase.DataBase.ColumnsNames;

namespace DataBase.DataBase.Commands.SkinImagesCommands
{
	public struct CreateSkinImagesCommand : ICommand
	{
		public string Command()
		{
			return $"CREATE TABLE IF NOT EXISTS {SkinImages.TableName}" +
				$"({SkinImages.AddonId} INTEGER NOT NULL," +
				$"{SkinImages.SkinImageKey} TEXT NOT NULL," +
				$"UNIQUE({SkinImages.AddonId})," +
				$"FOREIGN KEY ({SkinImages.AddonId})" +
				$"REFERENCES {Addons.TableName} ({Addons.Id}))";
		}
	}
}
