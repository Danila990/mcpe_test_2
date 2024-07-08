using Assets.Scripts.UI.UIPages.CanvasPoolScripts;
using Scripts.ObjectPoolPattern;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.UI.UIStates.CanvasPoolScripts
{
	public class CanvasPool : MonoBehaviour, IPool<CanvasGameObject>
	{
		private readonly LinkedList<CanvasGameObject> _freeCanvases = new LinkedList<CanvasGameObject>();
		private readonly SortedSet<int> _sortingOrders = new SortedSet<int>();

		public ICreation<CanvasGameObject> Creator
		{
			get; set;
		}

		private void Awake()
		{
			Creator = GetComponent<ICreation<CanvasGameObject>>();
			CanvasForPage.Pool = this;
		}

		public CanvasGameObject GetFree()
		{
			CanvasGameObject result;
			if(_freeCanvases.Any())
			{
				result = _freeCanvases.First();
				_freeCanvases.RemoveFirst();
			}
			else
			{
				result = Creator.Create();
			}

			int newSortingOrder = _sortingOrders.LastOrDefault() + 1;
			result.Canvas.sortingOrder = newSortingOrder;
			_sortingOrders.Add(newSortingOrder);
			return result;
		}

		public void SetFree(CanvasGameObject toSetFree)
		{
			_freeCanvases.AddLast(toSetFree);
			_sortingOrders.Remove(toSetFree.Canvas.sortingOrder);
		}
	}
}
