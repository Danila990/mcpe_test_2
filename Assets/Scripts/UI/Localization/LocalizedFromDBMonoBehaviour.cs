using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Assets.Scripts.Language
{
	public abstract class LocalizedFromDBMonoBehaviour : MonoBehaviour
	{
		protected int _localizationId = -1;
		private Locale _onDisableLocale;

		public virtual int LocalizationId
		{
			get
			{
				return _localizationId;
			}

			set
			{
				_localizationId = value;
			}
		}

		protected virtual void OnEnable()
		{
			LocalizationSettings.SelectedLocaleChanged += UpdateLocale;
			if(_onDisableLocale != LocalizationSettings.SelectedLocale)
			{
				UpdateLocale(LocalizationSettings.SelectedLocale);
			}
		}

		protected virtual void OnDisable()
		{
			LocalizationSettings.SelectedLocaleChanged -= UpdateLocale;
			_onDisableLocale = LocalizationSettings.SelectedLocale;
		}

		public abstract void UpdateLocale(Locale locale);
	}
}
