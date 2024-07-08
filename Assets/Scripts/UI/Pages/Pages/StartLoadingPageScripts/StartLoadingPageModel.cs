using System;
using System.Threading.Tasks;
using Assets.Scripts.UI.UIPages.Pages.StartLoadingPageScripts;
using Scripts.FileLoaders;
using Scripts;
using Scripts.FileLoaders.LoadersInterfaces;
using System.Threading;
using Assets.Scripts.DataBase;
using GeneralData;

namespace Assets.Scripts.UI.Pages.Pages.StartLoadingPageScripts
{
	public class StartLoadingPageModel
	{
		private readonly ExtraDelayWaiter _extraDelayWaiter = new ExtraDelayWaiter();
		private readonly IFileLoader _fileLoader;

		public StartLoadingPageModel()
		{
			_extraDelayWaiter.Progress += OnWaitProgress;
			_fileLoader = new FileLoader(BucketData.ObjectsListFileKey);
			_fileLoader.IsOverwriteFile = true;
			_fileLoader.OnProgressUpdate += OnFileLoaderProgress;
		}

		public event Action<float> OnLoadingProgress;

		/// <summary>
		/// В секундах
		/// </summary>
		public int ExtraDelayToWait 
		{
			get => _extraDelayWaiter.SecondsToWait;
			set => _extraDelayWaiter.SecondsToWait = value;
		}

		public async Task<LoadStatus> Load(CancellationToken token)
		{
			LoadStatus loadResult = await _fileLoader.Load(token);
			token.ThrowIfCancellationRequested();
			if(loadResult == LoadStatus.Success)
			{
				await OpenDataBases();
				await _extraDelayWaiter.Wait(token);
			}

			return loadResult;
		}

		private async Task OpenDataBases()
		{
			string pathToObjectsListDB = FolderToSave.FolderPath + '/' + BucketData.ObjectsListFileKey;
			await DataBaseExecuters.Instance.OpenObjectsListDB(pathToObjectsListDB);
			string pathToSavedDataDB = FolderToSave.FolderPath + '/' + FillFileNames.SavedDataDataBaseFileName;
			await DataBaseExecuters.Instance.OpenSavedDataDB(pathToSavedDataDB);
		}

		private void OnFileLoaderProgress(float value)
		{
			ReportLoadingProgress(value / 2);
		}

		private void OnWaitProgress(float value)
		{
			ReportLoadingProgress(0.5f + value / 2);
		}

		private void ReportLoadingProgress(float value)
		{
			OnLoadingProgress?.Invoke(value);
		}
	}
}
