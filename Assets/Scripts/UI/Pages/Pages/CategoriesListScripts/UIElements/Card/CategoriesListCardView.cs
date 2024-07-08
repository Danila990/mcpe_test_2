using Assets.Scripts.Language;
using Assets.Scripts.UI.UIPages;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.UIStates.CategoriesListScripts.Card.UI
{
	public class CategoriesListCardView : MonoBehaviour
	{
		[field: SerializeField]
		public LocalizedTMPFromDB CategoryNameText
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
		public Toggle CardButton 
		{
			get;
			private set;
		}

		private void Awake()
		{
			CategoryNameText.LocalizationId = -1;
		}

		public void ToBaseState()
		{
			CardButton.group = null;
			CardButton.onValueChanged.RemoveAllListeners();
			CategoryNameText.LocalizationId = -1;
			PreviewImage.Cancel();
			PreviewImage.SetLoadingState();
		}
	}
}
