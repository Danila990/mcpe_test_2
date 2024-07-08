﻿using Scripts.ObjectPoolPattern;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.UI.UIStates.CategoriesListScripts.Card.UI
{
	public class CategoriesListCardPool : MonoBehaviour, IPool<CategoriesListCardView>
	{
		private readonly LinkedList<CategoriesListCardView> _freeCards =
			new LinkedList<CategoriesListCardView>();

		public ICreation<CategoriesListCardView> Creator
		{
			get;
			set;
		}

		private void Awake()
		{
			Creator = GetComponent<ICreation<CategoriesListCardView>>();
			CategoriesListCard.Pool = this;
		}

		public CategoriesListCardView GetFree()
		{
			CategoriesListCardView result = null;
			if(_freeCards.Any())
			{
				result = _freeCards.First();
				_freeCards.Remove(result);
			}
			else
			{
				result = Creator.Create();
			}

			result.gameObject.SetActive(true);
			return result;
		}

		public void SetFree(CategoriesListCardView view)
		{
			view.ToBaseState();
			view.gameObject.SetActive(false);
			view.transform.SetParent(transform, false);
			_freeCards.AddLast(view);
		}
	}
}