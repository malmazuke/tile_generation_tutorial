using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter  ))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class TGMap : MonoBehaviour {

	public int sizeX = 100;
	public int sizeZ =  50;
	public float tileSize = 1.0f;
	public float tileHeight = 0.2f;
	public Texture2D terrainTiles;
	public int tileResolution;
	
	// Use this for initialization
	void Start () {
		BuildMesh();
	}
	
	Color[][] ChopUpTiles() {
		int numTilesPerRow = terrainTiles.width / tileResolution;
		int numRows = terrainTiles.height / tileResolution;
		
		Color[][] tiles = new Color[numTilesPerRow * numRows][];
		
		for (int y = 0; y < numRows; y++) {
			for (int x = 0; x < numTilesPerRow; x++) {
				tiles[y * numTilesPerRow + x] = terrainTiles.GetPixels(x*tileResolution, y*tileResolution, tileResolution, tileResolution);
			}
		}
		
		return tiles;
	}
	
	public void BuildTexture() {
		int texWidth = sizeX * tileResolution;
		int texHeight = sizeZ * tileResolution;
		Texture2D texture = new Texture2D(texWidth, texHeight);
		
		Color[][] tiles = ChopUpTiles();
		
		for (int y = 0; y < sizeZ; y++) {
			for (int x = 0; x < sizeX; x++) {
				Color[] p = tiles[Random.Range(0, tiles.Length)];
				texture.SetPixels(x * tileResolution, y * tileResolution, tileResolution, tileResolution, p);
			} 
		}
		texture.filterMode = FilterMode.Point;
		texture.Apply();
		
		MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
		meshRenderer.sharedMaterials[0].mainTexture = texture;
	}
	
	public void BuildMesh() {		
		int numTiles = sizeX * sizeZ;
		int numTris = numTiles * 2;
		
		int vSizeX = sizeX + 1;
		int vSizeZ = sizeZ + 1;
		int numVerts = vSizeX * vSizeZ;
		 
		// Generate the mesh data
		Vector3[] vertices = new Vector3[numVerts];
		Vector3[] normals  = new Vector3[numVerts];
		Vector2[] uv 	   = new Vector2[numVerts];
		
		int[] triangles = new int[numTris * 3];
		
		int x, z;
		
		for (z = 0; z < vSizeZ; z++) {
			for (x = 0; x < vSizeX; x++) {
				vertices[z * vSizeX + x] = new Vector3(x * tileSize, Random.Range(-tileHeight, tileHeight), z * tileSize);
				normals[z * vSizeX + x] = Vector3.up;
				uv[z * vSizeX + x] = new Vector2((float)x / sizeX, (float)z / sizeZ);
			}
		}
		
		for (z = 0; z < sizeZ; z++) {
			for (x = 0; x < sizeX; x++) { 
				int squareIndex = z * sizeX + x;
				int triOffset = squareIndex * 6;
				
				triangles[triOffset + 0] = z * vSizeX + x + 	   	 0;
				triangles[triOffset + 1] = z * vSizeX + x + vSizeX + 0;
				triangles[triOffset + 2] = z * vSizeX + x + vSizeX + 1;
				
				triangles[triOffset + 3] = z * vSizeX + x +  	     0;
				triangles[triOffset + 4] = z * vSizeX + x + vSizeX + 1;
				triangles[triOffset + 5] = z * vSizeX + x + 		 1;
			}
		}
		
		// Create a new mesh and populate with the data
		Mesh mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.normals = normals;
		mesh.uv = uv;
	
		// Assign our mesh to our filter/renderer/collider
		MeshFilter   meshFilter   = GetComponent<MeshFilter>();
		MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
		MeshCollider meshCollider = GetComponent<MeshCollider>();	
		
		meshFilter.mesh = mesh;
		meshCollider.sharedMesh = mesh;
		
		BuildTexture();
	}
}
