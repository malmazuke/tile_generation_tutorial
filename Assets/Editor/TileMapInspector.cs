﻿using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TileMap))]
public class TileMapInspector : Editor {

	public override void OnInspectorGUI() {
		DrawDefaultInspector();
		
		if (GUILayout.Button("Regenerate")){
			TileMap map = (TileMap)target;
			map.BuildMesh();
		}
	}
}
