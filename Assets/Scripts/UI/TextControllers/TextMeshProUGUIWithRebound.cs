using Scripts.UI;
using TMPro;

namespace Assets.Scripts.UI
{
	public class TextMeshProUGUIWithRebound : TextMeshProUGUI
	{
		private bool _needRecalculate = false;

		public override string text
		{
			set
			{
				base.text = value;
				SetDirty();
			}
		}

		private void LateUpdate()
		{
			if(_needRecalculate)
			{
				this.ReboundRectTransformHeightFitText();
				_needRecalculate = false;
			}
		}

		public void SetDirty()
		{
			_needRecalculate = true;
		}

		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			SetDirty();
		}
	}
}
