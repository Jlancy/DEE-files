using UnityEngine;
using System.Collections;



public class N_Map : MonoBehaviour {
    //==========================================================================
    //Constants
    //==========================================================================
    //Chunk constant
    public const int CHUNK_WIDTH = 13;
    public const int CHUNK_HEIGHT = 10;
    
    //Map dimension constants
    public const int SML_WIDTH = 3;
    public const int SML_HEIGHT = 5;
    public const int LRG_WIDTH = 6;
    public const int LRG_HEIGHT = 10;

    //==========================================================================
    //Variables
    //==========================================================================
    public int size_x = MAP_CONSTANT.CHUNK_WIDTH * MAP_CONSTANT.SML_WIDTH;
    public int size_y = MAP_CONSTANT.CHUNK_HEIGHT * MAP_CONSTANT.SML_HEIGHT;
    public float tileSize;                  //Size of the tiles

    public GameObject nullTile;             //PlaceHolder Tiles
    public GameObject[] grassTiles;         //Array of grass tiles
    public GameObject[] treeTiles;          //Array of tree tiles
    public GameObject[] waterTiles;         //Array of water tiles
    public GameObject[] dirtTiles;          //Array of dirt tiles
    public GameObject[] dirtToGrassTiles;   //Array of dirt to grass tiles
    public GameObject[] doodadTiles;        //This would include prebuilt tiles, trees may move here


    private Transform mapHolder;            //Store reference to transfrom of the map???
                                            //private Vector3[,] gridMap;				//Array of the grid
                                            //private List<Vector3> gridMap2;			//List of the grid

    private TDMap mapData;                  //
                                            //public MapType mapType;
                                            //private int[,] mapData;
                                            //private int[,] collisionMap;

    //==========================================================================
    //Function
    //==========================================================================

    void Start()
    {
        Initialize();
        BuildMap();
    }

    void Initialize()
    {
        //gridMap = new Vector3[size_x, size_y];
        mapData = new TDMap(size_x, size_y, MapType.Forest);
    }

    void OnDestroy()
    {
        //DestroyImmediate(mapHolder);
    }

    void BuildMap()
    {
        mapHolder = new GameObject("Map").transform;
        TileType tileCheck;
        GameObject toInstantiate;

        for (int y = 0; y < size_y; y++)
        {
            for (int x = 0; x < size_x; x++)
            {
                tileCheck = mapData.GetTileTypeAt(x, y);
                toInstantiate = nullTile;

                if (tileCheck == TileType.Water)
                    toInstantiate = waterTiles[Random.Range(0, dirtTiles.GetLength(0))];
                else if (tileCheck == TileType.Dirt)
                    toInstantiate = dirtTiles[Random.Range(0, dirtTiles.GetLength(0))];
                else if (tileCheck == TileType.Tree)
                    toInstantiate = treeTiles[Random.Range(0, treeTiles.GetLength(0))];

                GameObject baseinstance = Instantiate(grassTiles[Random.Range(0, grassTiles.GetLength(0))], new Vector3(x * tileSize, y * tileSize, 0f), Quaternion.identity) as GameObject;
                GameObject instance = Instantiate(toInstantiate, new Vector3(x * tileSize, y * tileSize, 0f), Quaternion.identity) as GameObject;
                baseinstance.transform.SetParent(mapHolder);
                instance.transform.SetParent(mapHolder);
            }
        }
    }
}
