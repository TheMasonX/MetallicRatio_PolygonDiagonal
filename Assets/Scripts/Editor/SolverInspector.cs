using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(Solver))]
public class SolverInspector : Editor
{
	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();

		var script = target as Solver;
		

		GUILayout.Space(20);

		if(GUILayout.Button("--- RUN!!! ---"))
		{
			EditorCoroutine.Start(script.Run());
		}

		if(script.debugLog.Length == 0)
			return;

		GUILayout.Space(40);

		if(GUILayout.Button("--- Save Log ---"))
		{
		}
	}

//	static void WriteString()
//	{
//		string path = "Assets/DebugLog.txt";
//		
//		//Write some text to the test.txt file
//		StreamWriter writer = new StreamWriter(path, true);
//		writer.WriteLine("Test");
//		writer.Close();
//		
//		//Re-import the file to update the reference in the editor
//		AssetDatabase.ImportAsset(path); 
//		TextAsset asset = Resources.Load("test");
//		
//		//Print the text from the file
//		Debug.Log(asset.text);
//	}
}
