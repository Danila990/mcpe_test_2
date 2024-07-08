#if UNITY_EDITOR

using Assets.Scripts.Extensions;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace EditorScripts
{
	[CreateAssetMenu(fileName = "BackgroundChanger", menuName = "AutoScripts/BackgroundChanger")]
	public class BackgroundChanger : ScriptableObject
	{
		[SerializeField] private string pathToBackground;

		public void ImportSpritesToProject()
		{
			string backgroundsFolderName = Path.GetFileName(pathToBackground);
			string destFolder = Path.Combine(Application.dataPath, "Resources", backgroundsFolderName);
			FileExtensions.CopyDirectory(pathToBackground, destFolder, true);
			AssetDatabase.Refresh();
		}
	}

	[CustomEditor(typeof(BackgroundChanger))]
	public class BackgroundChangerButton : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();
			BackgroundChanger iconsAutoSetter = (BackgroundChanger)target;
			if(!GUILayout.Button("Change background"))
			{
				return;
			}

			iconsAutoSetter.ImportSpritesToProject();
			Debug.Log("background updated");
		}
	}
}
#endif