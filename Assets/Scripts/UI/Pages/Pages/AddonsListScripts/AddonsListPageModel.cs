using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Assets.Scripts.UI.Pages.Pages.AddonsListScripts;

namespace Assets.Scripts.UI.UIPages.Pages.AddonsListScripts
{
	public class AddonsListPageModel
	{
		public readonly int? CategoryId;

		private readonly AddonsIdGetter _addonsIdGetter;

		public AddonsListPageModel(int? categoryId)
		{
			_addonsIdGetter = new AddonsIdGetter(categoryId);
			CategoryId = categoryId;
		}

		public int LoadPerRequest 
		{
			get => _addonsIdGetter.LoadPerRequest;
			set => _addonsIdGetter.LoadPerRequest = value;
		}

		public Task<List<int>> LoadNextAddonsId(CancellationToken token)
		{
			return _addonsIdGetter.LoadNextAddonsId(token);
		}
	}
}
