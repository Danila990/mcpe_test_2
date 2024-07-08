using Assets.Scripts.UI.UIPages;
using Scripts.UI.FixedScroll;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.UIStates.AddonPageScripts.UI.Elements
{
	[RequireComponent(typeof(FixedScrollRect))]
	public class ImagesScrollRect : MonoBehaviour
	{
		[SerializeField] private ImageLoader _imagePrefab;

		public FixedScrollRect ScrollRect
		{
			get;
			private set;
		}

		private void Awake()
		{
			ScrollRect = GetComponent<FixedScrollRect>();
		}

		public void LoadImages(IEnumerable<string> pathes)
		{
			StartCoroutine(LoadImagesWithDelay(pathes));
		}

		private IEnumerator LoadImagesWithDelay(IEnumerable<string> pathes)
		{
			foreach(string path in pathes)
			{
				AddAndLoadImage(path);
				yield return null;
			}
		}

		private void AddAndLoadImage(string path)
		{
			ImageLoader createdImage = GameObject.Instantiate(_imagePrefab);
			createdImage.transform.SetParent(ScrollRect.ScrollRect.content, false);
			createdImage.Load(path);
		}
	}
}
