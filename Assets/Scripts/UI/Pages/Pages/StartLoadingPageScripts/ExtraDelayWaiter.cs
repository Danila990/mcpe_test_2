using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.UI.UIPages.Pages.StartLoadingPageScripts
{
	public class ExtraDelayWaiter
	{
		public int SecondsToWait;

		public event Action<float> Progress;

		public async Task Wait(CancellationToken token)
		{
			var secondsToWait = SecondsToWait;
			for(int i = 0; i < secondsToWait; i++)
			{
				Progress?.Invoke(((float)(i + 1)) / secondsToWait);
				await Task.Delay(1000);
				token.ThrowIfCancellationRequested();
				if(!Application.isPlaying) 
				{
					throw new OperationCanceledException();
				}
			}
		}
	}
}
