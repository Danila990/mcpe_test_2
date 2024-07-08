using System.Collections.Generic;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Assets.Scripts.DataBase.Commands.AddonsCommands;
using Assets.Scripts.DataBase;
using Assets.Scripts.FillData.Addons;
using Assets.Scripts.UI.Pages.Pages.AddonPageScripts;

namespace Assets.Scripts.UI.UIPages.Pages.AddonsListScripts.UIElements.Card
{
	public class AddonsListCardModel
	{
		public readonly int AddonId;

		private readonly FavoriteAddonController _favoriteAddonController;

		public AddonsListCardModel(int addonId)
		{
			AddonId = addonId;
			_favoriteAddonController = new FavoriteAddonController(addonId);
		}

		public async Task<AddonPreviewData> GetPreviewData(CancellationToken token)
		{
			var commandExecuter = DataBaseExecuters.Instance.ObjectsListCommandExecuter;
			var command = new SelectAddonsPreviewDataCommand();
			command.AddonId = AddonId;
			using(DbDataReader reader = await commandExecuter.ExecuteReader(command, token: token).ConfigureAwait(false))
			{
				while(reader.Read())
				{
					AddonPreviewData result = new AddonPreviewData();
					result.AddonNameLocalizationId = reader.GetInt32(0);
					result.PreviewImageKey = reader.GetString(1);
					return result;
				}
			}

			throw new KeyNotFoundException("Данных не оказалось");
		}

		public Task<bool> IsAddonFavorite(CancellationToken token) 
		{
			return _favoriteAddonController.IsAddonFavorite(token);
		}
	}
}
