﻿using Assets.Scripts.UI.UIStates.AddonsListScripts.AddonsData;
using Scripts.ObjectPoolPattern;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.UI.UIStates.AddonsListScripts.AddonsData
{
	public class AddonsListCardPool : MonoBehaviour, IPool<AddonsListCardView>
	{
		private readonly LinkedList<AddonsListCardView> _freeCards =
			new LinkedList<AddonsListCardView>();

		public ICreation<AddonsListCardView> Creator
		{
			get;
			set;
		}

		private void Awake()
		{
			Creator = GetComponent<ICreation<AddonsListCardView>>();
			AddonsListCard.Pool = this;
		}

		public AddonsListCardView GetFree()
		{
			AddonsListCardView result;
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

		public void SetFree(AddonsListCardView view)
		{
			view.ToBaseState();
			view.gameObject.SetActive(false);
			view.transform.SetParent(transform, false);
			_freeCards.AddLast(view);
		}
	}
}