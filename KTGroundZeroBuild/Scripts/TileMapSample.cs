using UnityEngine;
using System.Collections.Generic; 		//Allows us to use Lists.


//public enum MapType : int {Forest, Cave, ForestByChunk};

//[ExecuteInEditMode]
public class TileMapSample : MonoBehaviour {
	//==========================================================================
	//Variables
	//==========================================================================
	public int size_x = 20;					//X size, max width
	public int size_y = 20; 				//Y size, max height
	public float tileSize;					//Size of the tiles

	public GameObject nullTile;				//PlaceHolder Tiles
	public GameObject[] grassTiles;			//Array of grass tiles
	public GameObject[] treeTiles;			//Array of tree tiles
	public GameObject[] waterTiles;			//Array of water tiles
	public GameObject[] dirtTiles;			//Array of dirt tiles
	public GameObject[] dirtToGrassTiles;	//Array of dirt to grass tiles
	public GameObject[] otherTiles;		


	private Transform mapHolder;			//Store reference to transfrom of the map???
	//private Vector3[,] gridMap;				//Array of the grid
	//private List<Vector3> gridMap2;			//List of the grid

	private TDMap mapData;					//
	//public MapType mapType;
	//private int[,] mapData;
	//private int[,] collisionMap;

	//==========================================================================
	//Function
	//==========================================================================

	void Start(){
		Initialize ();
		BuildMap ();
	}

	void Initialize(){
		//gridMap = new Vector3[size_x, size_y];
		mapData = new TDMap (size_x, size_y, MapType.Forest);
	}

	void OnDestroy(){
		//DestroyImmediate(mapHolder);
	}

	void BuildMap(){
		mapHolder = new GameObject ("Map").transform;
		TileType tileCheck;
		GameObject toInstantiate;

		for (int y = 0; y < size_y; y++) {
			for (int x = 0; x < size_x; x++) {
				tileCheck = mapData.GetTileDataAt (x, y).GetTileType();
				toInstantiate = nullTile;

				if(tileCheck == TileType.Water)
					toInstantiate = waterTiles[Random.Range(0, dirtTiles.GetLength(0))];
				else if(tileCheck == TileType.Dirt)
					toInstantiate = dirtTiles[Random.Range(0, dirtTiles.GetLength(0))];
				else if(tileCheck == TileType.TreeOnGrass)
					toInstantiate = treeTiles[0];
				else if(tileCheck == TileType.AppleTree)
					toInstantiate = treeTiles[1];
				else if(tileCheck == TileType.DirtToGrassT)
					toInstantiate = dirtToGrassTiles[0];
				else if(tileCheck == TileType.DirtToGrassL)
					toInstantiate = dirtToGrassTiles[1];
				else if(tileCheck == TileType.DirtToGrassR)
					toInstantiate = dirtToGrassTiles[2];
				else if(tileCheck == TileType.DirtToGrassB)
					toInstantiate = dirtToGrassTiles[3];
				else if(tileCheck == TileType.DirtToGrassTL)
					toInstantiate = dirtToGrassTiles[4];
				else if(tileCheck == TileType.DirtToGrassTR)
					toInstantiate = dirtToGrassTiles[5];
				else if(tileCheck == TileType.DirtToGrassBL)
					toInstantiate = dirtToGrassTiles[6];
				else if(tileCheck == TileType.DirtToGrassBR)
					toInstantiate = dirtToGrassTiles[7];
				else if(tileCheck == TileType.DirtToGrassTLi)
					toInstantiate = dirtToGrassTiles[8];
				else if(tileCheck == TileType.DirtToGrassTRi)
					toInstantiate = dirtToGrassTiles[9];
				else if(tileCheck == TileType.DirtToGrassBLi)
					toInstantiate = dirtToGrassTiles[10];
				else if(tileCheck == TileType.DirtToGrassBRi)
					toInstantiate = dirtToGrassTiles[11];
				else if(tileCheck == TileType.NorthGate1)
					toInstantiate = otherTiles[0];
				else if(tileCheck == TileType.NorthGate2)
					toInstantiate = otherTiles[1];
				else if(tileCheck == TileType.Shit)
					toInstantiate = otherTiles[2];
					

				/*
				switch(tileCheck){
				case TileType.Water:
					toInstantate = waterTiles[Random.Range(0, dirtTiles.GetLength(0))];
					break;
				case TileType.Dirt:
					toInstantate = dirtTiles[Random.Range(0, dirtTiles.GetLength(0))];
					break;

				}
				*/
				GameObject baseinstance = Instantiate(grassTiles[Random.Range(0, grassTiles.GetLength(0))], new Vector3(x * tileSize, y * tileSize, 0f), Quaternion.identity) as GameObject;
				GameObject instance = Instantiate(toInstantiate, new Vector3(x * tileSize, y * tileSize, 0f), Quaternion.identity) as GameObject;
				baseinstance.transform.SetParent(mapHolder);
				instance.transform.SetParent (mapHolder);
			}
		}
	}
		//use texture to test the map generation code
	//==============================================================================================



}
