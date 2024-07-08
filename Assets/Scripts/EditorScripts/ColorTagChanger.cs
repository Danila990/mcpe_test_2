#if UNITY_EDITOR

using EditorScripts;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.EditorScripts
{
	[CreateAssetMenu(fileName = "ColorTagChanger", menuName = "AutoScripts/ColorTagChanger")]
	public class ColorTagChanger : ScriptableObject
	{
		[SerializeField] private List<GameObject> prefabsToChangeColor;
		[SerializeField] private Color color0;
		[SerializeField] private Color color1;
		[SerializeField] private Color color2;
		[SerializeField] private Color color3;

		public void ChangeInPrefabs()
		{
			foreach(var prefab in prefabsToChangeColor)
			{
				ChangeInPrefab(prefab);
			}
		}

		private void ChangeInPrefab(GameObject prefab)
		{
			var colorTags = prefab.transform.GetComponentsInChildren<ColorTag>(true);
			foreach(var colorTag in colorTags)
			{
				Image image = colorTag.GetComponent<Image>();
				Color color = colorTag.ColorNum switch
				{
					0 => color0,
					1 => color1,
					2 => color2,
					3 => color3,

					_ => throw new ArgumentException("Неизвестный цвет")
				};

				Undo.RecordObject(prefab, "Change color");
				image.color = color;
				EditorUtility.SetDirty(prefab);
				PrefabUtility.RecordPrefabInstancePropertyModifications(image);
				UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(prefab.scene);
			}
		}
	}

	[CustomEditor(typeof(ColorTagChanger))]
	public class ColorTagChangerButton : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();
			ColorTagChanger cardCreator = (ColorTagChanger)target;
			if(!GUILayout.Button("Change color in prefabs"))
			{
				return;
			}

			cardCreator.ChangeInPrefabs();
			Debug.Log("ColorTagChanger changed");
		}
	}
}
#endif