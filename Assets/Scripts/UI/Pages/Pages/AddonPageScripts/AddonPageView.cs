using UnityEngine.UI;
using UnityEngine;
using Assets.Scripts.UI.UIStates.AddonPageScripts.UI.Elements;
using UnityEngine.Localization;
using Assets.Scripts.UI.UIStates.UICore.ElementContainers;
using Assets.Scripts.Language;

namespace Scripts.UI.UIStates.AddonPageScripts
{
	public class AddonPageView : SimplePageView
	{
		[field: SerializeField]
		public LocalizedTMPFromDB HeaderText
		{
			get;
			private set;
		}

		[field: SerializeField]
		public Toggle FavoriteButton
		{
			get;
			private set;
		}

		[field: SerializeField]
		public Button RewardButton
		{
			get;
			private set;
		}

		[field: SerializeField]
		public Button SupportedVersionsButton
		{
			get;
			private set;
		}

		[field: SerializeField]
		public ImagesScrollRect ImagesScrollRect
		{
			get;
			private set;
		}

		[field: SerializeField]
		public LocalizedTMPFromDB DescriptionText
		{
			get;
			private set;
		}

		[field: SerializeField]
		public LocalizedString WatchRewardAdText
		{
			get;
			private set;
		}

		[field: SerializeField]
		public LocalizedString SupportedVersionsText
		{
			get;
			private set;
		}
	}
}
