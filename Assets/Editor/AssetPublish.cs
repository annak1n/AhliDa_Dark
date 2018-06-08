#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;

public class CreateAssetBundles : EditorWindow {

	private string path = "AssetBundles";
	private BuildAssetBundleOptions options = BuildAssetBundleOptions.UncompressedAssetBundle;
	private BuildTarget platform = BuildTarget.StandaloneWindows;


	[MenuItem("Publish/Build AssetBundles")]
	public static void ShowWindow()
	{
		EditorWindow.GetWindow(typeof(CreateAssetBundles));
	}

	void OnGUI()
	{
		GUILayout.Label("AssetBundles Settings", EditorStyles.boldLabel);

		EditorGUILayout.Separator();
		path = EditorGUILayout.TextField("Output Path:", path);

		EditorGUILayout.Separator();
		GUILayout.BeginHorizontal();
		GUILayout.Label("Options:");
		options = (BuildAssetBundleOptions)EditorGUILayout.EnumPopup(options);
		GUILayout.EndHorizontal();

		EditorGUILayout.Separator();
		GUILayout.BeginHorizontal();
		GUILayout.Label("Platform:");
		platform = (BuildTarget)EditorGUILayout.EnumPopup(platform);
		GUILayout.EndHorizontal();

		EditorGUILayout.Separator();
		if(GUILayout.Button("Create AssetBundles"))
		{
			if(!Directory.Exists(path)) Directory.CreateDirectory(path);
			BuildPipeline.BuildAssetBundles(path, options, platform);
			EditorUtility.RevealInFinder(path);
		}
	}
}
#endif