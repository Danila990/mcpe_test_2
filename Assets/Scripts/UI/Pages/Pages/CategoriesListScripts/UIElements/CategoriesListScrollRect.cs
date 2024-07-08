using Assets.Scripts.UI.UIStates.CategoriesListScripts.Card.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.UIStates.CategoriesListScripts.UI.ScrollRects
{
	[RequireComponent(typeof(ScrollRect), typeof(ToggleGroup))]
	public class CategoriesListScrollRect : MonoBehaviour
	{
		protected LinkedList<CategoriesListCard> _cardsInUse = new LinkedList<CategoriesListCard>();

		private ScrollRect _scrollRect;
		private ToggleGroup _toggleGroup;

		public event Action<int> OnCardClick;

		private void Awake()
		{
			_scrollRect = GetComponent<ScrollRect>();
			_toggleGroup = GetComponent<ToggleGroup>();
		}

		protected virtual void OnDestroy()
		{
			ClearCardsInUse();
		}

		public void AddCards(IEnumerable<int> categories)
		{
			foreach(var categoryId in categories)
			{
				AddCard(categoryId);
			}
		}

		private void AddCard(int catgoryId)
		{
			CategoriesListCard categoryCard = new CategoriesListCard(catgoryId);
			categoryCard.SetParentTransform(_scrollRect.content);
			categoryCard.SetToggleGroup(_toggleGroup);
			categoryCard.OnCardClick += CardClicked;
			_cardsInUse.AddLast(categoryCard);
		}

		private void ClearCardsInUse()
		{
			foreach(var card in _cardsInUse)
			{
				card.Dispose();
			}
		}

		private void CardClicked(int categoryId) 
		{
			OnCardClick?.Invoke(categoryId);
		}
	}
}
