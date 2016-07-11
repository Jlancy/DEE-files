using UnityEngine;
using System.Collections;

public class Chunk : MonoBehaviour {

    public GameObject[] tiles;              //Array of tiles used to build the doodad
    public int size_x, size_y;              //Size of the doodad
    public int pos_x, pos_y;                //Position of the doodad
    public float tileSize;					//Size of the tiles
    private Transform doodadHolder;         //Store reference to transfrom of the map???

    // Use this for initialization
    void Start()
    {
        //Doodad_Builder();
    }

    // Update is called once per frame
    void Update()
    {

    }
    /*
    void Doodad_Builder()
    {
        doodadHolder = new GameObject("Doodad").transform;
        GameObject toInstantiate;
        int tN = 0;
        for (int y = pos_y; y < pos_y + size_y; y++)
        {
            for (int x = pos_x; x < pos_x + size_x; x++)
            {
                if (tiles[tN])
                {
                    toInstantiate = tiles[tN];
                    GameObject instance = Instantiate(toInstantiate, new Vector3(x * tileSize, y * tileSize, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(doodadHolder);
                }
                tN++;
            }
        }
    }
    */
}
