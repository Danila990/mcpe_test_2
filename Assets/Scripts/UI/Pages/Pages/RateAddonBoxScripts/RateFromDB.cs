using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Assets.Scripts.DataBase;
using System.Data.Common;
using Assets.Scripts.DataBase.SavedData.Commands.RatingCommands;

namespace Assets.Scripts.UI.UIPages.Pages.RateAddonBoxScripts
{
	public class RateAddonBoxModel
	{
		private readonly int _addonId;

		public RateAddonBoxModel(int addonId)
		{
			_addonId = addonId;
		}

		public async Task<int?> GetRating(CancellationToken token)
		{
			var commandExecuter = DataBaseExecuters.Instance.SavedDataCommandExecuter;
			var command = new SelectRatingCommand();
			command.AddonId = _addonId;

			using(DbDataReader reader = await commandExecuter.ExecuteReader(command, token: token).ConfigureAwait(false))
			{
				var result = new List<int>();
				while(reader.Read())
				{
					return reader.GetInt32(0);
				}

				return null;
			}
		}

		public async Task SetRating(int rate, CancellationToken token)
		{
			var commandExecuter = DataBaseExecuters.Instance.SavedDataCommandExecuter;
			var command = new InsertRatingCommand();
			command.AddonId = _addonId;
			command.Rate = rate;

			await commandExecuter.ExecuteNonQuery(command, token: token).ConfigureAwait(false);
		}
	}
}
