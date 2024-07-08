#if UNITY_EDITOR
using UnityEngine;

namespace EditorScripts
{
	public class ColorTag : MonoBehaviour
	{
		[field: SerializeField, Range(0, 3)]
		public int ColorNum
		{
			get;
			private set;
		}
	}
}
#endif