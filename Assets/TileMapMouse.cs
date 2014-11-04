using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TileMap))]
public class TileMapMouse : MonoBehaviour {

	TileMap _tileMap;
	Vector3 _currentTileCoord;
	public Transform selectionCube;
	
	void Start() {
		_tileMap = GetComponent<TileMap>();
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;
		
		if (collider.Raycast(ray, out hitInfo, Mathf.Infinity)) {
			int x = Mathf.FloorToInt(hitInfo.point.x / _tileMap.tileSize);
			int z = Mathf.FloorToInt(hitInfo.point.z / _tileMap.tileSize);
			
			_currentTileCoord.x = x;
			_currentTileCoord.z = z;
			
			selectionCube.transform.position = _currentTileCoord;
		} else {
			
		}
	}
}
