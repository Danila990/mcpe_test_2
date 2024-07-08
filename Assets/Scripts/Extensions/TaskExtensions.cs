using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Extensions
{
	public static class TaskExtensions
	{
		public static async void LogException(this Task task) 
		{
			try
			{
				await task.ConfigureAwait(false);
			}
			catch(Exception ex)
			{
				Debug.LogException(ex);
				AppMetrica.Instance.ReportUnhandledException(ex);
			}
		}
	}
}
