#if UNITY_EDITOR

using AddonFillConverter.Addons.Converters;
using GeneralData;
using System.IO;
using System.Threading.Tasks;

namespace Assets.Scripts.EditorScripts.AddonConverterScripts
{
	public static class FilesToDataBaseCacher
	{
		/// <summary>
		/// В папке появится файл {FillFileNames.ObjectsListDataBaseFileName} - база данных со всеми файлами внутри папки
		/// </summary>
		public static async Task Cache(AddonData[] addonsData, string fillDirectoryPath)
		{
			string pathToDataBase = Path.Combine(fillDirectoryPath, FillFileNames.ObjectsListDataBaseFileName);
			using(DataBaseActions dbActions = new DataBaseActions(pathToDataBase))
			{
				await dbActions.OpenConnection();
				await dbActions.Create();
				await dbActions.AddAddonPageData(addonsData);
			}
		}
	}
}

#endif