using System.Collections.Generic;
using UnityEngine;

#if ADMOB_ENABLED
using Scripts.AD.Native;
#endif

namespace Scripts.UI.UIStates.AddonsListScripts.ScrollRects
{
	public class AddonsListScrollRectWithNative : AddonsListScrollRect
	{
#if ADMOB_ENABLED
		[SerializeField, Min(1)]
		private int _indexMultipleToAdd;
		[SerializeField]
		private NativeComponentsContainer _nativePrefab;

		private LinkedList<NativeComponentsContainer> _nativeInUse = new LinkedList<NativeComponentsContainer>();

		public override void ToBaseState()
		{
			base.ToBaseState();
			foreach(var native in _nativeInUse)
			{
				GameObject.Destroy(native.gameObject);
			}

			_nativeInUse.Clear();
		}

		protected override void AddLast(int addonId)
		{
			base.AddLast(addonId);
			int addedCardIndex = _cardsInUse.Count - 1;
			if(IsNativeIndex(addedCardIndex))
			{
				var nativeGameObject = GameObject.Instantiate(_nativePrefab);
				_nativeInUse.AddLast(nativeGameObject);
				nativeGameObject.transform.SetParent(ScrollRect.content, false);
			}
		}

		private bool IsNativeIndex(int index)
		{
			return index % _indexMultipleToAdd == 0;
		}
#endif
	}
}
