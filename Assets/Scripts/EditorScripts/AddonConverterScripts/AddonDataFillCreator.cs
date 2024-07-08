#if UNITY_EDITOR

using AddonFillConverter;
using AddonFillConverter.Addons.Converters;
using Assets.Scripts.EditorScripts.AddonConverterScripts;
using Scripts;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace EditorScripts.AddonPageScripts
{
	[CreateAssetMenu(fileName = "AddonDataFillCreator", menuName = "AutoScripts/AddonDataFillCreator")]
	public class AddonDataFillCreator : ScriptableObject
	{
		[SerializeField] private string pathToAddonsFolder;

		public async void FillAddonDataFiller()
		{
			string convertedFillDirectoryPath = Path.Combine(Application.dataPath, "StreamingAssets", 
				BucketData.AddonsFolderName);
			if(Directory.Exists(convertedFillDirectoryPath))
			{
				Directory.Delete(convertedFillDirectoryPath, true);
			}

			FillConverter fillConverter = new FillConverter(pathToAddonsFolder,
				convertedFillDirectoryPath, BucketData.AddonsFolderName);
			AddonData[] addonsPageDataForDB = fillConverter.ConvertAddons();
			await FilesToDataBaseCacher.Cache(addonsPageDataForDB, convertedFillDirectoryPath);
			AssetDatabase.Refresh();
		}
	}

	[CustomEditor(typeof(AddonDataFillCreator))]
	public class AddonDataFillerButton : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();
			AddonDataFillCreator cardCreator = (AddonDataFillCreator)target;
			if(!GUILayout.Button("Set Addon Page Data From Directory"))
			{
				return;
			}

			cardCreator.FillAddonDataFiller();
			Debug.Log("CardsDataFillerInAddonPage filled");
		}
	}
}
#endif