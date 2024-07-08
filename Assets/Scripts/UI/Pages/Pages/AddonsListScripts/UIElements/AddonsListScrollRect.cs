using Scripts.UI.UIStates.AddonsListScripts.AddonsData;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.UI.Scrolls.DynamicScroll.New;

namespace Scripts.UI.UIStates.AddonsListScripts.ScrollRects
{
	[RequireComponent(typeof(ScrollRect), typeof(ScrollRectLastElementOnScreenChecker))]
	public class AddonsListScrollRect : MonoBehaviour
	{
		protected readonly List<int> _dataSource = new List<int>();

		protected LinkedList<AddonsListCard> _cardsInUse = new LinkedList<AddonsListCard>();

		public ScrollRect ScrollRect
		{
			get;
			private set;
		}

		public event Action OnNeedAddAddons;

		private void Awake()
		{
			ScrollRect = GetComponent<ScrollRect>();
			var addLastChecker = GetComponent<ScrollRectLastElementOnScreenChecker>();
			addLastChecker.LastElementOnScreenCallback += NotifyNeedAddAddons;
		}

		public void AddAddons(IEnumerable<int> addonsId)
		{
			_dataSource.AddRange(addonsId);
			foreach(var addonId in addonsId)
			{
				AddLast(addonId);
			}
		}

		public virtual void ToBaseState()
		{
			ClearCardsInUse();
			_dataSource.Clear();
			ScrollRect.content.anchoredPosition = new Vector2(ScrollRect.content.anchoredPosition.x, 0);
			ScrollRect.velocity = Vector2.zero;
		}

		public void ClearCardsInUse()
		{
			foreach(var card in _cardsInUse)
			{
				card.Dispose();
			}

			_cardsInUse.Clear();
		}

		protected virtual void AddLast(int addonId)
		{
			AddonsListCard addonCard = new AddonsListCard(addonId);
			addonCard.SetParent(ScrollRect.content);
			_cardsInUse.AddLast(addonCard);
		}

		private void NotifyNeedAddAddons()
		{
			OnNeedAddAddons?.Invoke();
		}
	}
}
