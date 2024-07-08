using Scripts.FileLoaders;
using Scripts.FileLoaders.LoadersInterfaces;
using Scripts.UI;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.UI.UIPages
{
	public class ImageLoader : MonoBehaviour
	{
		[SerializeField] private ImageSpriteController _image;
		[SerializeField] private GameObject _loadingState;

		private CancellationTokenSource _cancellationToken;

		private void Awake()
		{
			SetLoadingState();
		}

		private void OnDestroy()
		{
			Cancel();
		}

		public async void Load(string fileKey)
		{
			var cancellationToken = new CancellationTokenSource();
			_cancellationToken = cancellationToken;
			CancellationToken token = _cancellationToken.Token;
			try
			{
				Sprite sprite = await LoadSprite(fileKey, token);
				token.ThrowIfCancellationRequested();
				RemoveLoadingState();
				_image.Sprite = sprite;
			}
			catch(OperationCanceledException)
			{
			}
			finally
			{
				cancellationToken.Dispose();
				if(_cancellationToken == cancellationToken)
				{
					_cancellationToken = null; 
				}
			}
		}

		public void Cancel()
		{
			_cancellationToken?.Cancel();
		}

		public void SetLoadingState()
		{
			_loadingState.SetActive(true);
			_image.Sprite = null;
		}

		private static async Task<Sprite> LoadSprite(string fileKey, CancellationToken token)
		{
			IFileLoader loader = new FileLoader(fileKey);
			loader = new FileLoaderRestartOnError(loader);
			await loader.Load(token);
			if(loader.IsLoaded)
			{
				return await SpriteLoader.LoadSpriteFromFile(loader.PathToFile, token);
			}

			return null;
		}

		private void RemoveLoadingState()
		{
			_loadingState.SetActive(false);
		}
	}
}
