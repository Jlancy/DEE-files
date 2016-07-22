using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class DungeonBuilder : MonoBehaviour {

   

   
    
    // distance to space rooms apart
    private int roomPadX ;
    private int roomPadY ;

    public int roomWidth = 10;
    public int roomHeight = 5;
    public GameObject spawnItem;
    public GameObject[] spawnEnemy;
    //public Room roomSize = new Room(0,0,10,5);
    
    
    private List<Room> roomList = new List<Room>();
    private List<Room> branchRooms = new List<Room>();
    private List<Rect> hallList = new List<Rect>();

    public GameObject [] tilePrefabList;

    private int[][] tiles;
    
    public int maxRooms = 3;
    private int currentRooms= 0;

    private int OffsetX =0;
    private int OffsetY =0;
    private int boardHeight= 0;
    private int boardWidth= 0;
    [Range(0,20)]
    public int boardPadding = 10;


    // Use this for initialization
    void Start () {
        
        roomPadX = 4 + roomWidth;
        roomPadY = 4 + roomHeight;

        FirstRoomSetup();
        
        Debug.Log("int test" + findSmallestValue(0, -1));
        FindRoomToBranchOff();
        Debug.Log(boardHeight + "_" + boardWidth + "_" + OffsetX + "_" + OffsetY);

        SetupTileBoard();
        SetUpItems();
        //Debug.Log("checkVec"+roomList[5].boardVector);


         InstantiateBoard();
       // InstantiatrArray();


       this.transform.position = this.transform.position - new Vector3(0,40,0);
     

    }
	
    void SetupTileBoard()
    {
        boardHeight = Mathf.Abs(OffsetY - boardHeight)+boardPadding;
        boardWidth = Mathf.Abs(OffsetX - boardWidth)+boardPadding;

        tiles = new int[boardWidth][];
     
        for (int i = 0;i< tiles.GetLength(0) ; i++)
        {
            tiles[i] = new int[boardHeight];
          
        }

        
       for (int r= 0;r< roomList.Count; r++)
        {
            SetRoom(roomList[r]);
        }
        for (int h = 0; h < hallList.Count; h++)
        {
            SetHall(hallList[h]);
        }
    }

    
	

    void FirstRoomSetup()
    {
        roomList.Add(new Room(0,0,roomWidth,roomHeight));
        for(int i = 0;i< 3; i++)
        {
            BranchOff(roomList[0]);
        }
        // make a tele

       // Room temp = new Room(0, 1, 0, 0);

        //temp.boardVector = new Vector2(0, 1);
        //roomList.Add(temp);

    }

    void InstantiatrArray()
    {
        for(int i = 0; i < roomList.Count; i++)
        {
            Debug.Log("room"+i+"_" +  roomList[i].boardVector);
            InstantiateRoom(roomList[i]);
        }

        for(int i= 0; i <hallList.Count;i++)
        {
            InstantiateHall(hallList[i]);
        }
    }


    void FindRoomToBranchOff()
    {
        do
        {
            int randomRoom = Random.Range(0, branchRooms.Count);
            Room nextRoom = branchRooms[randomRoom];
            int ammountOfBranch = Random.Range(1, 4);
            for (int i = 0; i < ammountOfBranch; i++)
            {
                BranchOff(nextRoom);
            }
            branchRooms.Remove(nextRoom);
            Debug.Log("countt"+branchRooms.Count);
            
        } while (currentRooms < maxRooms);
    } 

    void BranchOff(Room room)
    {
        
        Vector2 NextRoomDir = MoveRandomDirection(room);
        if (NextRoomDir != Vector2.zero)
        {
            Vector2 roomPos = new Vector2(((int)NextRoomDir.x * roomPadX) , ((int)NextRoomDir.y * roomPadY) );
            Room NextRoom = new Room((int)roomPos.x, (int)roomPos.y, roomWidth, roomHeight);
            hallList.Add(Room.BuildHallway(room, NextRoom));
            NextRoom.boardVector = NextRoomDir;

            roomList.Add(NextRoom);
            branchRooms.Add(NextRoom);

            OffsetX = findSmallestValue(OffsetX, NextRoom.x);
            OffsetY = findSmallestValue(OffsetY,NextRoom.y);
            boardWidth = findBiggestValue(boardWidth, NextRoom.x + NextRoom.width);
            boardHeight = findBiggestValue(boardHeight, NextRoom.y + NextRoom.height);

            currentRooms++;


            
        }
        else
        {
            branchRooms.Remove(room);
        }
    }

    

    int findSmallestValue(int currentValue ,int newValue )
    {
        if (currentValue > newValue)
            return newValue;

        else
            return currentValue;
    }
    int findBiggestValue(int currentValue,int newValue)
    {
        if(currentValue < newValue)
            return newValue;

        else
            return currentValue;
    }

    Vector2 MoveRandomDirection(Room room)
    {
        
        List<int> randomNumList = new List<int>(new int[]{ 0,1,2,3});
       // bool donePicking = false;
        for(int r = 0; r <4;r++)
        {
            
            int randonNumArray = Random.Range(0, randomNumList.Count); 
            int randomInt = randomNumList[randonNumArray] ;
            
            Vector2 dir = Direction(randomInt, room);
            //Debug.Log(dir);
           
           
                if (CheckForRoom(dir) == false)
                {

                   // donePicking = true;
                    return dir;
                }
                else
                    randomNumList.Remove(randomInt);
            
        }
        return Vector2.zero;
    }

    bool CheckForRoom(Vector2 pos)
    {
        
        if (roomList.Exists(r => r.boardVector == pos))
            return true;
        else
            return false;
    }

    Vector2  Direction(int randomDirection,Room room)
    {
        //up
        if(randomDirection == 0)
            return room.boardVector + new Vector2(0, 1);                  
        //down
       else if(randomDirection == 1)
            return room.boardVector + new Vector2(0, -1);
        //left
        else if (randomDirection == 2)   
            return room.boardVector + new Vector2(-1, 0);
        // right
        else
            return room.boardVector + new Vector2(1 , 0);
    
        
    }

    void InstantiateRoom(Room room)
    {
    
        for (int w = 0; w < room.width; w++)
        {
            for(int h = 0; h < room.height;h++)
            {
                int randomInt = Random.Range(0, tilePrefabList.Length);
                Vector2 pos = new Vector2(room.x+w, room.y+h);
                GameObject tileInstance = Instantiate(tilePrefabList[randomInt], pos, Quaternion.identity) as GameObject;
                tileInstance.transform.parent = this.transform;
            }
        }
    }

    void InstantiateHall(Rect hall)
    {

        for (int w = 0; w < hall.width; w++)
        {
            for (int h = 0; h < hall.height; h++)
            {
                int randomInt = Random.Range(0, tilePrefabList.Length);
                Vector2 pos = new Vector2(hall.x + w, hall.y + h);
                GameObject tileInstance = Instantiate(tilePrefabList[randomInt], pos, Quaternion.identity) as GameObject;
                tileInstance.transform.parent = this.transform;
            }
        }
    }

    void SetRoom(Room room)
    {

        for (int w = 0; w < room.width; w++)
        {
            for (int h = 0; h < room.height; h++)
            {
               
                Vector2 pos = new Vector2(room.x + w - OffsetX + (boardPadding / 2), room.y + h - OffsetY + (boardPadding / 2));
               
                tiles[(int)pos.x][(int)pos.y] = 1;
              
            }
        }
    }
    void SetHall(Rect room)
    {

        for (int w = 0; w < room.width; w++)
        {
            for (int h = 0; h < room.height; h++)
            {

                Vector2 pos = new Vector2(room.x + w - OffsetX +(boardPadding/2), room.y + h - OffsetY + (boardPadding / 2));

                tiles[(int)pos.x][(int)pos.y] = 1;

            }
        }
    }
    void InstantiateBoard()
    {
        for(int i = 0; i< tiles.Length;i++)
        {
            for (int t = 0;t < tiles[i].Length;t++)
            {
                int tileID = tiles[i][t];
               
                Vector2 pos = new Vector2(i, t);
                GameObject tileInstance = Instantiate(tilePrefabList[tileID], pos, Quaternion.identity) as GameObject;

                
                tileInstance.transform.parent = this.transform;
            }
        }
    }


    void SetUpItems()
    {
        int randomRooom = Random.Range(0, branchRooms.Count);
        Room itemRoom = branchRooms[randomRooom];
        Vector2 pos = new Vector2(itemRoom.centerPoint.x  - OffsetX + (boardPadding / 2), itemRoom.centerPoint.y - OffsetY + (boardPadding / 2));
        GameObject item = Instantiate(spawnItem, pos, Quaternion.identity) as GameObject;
        item.transform.parent = this.transform;
    }
    /*
     * make first room( assign vector to every room based off the first)
     * have chest room adjacent 
     * intal adjacent room ammount( probaly stick 2 )
     * use case to branch off( based off direction)
     * have a leaf effect occur 
     * 
     */

}
