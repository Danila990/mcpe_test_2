#if UNITY_EDITOR

using AddonFillConverter.UnityScripts;
using System.Collections.Generic;

namespace Assets.Scripts.EditorScripts.AddonConverterScripts
{
	public static class LocalizedStringExtensions
	{
		public static string Find(this List<LocalizedString> localizedStrings, string locale)
		{
			var localizedString = localizedStrings.Find(localizedSctring => localizedSctring.Language == locale);
			return localizedString == null ? null : localizedString.Item;
		}

		public static void Replace(this List<LocalizedString> localizedStrings, string oldString, string newString)
		{
			foreach(var localizedString in localizedStrings)
			{
				localizedString.Item = localizedString.Item.Replace(oldString, newString);
			}
		}
	}
}

#endif