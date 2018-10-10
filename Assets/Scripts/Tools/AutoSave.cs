using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public class AutoSave
{
	static AutoSave()
	{
		EditorApplication.playmodeStateChanged = () =>
		{
			if (!EditorApplication.isPlayingOrWillChangePlaymode || EditorApplication.isPlaying) return;
			Debug.Log(string.Format("Auto-Saving scene before entering play mode : {0}", SceneManager.GetActiveScene().name));
			AssetDatabase.SaveAssets();
			EditorSceneManager.SaveOpenScenes();
		};
	}

}
