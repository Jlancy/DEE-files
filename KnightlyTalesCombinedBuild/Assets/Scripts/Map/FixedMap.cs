using UnityEngine;
using System.Collections;

public class FixedMap : MonoBehaviour {
    //==========================================================================
    //Variables
    //==========================================================================
    //public int size_x = MAP_CONSTANT.CHUNK_WIDTH * 3;
    //public int size_y = MAP_CONSTANT.CHUNK_HEIGHT * 2;
    public int size_x = 39;
    public int size_y = 22;
    public float tileSize;                  //Size of the tiles

    public GameObject nullTile;             //PlaceHolder Tiles
    public GameObject[] grassTiles;         //Array of grass tiles
    public GameObject[] treeTiles;          //Array of tree tiles
    public GameObject[] dirtTiles;          //Array of dirt tiles
    public GameObject[] doodadTiles;        //This would include prebuilt tiles, trees may move here
    public int[,] mapData;

    private Transform mapHolder;            //Store reference to transfrom of the map???

    //==========================================================================
    //Function
    //==========================================================================
    
    // Use this for initialization
    void Start () {
        Initialize();
        BuildMap();
    }

    // may not need
    void Initialize() 
    {
        //gridMap = new Vector3[size_x, size_y];
        //mapData = new TDMap(size_x, size_y, MapType.Forest);
        mapData = new int[22, 39] 
        //            1  2  3  4  5  6  7  8  9 10 11 12 13  1  2  3  4  5  6  7  8  9 10 11 12 13  1  2  3  4  5  6  7  8  9 10 11 12 13  
                  { { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },   // 1
                    { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },   // 2
                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1 },   // 3
                    { 1, 0, 0, 1, 2, 2, 2, 2, 2, 1, 0, 0, 1, 1, 0, 0, 1, 3, 0, 0, 1, 3, 0, 0, 0, 1, 1, 0, 0, 2, 2, 2, 2, 2, 0, 0, 1, 1, 1 },   // 4
                    { 1, 0, 0, 2, 2, 2, 2, 2, 2, 2, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 2, 2, 2, 2, 2, 2, 2, 0, 1, 1, 1 },   // 5
                    { 1, 0, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1 },   // 6
                    { 1, 0, 0, 2, 2, 2, 2, 2, 2, 2, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 2, 2, 2, 2, 2, 2, 2, 0, 1, 2, 1 },   // 7
                    { 1, 0, 0, 1, 2, 2, 2, 2, 2, 1, 0, 0, 1, 1, 0, 0, 0, 3, 1, 0, 0, 3, 1, 0, 0, 1, 1, 0, 0, 2, 2, 2, 2, 2, 0, 0, 1, 2, 1 },   // 8
                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 2, 1 },   // 9
                    { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1 },   //10
                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1 },   //11
                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1 },   // 1
                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 2, 1 },   // 2
                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 2, 2, 2, 2, 2, 2, 2, 2, 1 },   // 3
                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1 },   // 4
                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1 },   // 5
                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1 },   // 6
                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 1 },   // 7
                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 2, 2, 2, 2, 2, 2, 2, 0, 0, 1 },   // 8
                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 2, 2, 2, 2, 2, 0, 0, 1, 1 },   // 9
                    { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1 },   //10
                    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } }; //11

    }

    void BuildMap()
    {
        mapHolder = new GameObject("Map").transform;
        int tileCheck;
        GameObject toInstantiate;

        for (int y = 0; y < size_y; y++)
        {
            for (int x = 0; x < size_x; x++)
            {
                tileCheck = mapData[y, x];
                toInstantiate = nullTile;

                // 0 = plain grass
                // 1 = tree
                // 2 = dirt
                // 3 = movable object, additionally, an item will be hidden under this object

                if (tileCheck == 0)
                {
                    GameObject baseinstance = Instantiate(grassTiles[Random.Range(0, grassTiles.GetLength(0))], new Vector3(x * tileSize, y * tileSize, 0f), Quaternion.identity) as GameObject;
                    baseinstance.transform.SetParent(mapHolder); // Place gameobject under Map
                }
                else if (tileCheck == 1)
                {
                    toInstantiate = treeTiles[Random.Range(0, treeTiles.GetLength(0))];
                    GameObject instance = Instantiate(toInstantiate, new Vector3(x * tileSize, y * tileSize, 0f), Quaternion.identity) as GameObject;
                    GameObject baseinstance = Instantiate(grassTiles[Random.Range(0, grassTiles.GetLength(0))], new Vector3(x * tileSize, y * tileSize, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(mapHolder); // Place gameobject under Map
                    baseinstance.transform.SetParent(mapHolder); // Place gameobject under Map
                }
                else if (tileCheck == 2)
                {
                    toInstantiate = dirtTiles[Random.Range(0, dirtTiles.GetLength(0))];
                    GameObject instance = Instantiate(toInstantiate, new Vector3(x * tileSize, y * tileSize, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(mapHolder); // Place gameobject under Map
                }
                else if (tileCheck == 3)
                {
                    GameObject baseinstance = Instantiate(grassTiles[Random.Range(0, grassTiles.GetLength(0))], new Vector3(x * tileSize, y * tileSize, 0f), Quaternion.identity) as GameObject;
                    baseinstance.transform.SetParent(mapHolder); // Place gameobject under Map
                }

                //GameObject baseinstance = Instantiate(grassTiles[Random.Range(0, grassTiles.GetLength(0))], new Vector3(x * tileSize, y * tileSize, 0f), Quaternion.identity) as GameObject;

                //GameObject instance = Instantiate(toInstantiate, new Vector3(x * tileSize, y * tileSize, 0f), Quaternion.identity) as GameObject;
                //baseinstance.transform.SetParent(mapHolder);
                //instance.transform.SetParent(mapHolder);
            }
        }
    }
}
