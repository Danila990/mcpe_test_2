using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using System.Threading;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Scripts.UI
{
	public static class SpriteLoader
	{
		private static readonly Vector2 _centerPivot = new Vector2(0.5f, 0.5f);

		public static async Task<Sprite> LoadSpriteFromFile(string path, CancellationToken token)
		{
			var texture = await LoadTextureFromFile(path, token);
			var result = Sprite.Create(texture, new Rect(0, 0, texture.width,
				texture.height), _centerPivot, 100, 0, SpriteMeshType.FullRect);
			return result;
		}

#if UNITY_EDITOR
		public static Sprite LoadSpriteToResources(string sourcePath, string destFolderPathInResources)
		{
			if(!File.Exists(sourcePath))
			{
				Debug.LogError($"{sourcePath} missing image");
				return null;
			}

			string fileName = Path.GetFileName(sourcePath);
			string destFilePath = Path.Combine(Application.dataPath, "Resources", destFolderPathInResources, fileName);
			File.Copy(sourcePath, destFilePath, true);
			AssetDatabase.Refresh();
			string pathToFileInResources = Path.Combine(destFolderPathInResources, Path.GetFileNameWithoutExtension(fileName));
			return Resources.Load<Sprite>(pathToFileInResources);
		}
#endif

		private static async Task<Texture2D> LoadTextureFromFile(string filePath, CancellationToken token)
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			filePath = "jar:file://" + filePath;
#endif
			using(UnityWebRequest request = UnityWebRequestTexture.GetTexture(filePath))
			using(DownloadHandlerTexture textureDownloader = new DownloadHandlerTexture())
			{
				request.downloadHandler = textureDownloader;
				request.SendWebRequest();
				while(!request.isDone)
				{
					await Task.Yield();
					token.ThrowIfCancellationRequested();
				}

				return textureDownloader.texture;
			}
		}
	}
}