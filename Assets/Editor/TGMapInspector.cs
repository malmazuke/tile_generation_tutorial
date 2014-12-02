using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TGMap))]
public class TGMapInspector : Editor {

	public override void OnInspectorGUI() {
		DrawDefaultInspector();
		
		if (GUILayout.Button("Regenerate")){
			TGMap map = (TGMap)target;
			map.BuildMesh();
		}
	}
}
