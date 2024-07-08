using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.UI.LoadingElements
{
	public class HorizontalLoadingElement : MonoBehaviour
	{
		[SerializeField] private float _cooldown = 0.5f;

		private CancellationTokenSource _cancellationToken;

		private async void Start()
		{
			_cancellationToken = new CancellationTokenSource();
			try
			{
				await SwipeChilds(_cancellationToken.Token);
			}
			catch(OperationCanceledException)
			{

			}
			finally
			{
				_cancellationToken.Dispose();
				_cancellationToken = null;
			}
		}

		private void OnDestroy()
		{
			_cancellationToken.Cancel();
		}

		private async Task SwipeChilds(CancellationToken token)
		{
			while(true)
			{
				MoveLastChildToFirst();
				await Task.Delay((int)(1000 * _cooldown));
				token.ThrowIfCancellationRequested();
			}
		}

		private void MoveLastChildToFirst()
		{
			int childsCount = transform.childCount;
			Transform lastChild = transform.GetChild(childsCount - 1);
			lastChild.SetAsFirstSibling();
		}
	}
}
