using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.UIPages.Pages.OpenAddonPageScripts.UI
{
	public class ButtonWithDisabler : MonoBehaviour
	{
		[SerializeField]
		private GameObject Disabler;

		public Button Button
		{
			get;
			private set;
		}

		private void Awake()
		{
			Button = GetComponent<Button>();
		}

		public void ActivateDisabler()
		{
			Disabler.SetActive(true);
		}

		public void DeactivateDisabler()
		{
			Disabler.SetActive(false);
		}
	}
}
