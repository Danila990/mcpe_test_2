using Assets.Scripts.Language;
using Assets.Scripts.UI.UIPages;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.UIStates.AddonsListScripts.AddonsData
{
	public class AddonsListCardView : MonoBehaviour
	{
		[field: SerializeField]
		public LocalizedTMPFromDB AddonNameText
		{
			get;
			private set;
		}

		[field: SerializeField]
		public ImageLoader PreviewImage
		{
			get;
			private set;
		}

		[field: SerializeField]
		public Toggle IsFavoriteToggle
		{
			get;
			private set;
		}

		[field: SerializeField] 
		public Button OnCardButton
		{
			get;
			private set;
		}

		private void Awake()
		{
			AddonNameText.LocalizationId = -1;
		}

		public void ToBaseState()
		{
			OnCardButton.onClick.RemoveAllListeners();
			AddonNameText.LocalizationId = -1;
			PreviewImage.Cancel();
			PreviewImage.SetLoadingState();
		}
	}
}
