using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
	public static class TransformExtensions
	{
		public static Transform[] GetChilds(this Transform transform)
		{
			int childCount = transform.childCount;
			var childs = new Transform[childCount];
			for(int i = 0; i < childCount; i++)
			{
				childs[i] = transform.GetChild(i);
			}

			return childs;
		}

		public static void DestroyAllChilds(this Transform transform)
		{
			int childCount = transform.childCount;
			for(int i = 0; i < childCount; i++)
			{
				var childToDestroy = transform.GetChild(i);
				GameObject.Destroy(childToDestroy.gameObject);
			}
		}

		public static void SetAsPrenultimateSibling(this Transform transform)
		{
			int childsCount = transform.parent == null ? 
				SceneManager.GetActiveScene().rootCount : transform.parent.childCount;
			if(childsCount < 2)
			{
				return;
			}

			var prenultimateSiblingIndex = childsCount - 2;
			transform.SetSiblingIndex(prenultimateSiblingIndex);
		}
	}
}
