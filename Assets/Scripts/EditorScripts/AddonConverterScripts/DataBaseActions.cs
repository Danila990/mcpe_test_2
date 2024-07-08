#if UNITY_EDITOR

using DataBase.DataBase.ColumnsNames;
using DataBase.DataBase.Commands.AddonFilesCommands;
using DataBase.DataBase.Commands.AddonImagesCommands;
using DataBase.DataBase.Commands.AddonsCategoriesCommands;
using DataBase.DataBase.Commands.AddonsCommands;
using DataBase.DataBase.Commands.CategoriesCommands;
using DataBase.DataBase.Commands.DataBaseCommands;
using DataBase.DataBase.Commands.LocalizationCommands;
using DataBase.DataBase.Commands.SeedsCommands;
using DataBase.DataBase.Commands.SkinImagesCommands;
using DataBase.DataBase.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using AddonFillConverter.Addons.Converters;
using AddonFillConverter.UnityScripts;
using DataBase.Commands;
using BucketObjectsListUpdater.DB.DBExecuters;
using System.Linq;

namespace Assets.Scripts.EditorScripts.AddonConverterScripts
{
	public class DataBaseActions : IDisposable
	{
		private readonly DataBaseCommandExecuter _dataBaseExecuter;

		public DataBaseActions(string pathToDataBase)
		{
			_dataBaseExecuter = new DataBaseCommandExecuter(pathToDataBase);
		}

		public void Dispose()
		{
			_dataBaseExecuter.Dispose();
		}

		public Task OpenConnection()
		{
			return _dataBaseExecuter.OpenConnection();
		}

		public Task Create()
		{
			return CreateTables();
		}

		public async Task AddAddonPageData(params AddonData[] addonsPageData)
		{
			foreach(var addonPageData in addonsPageData)
			{
				var rawAddonId = await AddDataToAddonsTable(addonPageData);
				int addonId = Convert.ToInt32(rawAddonId);
				await AddDataToAddonsImagesTable(addonId, addonPageData.ImagePathes);
				await AddDataToAddonFilesTable(addonId, addonPageData.AddonPath);
				if(addonPageData.Categories != null && addonPageData.Categories.Any())
				{
					await AddDataToAddonsCategoriesTable(addonId, addonPageData.Categories);
				}
			}
		}

		private async Task CreateTables()
		{
			await _dataBaseExecuter.ExecuteNonQuery(new DropTableCommand() { TableName = Seeds.TableName });
			await _dataBaseExecuter.ExecuteNonQuery(new DropTableCommand() { TableName = SkinImages.TableName });
			await _dataBaseExecuter.ExecuteNonQuery(new DropTableCommand() { TableName = AddonFiles.TableName });
			await _dataBaseExecuter.ExecuteNonQuery(new DropTableCommand() { TableName = AddonImages.TableName });
			await _dataBaseExecuter.ExecuteNonQuery(new DropTableCommand() { TableName = AddonsCategories.TableName });
			await _dataBaseExecuter.ExecuteNonQuery(new DropTableCommand() { TableName = Addons.TableName });
			await _dataBaseExecuter.ExecuteNonQuery(new DropTableCommand() { TableName = Categories.TableName });
			await _dataBaseExecuter.ExecuteNonQuery(new DropTableCommand() { TableName = Localization.TableName });

			await _dataBaseExecuter.ExecuteNonQuery(new CreateLocalizationCommand());
			await _dataBaseExecuter.ExecuteNonQuery(new CreateCategoriesCommand());
			await _dataBaseExecuter.ExecuteNonQuery(new CreateAddonsCommand());
			await _dataBaseExecuter.ExecuteNonQuery(new CreateAddonsCategoriesCommand());
			await _dataBaseExecuter.ExecuteNonQuery(new CreateAddonImagesCommand());
			await _dataBaseExecuter.ExecuteNonQuery(new CreateAddonFilesCommand());
			await _dataBaseExecuter.ExecuteNonQuery(new CreateSkinImagesCommand());
			await _dataBaseExecuter.ExecuteNonQuery(new CreateSeedsCommand());
		}

		/// <returns>Id добавленного addon'a</returns>
		private async Task<object> AddDataToAddonsTable(AddonData addonData)
		{
			var rawAddonNameLocalizationId = await AddLocalization(addonData.AddonName);
			int addonNameLocalizationId = Convert.ToInt32(rawAddonNameLocalizationId);
			var rawDescriptionLocalizationId = await AddLocalization(addonData.Description);
			int descrtiptionLocalizationId = Convert.ToInt32(rawDescriptionLocalizationId);
			ICommand insertAddonsCommand = new InsertAddonsCommand()
			{
				AddonNameLocalizationId = addonNameLocalizationId,
				PreviewImageKey = addonData.PreviewImagePath,
				DescrtiptionLocalizationId = descrtiptionLocalizationId,
				Versions = addonData.Versions,
			};

			insertAddonsCommand = new SeveralCommand(insertAddonsCommand, new LastInsertRowIdCommand());
			return await _dataBaseExecuter.ExecuteScalar(insertAddonsCommand);
		}

		private async Task AddDataToAddonsImagesTable(int addonId, string[] imageKeys)
		{
			var command = new InsertAddonImagesCommand();
			command.ImageKeys = imageKeys;
			command.AddonId = addonId;
			await _dataBaseExecuter.ExecuteNonQuery(command);
		}

		private async Task AddDataToAddonFilesTable(int addonId, string addonKey)
		{
			var command = new InsertAddonFilesCommand();
			command.AddonId = addonId;
			command.AddonKey = addonKey;
			await _dataBaseExecuter.ExecuteNonQuery(command);
		}

		private async Task AddDataToAddonsCategoriesTable(int addonId, int[] categories)
		{
			var command = new InsertAddonsCategoriesCommand();
			command.AddonId = addonId;
			command.Categories = categories;
			await _dataBaseExecuter.ExecuteNonQuery(command);
		}

		private async Task<object> AddLocalization(List<LocalizedString> localization)
		{
			localization.Replace("'", "''");
			ICommand insertLocalizationCommand = new InsertLocalizationCommand()
			{
				EnLocale = localization.Find("en"),
				FrLocale = localization.Find("fr"),
				DeLocale = localization.Find("de"),
				IdLocale = localization.Find("id"),
				PlLocale = localization.Find("pl"),
				PtLocale = localization.Find("pt"),
				RuLocale = localization.Find("ru"),
				EsLocale = localization.Find("es"),
				ItLocale = localization.Find("it"),
				TrLocale = localization.Find("tr"),
				UkLocale = localization.Find("uk"),
			};

			var commandsToCombine = new List<ICommand>()
			{
				insertLocalizationCommand,
				new LastInsertRowIdCommand()
			};

			insertLocalizationCommand = new SeveralCommand()
			{
				Commands = commandsToCombine
			};

			return await _dataBaseExecuter.ExecuteScalar(insertLocalizationCommand);
		}
	}
}

#endif