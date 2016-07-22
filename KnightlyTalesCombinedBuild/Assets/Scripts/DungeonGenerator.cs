using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DungeonGenerator : MonoBehaviour {

	public bool Activate =false;
	public bool Draw =false;
	public bool Grid =false;
	public bool SaveList = false;
	public bool RoomCheck =false;
	public int DebugLeafInt= 0;
	public List<Leaf> leafList = new List<Leaf>();
	public PuzzleList puzzleList;
	private Leaf root ;
	public int gridSize;
	public int TestX =1;
	public LayerMask mask;
	int[,] dungeon;

	public MapSave saveData;

    public List<Room> RoomList = new List<Room>();

	public List<Rect> HallWay = new List<Rect>();
	public Room[,] RoomLayout; 
    public List<Vector2> roomPos = new List<Vector2>();
    public List<Vector2> usedRoomPos = new List<Vector2>();
    int colCount =0;
   
	public void Start()
	{
        
		Leaf root = new Leaf(0,0,gridSize,gridSize);
		dungeon = new int[gridSize,gridSize];
		RoomLayout = new Room[7, 7];
		Debug.Log(dungeon.GetLength(0) +"_"+dungeon.GetLength(1) );
		leafList.Add(root);
		puzzleList = GameObject.FindObjectOfType<PuzzleList>();
		StartCoroutine(BuildLeafTree());


	}
	public void Update()
	{
		if(RoomCheck)
		{
          
         //   Room tempRoom = ReturnCenterRoom();
            //Debug.Log(tempRoom.x+"__"+tempRoom.y);
			//FindLowestRoomX ();
            Debug.Log(leafList[0].LastLeftLeaf().room.x +"_"+leafList[0].LastLeftLeaf().room.y);
            Debug.Log(leafList[0].LastRightLeaf().room.x+"_" +leafList[0].LastRightLeaf().room.y);
            RoomCheck =false;
		}

		if(Activate)
		{
			StartCoroutine(BuildLeafTree());
		}

		if(SaveList)
		{
			saveData.MapList.Add( new MapInfo(dungeon ,this.transform.position));
			SaveList =false;
		}

		if(Grid)
		{
			int count = leafList.Count;
			Debug.Log(count);
			//drawRoom(leafList[0].x,leafList[0].y, leafList[0].width, leafList[0].height);
			for(int l = 0 ; l < leafList.Count ; l++)
			{

				//Debug.Log("l:"+ leafList[l].halls.Count);


				
				if (leafList[l].room != null)
				{	//Debug.Log(l);
					if(leafList[l].leftChild == null || leafList[l].rightChild == null)
					{
						SetGrid(leafList[l].room.x,leafList[l].room.y, leafList[l].room.width, leafList[l].room.height,1);
						//RoomList.Add(leafList[l].room);

						

					}
					else
					{
						leafList[l].room = new Room(0,0,0,0);
					}
				}


				if( leafList[l].halls != null)
				{
					for(int i = 0 ; i < leafList[l].halls.Count; i++)
					{

                        //if(leafList[l].leftChild == null || leafList[l].rightChild == null)
						SetGrid(leafList[l].halls[i].x,leafList[l].halls[i].y,leafList[l].halls[i].width, leafList[l].halls[i].height,1);
						//HallWay.Add(leafList[l].halls[i]);

					}
				}

			}


		
			Draw =true;
			Grid=false;



		}
		if (Draw)
		{
			//RoomList.Reverse();
			draw();

			transform.position = new Vector2(33,0);
		
		}
	

	}

	// temp room creation to make sure its working
	void SetGrid(float x , float y, float width, float height, int tileInt)
	{


		// start postion 
		Vector2 CurrentPos = new Vector2((int)x , (int)y );

		// set tile id in grid
		for(int row = 0; row < (int)width; row++ )
		{
			
			for(int col = 0; col <(int)height; col++)
			{
				//Debug.Log(CurrentPos);
				dungeon[(int)CurrentPos.x,(int)CurrentPos.y] =tileInt;
				CurrentPos = CurrentPos + new Vector2(0,1);

			}
			CurrentPos = CurrentPos + new Vector2(1,- (int)height);
		}


	}

	void draw()
	{
		
		Vector2 CurrentPos = new Vector2(0, 0 );
		Color ColorArea;
	
		// creat tile based of id from prefablist;
		for(int row = 0; row < dungeon.GetLength(0); row++ )
		{

			for(int col = 0; col < dungeon.GetLength(1); col++)
			{
				int TileID = dungeon[(int)CurrentPos.x,(int)CurrentPos.y];
				GameObject tile;
				tile = Instantiate(puzzleList.TempTile[TileID]);
				tile.transform.parent = this.transform;
				//Debug.Log(CurrentPos);

				if (TileID == 1)
					ColorArea = Color.white;
				else if (TileID == 2)
					ColorArea = Color.green;
				else
					ColorArea =Color.white;
					
				tile.GetComponent<SpriteRenderer>().color = ColorArea;
				tile.transform.position = CurrentPos;
				CurrentPos = CurrentPos + new Vector2(0,1);

			}
			CurrentPos = CurrentPos + new Vector2(1,-dungeon.GetLength(1));
		}

		Draw =false;
	}

	
    /*
	void FindRoomCol(int ColX)
	{	//
		List<Room> tempColRoomList = new List<Room>(); 

		for (int i = 0; i < RoomList.Count; i++) 
		{
			if(RoomList[i].x <= ColX+4)
			{
				tempColRoomList.Add (RoomList [i]);

			}
				
		}


		tempColRoomList.Sort((a,b)=> b.y.CompareTo(a.y) );

		for (int t = 0; t < tempColRoomList.Count; t++) 
		{
			RoomList.Remove (tempColRoomList [t]);
			//Debug.Log (tempColRoomList [t].y);
		}
		AddToLayout (tempColRoomList,colCount);
		colCount++;
		FindLowestRoomX ();
	}
	void FindLowestRoomX()
	{
		int LowestX = 50;
        if (RoomList.Count != 0)
        {
            for (int i = 0; i < RoomList.Count; i++)
            {
                if (LowestX > RoomList[i].x)
                    LowestX = RoomList[i].x;
            }
            FindRoomCol(LowestX);
        }

	}
	void AddToLayout(List<Room> roomList, int col)
	{
		
		if (RoomLayout [0, col] == null) {
			for (int i = 0; i < roomList.Count; i++) {
				RoomLayout [i, col] = roomList [i];
                roomPos.Add ( new Vector2(i, col));
			}

		} 


	}
    */

	// modify
	// this will loop unitll it cant creat anymore more child leafs
	IEnumerator BuildLeafTree()
	{
		Activate = false;
		


		bool didSplit = true;
		// temp list  to  add new child leafs after to main list 
		// after foreach loop is done 
		List<Leaf> tempLeaf = new List<Leaf>();

		while(didSplit)
		{
			didSplit =false;
			// loop throuhg main leaf list  and check
			// if any leaf has any empty child 
			// if it is add new child leafs if possible
			foreach ( Leaf l in leafList)
			{
				if(l.leftChild == null && l.rightChild == null)
				{
					if(l.width > Leaf.MAX_LEAF_SIZE || l.height > Leaf.MAX_LEAF_SIZE || Random.value > 0.25f )
					{
						if (l.Split())
						{
							tempLeaf.Add(l.leftChild);
							tempLeaf.Add(l.rightChild);
							didSplit =true;
						}
					}
				}
			}
			yield return null;
		}

		// add temp list to main list;
		if(tempLeaf.Count !=0)

		{
			foreach(Leaf l in tempLeaf)
			{
               
				leafList.Add(l);
			}
			tempLeaf.Clear();
            leafList[0].CreatRooms();
           
          


			StartCoroutine(BuildLeafTree());
		}

		else{
			
            leafList[0].BuidlHalls();
            //leafList[0].rightChild.BuidlHalls();
            //leafList[0].leftChild.BuidlHalls();
            leafList [0].rightChild.ReturnLastRooms (RoomList);
            leafList [0].leftChild.ReturnLastRooms (RoomList);
           // FindLowestRoomX();
            //leafList[0].CreatRooms();
			//Debug.Log("b4Grid "+ leafList.Count);
			
			Grid =true;
		}
			
	}





}
