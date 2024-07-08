using Assets.Scripts.UI.UIPages.Pages.RateAddonBoxScripts.UI;
using Assets.Scripts.UI.UIStates.UICore.ElementContainers;
using Scripts.ObjectPoolPattern;
using Scripts.UI.UIStates.UICore;
using System;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts.UI.UIPages.Pages.RateAddonBoxScripts
{
	public class RateAddonBox : UIOverlayPage
	{
		public static ICreation<RateAddonBoxView> Creator;

		private readonly RateAddonBoxView _view;
		private readonly RateAddonBoxModel _rateFromDB;

		private CancellationTokenSource _cancellationToken;
		private bool _needToSaveResult = false;

		public RateAddonBox(UISimplePage parent, int addonId) : base(parent)
		{
			_view = Creator.Create();
			_view.Parent = Parent.Transform;
			_view.BackgroundButton.onClick.AddListener(OnEscapePressed);
			_view.CrossButton.onClick.AddListener(OnEscapePressed);
			_rateFromDB = new RateAddonBoxModel(addonId);
			SetRatingFromDB();
		}

		public override void Dispose()
		{
			SaveRating();
			base.Dispose();
			_cancellationToken?.Cancel();
			GameObject.Destroy(_view.gameObject);
		}

		private async void SetRatingFromDB()
		{
			_cancellationToken = new CancellationTokenSource();
			CancellationToken token = _cancellationToken.Token;
			try
			{
				int? rating = await _rateFromDB.GetRating(token);
				token.ThrowIfCancellationRequested();
				if(rating != null)
				{
					_view.RateScroll.SetRate(rating.Value);
				}

				_view.RateScroll.OnRateChanged += (i) => _needToSaveResult = true;
			}
			catch(OperationCanceledException)
			{
			}
			finally
			{
				_cancellationToken.Dispose();
				_cancellationToken = null;
			}
		}

		private async void SaveRating()
		{
			if(!_needToSaveResult)
			{
				return;
			}

			await _rateFromDB.SetRating(_view.RateScroll.Rate, default);
		}

		protected override OverlayPageView GetOverlayPageView()
		{
			return _view;
		}
	}
}
