using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Assets.Scripts.DataBase.Commands.LocalizationCommands;
using Assets.Scripts.DataBase;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Assets.Scripts.Language
{
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class LocalizedTMPFromDB : LocalizedFromDBMonoBehaviour
	{
		private TextMeshProUGUI _text;
		private CancellationTokenSource _cancellationToken;

		private void Awake()
		{
			_text = GetComponent<TextMeshProUGUI>();
		}

		public override int LocalizationId
		{
			set
			{
				CancelLocalizationLoading();
				base.LocalizationId = value;
				_text.text = "";
				UpdateLocale(LocalizationSettings.SelectedLocale);
			}
		}

		private void OnDestroy()
		{
			CancelLocalizationLoading();
		}

		public override async void UpdateLocale(Locale locale)
		{
			if(LocalizationId < 0)
			{
				return;
			}

			CancellationTokenSource cancellationToken = new CancellationTokenSource();
			_cancellationToken = cancellationToken;
			CancellationToken token = cancellationToken.Token;
			try
			{
				var localization = await Load(LocalizationId, locale, token);
				token.ThrowIfCancellationRequested();
				_text.text = localization;
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

		private void CancelLocalizationLoading()
		{
			_cancellationToken?.Cancel();
		}

		private async Task<string> Load(int localizationId, Locale locale, CancellationToken token)
		{
			var commandExecuter = DataBaseExecuters.Instance.ObjectsListCommandExecuter;
			var command = new SelectLocalizationCommand();
			command.LocalizationId = localizationId;
			command.LocaleCode = locale.Identifier.Code;
			using(DbDataReader reader = await commandExecuter.ExecuteReader(command, token: token).ConfigureAwait(false))
			{
				while(reader.Read())
				{
					return reader.GetString(0);
				}
			}

			throw new KeyNotFoundException("Данных не оказалось");
		}
	}
}