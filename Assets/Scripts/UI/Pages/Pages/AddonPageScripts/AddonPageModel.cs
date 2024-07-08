using System.Collections.Generic;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Assets.Scripts.DataBase.Commands.AddonImagesCommands;
using Assets.Scripts.DataBase.Commands.AddonsCommands;
using Assets.Scripts.DataBase;
using Assets.Scripts.FillData.Addons;
using Scripts.AddonData;
using UnityEngine;
using Assets.Scripts.UI.Pages.Pages.AddonPageScripts;

namespace Assets.Scripts.UI.UIPages.Pages.AddonPageScripts
{
	public class AddonPageModel
	{
		public readonly int AddonId;
		public readonly RewardAdType RewardAdType;

		private readonly FavoriteAddonController _favoriteAddonController;

		public AddonPageModel(int addonId)
		{
			AddonId = addonId;
			RewardAdType = Random.Range(0, 100) < 50 ? RewardAdType.RewardAd : RewardAdType.InterAd;
			_favoriteAddonController = new FavoriteAddonController(addonId);
		}

		public async Task<AddonData> GetAddonData(CancellationToken token)
		{
			var command = new SelectAddonTextCommand();
			command.AddonId = AddonId;
			var commandExecuter = DataBaseExecuters.Instance.ObjectsListCommandExecuter;
			using(DbDataReader reader = await commandExecuter.ExecuteReader(command, token: token).ConfigureAwait(false))
			{
				while(reader.Read())
				{
					AddonData result = new AddonData();
					result.AddonNameLocalizationId = reader.GetInt32(0);
					result.DescriptionLocalizationId = reader.GetInt32(1);
					if(!reader.IsDBNull(2))
					{
						result.Versions = reader.GetString(2);
					}

					return result;
				}
			}

			throw new KeyNotFoundException("Данных не оказалось");
		}

		public async Task<List<string>> GetImageKeys(CancellationToken token)
		{
			var command = new SelectAddonImages();
			command.AddonId = AddonId;
			var commandExecuter = DataBaseExecuters.Instance.ObjectsListCommandExecuter;
			using(DbDataReader reader = await commandExecuter.ExecuteReader(command, token: token).ConfigureAwait(false))
			{
				List<string> imageKeys = new List<string>();
				while(await Task.Run(reader.Read, token).ConfigureAwait(false))
				{
					imageKeys.Add(reader.GetString(0));
				}

				if(imageKeys.Count == 0)
				{
					throw new KeyNotFoundException("Данных не оказалось");
				}

				return imageKeys;
			}
		}

		public Task<bool> IsAddonFavorite(CancellationToken token)
		{
			return _favoriteAddonController.IsAddonFavorite(token);
		}

		public Task SetAddonFavirite(bool isFavorite, CancellationToken token) 
		{
			return _favoriteAddonController.SetAddonFavirite(isFavorite, token);
		}
	}
}
