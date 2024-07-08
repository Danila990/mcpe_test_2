using System;
using UnityEngine;

public class OnRectTransformSizeChangedNotifier : MonoBehaviour
{
	public event Action OnSizeChanged;

	private void OnRectTransformDimensionsChange()
	{
		OnSizeChanged?.Invoke();
	}
}