#if UNITY_EDITOR

using Assets.Scripts.Extensions;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace EditorScripts
{
	[CreateAssetMenu(fileName = "IconsAutoCopy", menuName = "AutoScripts/IconsAutoCopy")]
	public class IconsAutoCopy : ScriptableObject
	{
		[SerializeField] private string pathToIcons;

		public void ImportSpritesToProject()
		{
			string iconsFolderName = Path.GetFileName(pathToIcons);
			string destFolder = Path.Combine(Application.dataPath, "Resources", iconsFolderName);
			FileExtensions.CopyDirectory(pathToIcons, destFolder, true);
			AssetDatabase.Refresh();
		}
	}
	
	[CustomEditor(typeof(IconsAutoCopy)), CanEditMultipleObjects]
	public class IconsAutoCopyButton : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();
			IconsAutoCopy iconsAutoSetter = (IconsAutoCopy)target;
			if(!GUILayout.Button("Copy icons to resources"))
			{
				return;
			}

			Undo.RecordObject(iconsAutoSetter, "icons updated");
			iconsAutoSetter.ImportSpritesToProject();
			Debug.Log("iconsAutoSetter updated");
		}
	}
}
#endif