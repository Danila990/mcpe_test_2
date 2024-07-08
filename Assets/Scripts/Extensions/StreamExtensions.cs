using System.IO;
using System.Threading;
using System;
using System.Threading.Tasks;

namespace Assets.Scripts
{
	public static class StreamExtensions
	{
		public static async Task CopyToAsyncWithProgress(this Stream source, Stream destination, int bufferSize = 81920,
			CancellationToken cancellationToken = default, Action<long> onProgress = null)
		{
			byte[] buffer = new byte[bufferSize];
			long totalBytesRead = 0;
			while(true)
			{
				int bytesRead = await source.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false);
				cancellationToken.ThrowIfCancellationRequested();
				if(bytesRead == 0)
				{
					break;
				}

				await destination.WriteAsync(buffer, 0, bytesRead, cancellationToken).ConfigureAwait(false);
				cancellationToken.ThrowIfCancellationRequested();
				totalBytesRead += bytesRead;
				onProgress?.Invoke(totalBytesRead);
			}
		}
	}
}
