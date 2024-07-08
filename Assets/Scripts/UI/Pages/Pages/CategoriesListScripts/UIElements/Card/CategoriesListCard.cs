using Scripts.ObjectPoolPattern;
using System.Threading;
using System;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.UI.Pages.Pages.CategoriesListScripts.UIElements.Card;

namespace Assets.Scripts.UI.UIStates.CategoriesListScripts.Card.UI
{
	public class CategoriesListCard
	{
		public static IPool<CategoriesListCardView> Pool;

		private readonly CategoriesListCardView _view;
		private readonly CategoriesListCardModel _model;

		private CancellationTokenSource _cancellationToken;

		public CategoriesListCard(int categoryId)
		{
			_view = Pool.GetFree();
			_view.CardButton.onValueChanged.AddListener(CardClicked);
			_model = new CategoriesListCardModel(categoryId);
			LoadAndSetPreviewAddonData();
		}

		public event Action<int> OnCardClick;

		public void Dispose()
		{
			_cancellationToken?.Cancel();
			Pool.SetFree(_view);
		}

		public void SetParentTransform(Transform scrollRect) 
		{
			_view.gameObject.transform.SetParent(scrollRect, false);
		}

		public void SetToggleGroup(ToggleGroup group)
		{
			_view.CardButton.group = group;
		}

		private async void LoadAndSetPreviewAddonData()
		{
			_cancellationToken = new CancellationTokenSource();
			CancellationToken token = _cancellationToken.Token;
			try
			{
				var categoryData = await _model.GetCategoryData(token);
				token.ThrowIfCancellationRequested();
				_view.CategoryNameText.LocalizationId = categoryData.CategoryNameLocalizationId;
				_view.PreviewImage?.Load(categoryData.PreviewImageKey);
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

		private void CardClicked(bool isOn)
		{
			if(!isOn) 
			{
				return;			
			}

			OnCardClick?.Invoke(_model.CategoryId);
		}
	}
}
