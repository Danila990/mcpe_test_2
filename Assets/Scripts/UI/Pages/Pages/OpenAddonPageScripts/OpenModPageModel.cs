using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Assets.Scripts.DataBase.Commands.AddonFilesCommands;
using Assets.Scripts.DataBase;
using Scripts.FileLoaders;
using Scripts.AddonData.AddonOpen;

namespace Assets.Scripts.UI.Pages.Pages.OpenAddonPageScripts
{
	public class OpenModPageModel
	{
		private readonly int _addonId;

		private string _pathToAddon;

		public OpenModPageModel(int addonId)
		{
			_addonId = addonId;
		}

		public event Action<float> OnProgressUpdate;

		public OpenAddonResult OpenAddon()
		{
			return AddonOpener.OpenAddon(_pathToAddon);
		}

		public async Task<LoadStatus> LoadAddon(CancellationToken token)
		{
			_pathToAddon = null;
			string addonKey = await GetAddonKey(token);
			var addonLoader = new FileLoader(addonKey);
			addonLoader.OnProgressUpdate += (progress) => OnProgressUpdate?.Invoke(progress);
			LoadStatus result = await addonLoader.Load(token);
			_pathToAddon = result == LoadStatus.Success ? addonLoader.PathToFile : null;
			return result;
		}

		private async Task<string> GetAddonKey(CancellationToken token)
		{
			var commandExecuter = DataBaseExecuters.Instance.ObjectsListCommandExecuter;
			var command = new SelectAddonFilesCommand();
			command.AddonId = _addonId;
			using(DbDataReader reader = await commandExecuter.ExecuteReader(command, token: token).ConfigureAwait(false))
			{
				while(reader.Read())
				{
					return reader.GetString(0);
				}
			}

			throw new KeyNotFoundException("Данных не оказалось");
		}
	}
}
