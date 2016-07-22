using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class Leaf  {

	public const int MIN_LEAF_SIZE = 9;
	public const int MAX_LEAF_SIZE = 18;
	public int x;
	public int y; 
	public int width;
	public int height;
	public Leaf leftChild;
	public Leaf rightChild;

	public Room room;

	public List<Rect> halls = new List<Rect>();



	public Leaf(int X, int Y, int Width, int Height)
	{
		x = X;
		y = Y;
		width = Width;
		height = Height;
	}

	public bool Split()
	{
		//check before spliting if the child are empty
		//Random.seed = (int)System.DateTime.Now.Ticks;
		if(leftChild !=null || rightChild !=null)
			return false;
		// Decide to slice horizontal or vertical
		bool splitH = CoinFlip();
		if (width > height && (float)width / (float)height >=1.25f)
			splitH =false;
		else if (height > width && (float)height / (float)width >=1.25f)
			splitH =true;

		// check if its possible to make a slice
		int max = (splitH ? height :width) - MIN_LEAF_SIZE;
		if (max <= MIN_LEAF_SIZE)
			return false;
		// get random number
		int split = Random.Range(MIN_LEAF_SIZE,max);


		// slice horizontal
		// add child leafs
		if(splitH)
		{
			leftChild = new Leaf(x,y,width,split);
			rightChild  =new Leaf(x,y+split,width, height -split);
		}
		//slice vertical
		// add child leafs
		else
		{
			leftChild = new Leaf(x,y,split,height);
			rightChild = new Leaf(x+split, y, width - split, height);

		}


			return true;
	}

    public void CreatRooms()
	{
	

		if(leftChild !=null || rightChild !=null)
		{
			if(leftChild!= null)
                leftChild.CreatRooms();

			if(rightChild!= null)
                rightChild.CreatRooms();

			if(leftChild!= null && rightChild != null)
			{
				
			 //createHall2(leftChild.getRoom(), rightChild.getRoom());
			}
		}
		else 
		{
			//Debug.Log("buidlRoom");
			Vector2 roomSize;
			Vector2 roomPos;

			//roomSize = new Vector2(width-2,height-2);
			//roomPos = new Vector2( width - roomSize.x -1 ,height - roomSize.y-1 );

			roomSize = new Vector2((int)Random.Range(width/1.5f,width-2), (int)Random.Range(height/1.5f, height-2));
			//roomSize = new Vector2((int)Random.Range(6,width-2), (int)Random.Range(6, height-2));
			roomPos = new Vector2( (int)Random.Range(1,width - roomSize.x -1), (int)Random.Range(1,height - roomSize.y -1));
			room = new Room( x+ (int)roomPos.x , y + (int)roomPos.y,(int)roomSize.x, (int)roomSize.y );
           
		}
	}
	public void BuidlHalls()
	{
		if(leftChild !=null || rightChild !=null)
		{
			if(leftChild!= null)
				leftChild.BuidlHalls();

			if(rightChild!= null)
				rightChild.BuidlHalls();

			if(leftChild!= null && rightChild != null)
			{
                //Debug.Log(leftChild.room.x + "E_" + rightChild.room.x);
                createHall2(leftChild.getRoom(), rightChild.getRoom());
			}
		}
	}


	public void ReturnLastRooms(List<Room> roomList)
	{
		if(leftChild != null || rightChild  != null)
		{
			if (leftChild != null) 
				leftChild.ReturnLastRooms (roomList);
		
			if (rightChild != null)
				rightChild.ReturnLastRooms (roomList);
			
				

		}
		else 
		{

				roomList.Add (room);
			


			
		}

	
	}
	public Room getRoom()
	{
		Room lRoom  = new Room();
		Room rRoom  = new Room();
        if(room != null )
            return room;

		else
			
		{
			if(leftChild != null)
			{
				
				lRoom = leftChild.getRoom();
			}
			if(rightChild!=null)
			{
				rRoom = rightChild.getRoom();
			}

			if(lRoom == new Room()&& rRoom == new Room())
				return null;
			
			else if(rRoom == new Room())
				return lRoom;
			else 
				return rRoom;


			
		}
	}

	public void createHall(Room l, Room r)
	{

		//random pos 
		//Vector2 point_1 = new Vector2 (Random.Range(l.x +1, l.x + l.width -2  ), Random.Range(l.y +1, l.y + l.height-2));
		//Vector2 point_2 = new Vector2 (Random.Range(r.x +1, r.x+ r.width -2  ), Random.Range(r.y +1, r.y +r.height -2));


		halls.Clear();
		//Vector2 point_1 = new Vector2 ((int)l.x +2  ,(int)l.y +2);
		//Vector2 point_2 = new Vector2 ((int)r.x +2 ,(int)r.y +2);
		
        Vector2 point_1 = new Vector2(l.centerPoint.x,l.centerPoint.y);
        Vector2 point_2 = new Vector2(r.centerPoint.x, r.centerPoint.y);


		float  w = point_2.x - point_1.x;
		float  h = point_2.y - point_1.y;


		if (point_1.x >=1 && point_1.y >=1  && point_2.x >=1  && point_2.y >=1 )
		{
	
		if(w < 0)
		{

			if(h<0)
			{
				
			
					halls.Add(new Rect(point_2.x, point_1.y,Mathf.Abs(w), 1));
					//halls.Add(new Rect(point_2.x, point_2.y,1,Mathf.Abs(h)));
				
			}
			else if(h >0)
			{
				
				
					//halls.Add(new Rect(point_2.x, point_1.y, Mathf.Abs(w),1));
					halls.Add(new Rect(point_2.x, point_1.y, 1 ,Mathf.Abs(h)));
			

			}
			else if (h== 0)
			{
				
				halls.Add(new Rect(point_2.x, point_2.y, Mathf.Abs(w), 1));
			}
		}

		else if (w >0)
		{
		
			if(h<0)
			{

			
					halls.Add(new Rect(point_1.x, point_2.y,Mathf.Abs(w), 1));
					halls.Add(new Rect(point_1.x, point_2.y,1,Mathf.Abs(h)));
			
			}
			else if(h >0)
			{
				
				
					halls.Add(new Rect(point_1.x, point_1.y, Mathf.Abs(w),1));
					halls.Add(new Rect(point_2.x, point_1.y, 1 ,Mathf.Abs(h)));
			
			}
			else if (h== 0)
			{

				halls.Add(new Rect(point_1.x, point_1.y, Mathf.Abs(w), 1));
			}
		}

		else if(w == 0)
		{
			//Debug.Log ("rectL"+l +"w<0"+"w:"+w+" h:"+h +"rectR"+r);
			if (h <0)
			{
				halls.Add(new Rect( point_2.x, point_2.y,1, Mathf.Abs(h)));
			}
			else if( h >0)
			{
				halls.Add(new Rect( point_1.x, point_1.y,1, Mathf.Abs(h)));
			}
		}
		}
	
	}

    public void createHall2(Room l, Room r)
    {
        halls.Clear();
        
        Vector2 point_1 = new Vector2 ((int)l.x +2  ,(int)l.y +2);
        Vector2 point_2 = new Vector2 ((int)r.x +2 ,(int)r.y +2);

        //Vector2 point_1 = new Vector2 (l.centerPoint.x, l.centerPoint.y);
       // Vector2 point_2 = new Vector2 (r.centerPoint.x, r.centerPoint.y);


        float  w = point_2.x - point_1.x;
        float  h = point_2.y - point_1.y;

        float angle = Mathf.Atan2(w,h) * (180/ Mathf.PI);


        //Debug.Log("angle" + angle);
        //+
        if (angle >= 0 && angle<180) 
        {
            if (0 <= angle && angle <= 45) 
            {
               // Debug.Log("1");
                halls.Add( new Rect (point_1.x, point_1.y, 1, Mathf.Abs(h)));
            }
            else if (46 <= angle && angle <= 90) 
            {
                //Debug.Log("2");
                halls.Add( new Rect (point_1.x, point_1.y, Mathf.Abs (w), 1));
            }
            else if (91 <= angle && angle <= 135) 
            {
               // Debug.Log("3");
                halls.Add(new Rect (point_1.x, point_1.y, Mathf.Abs (w), 1));
            }

            else 
            {
               // Debug.Log("4");
                halls.Add( new Rect(point_2.x, point_2.y,1,  Mathf.Abs(h)));
            }   

        }
        // -
        else 
        {
            if (-1 >= angle && angle >= -45) 
            {
               // Debug.Log("5");
                halls.Add(new Rect (point_1.x, point_1.y, 1,  Mathf.Abs(h)));
            }
            else if (-46 >= angle && angle >= -90) 
            {
               // Debug.Log("6");
                halls.Add( new Rect (point_2.x, point_2.y, Mathf.Abs (w), 1));
            }
            else if (-91 >= angle && angle >= -135) 
            {
               // Debug.Log("7");
                halls.Add( new Rect (point_2.x, point_2.y, Mathf.Abs (w), 1));
            }

            else 
            {
                //Debug.Log("8");
                        halls.Add( new Rect(point_2.x, point_2.y, 1,  Mathf.Abs(h)));
            }   
        }
        
    }
	public Leaf LastLeftLeaf()
	{
		if(leftChild != null)
			return rightChild.LastLeftLeaf();

		else
			return this;
	}
	public Leaf LastRightLeaf()
	{
		if(rightChild != null)
			return leftChild.LastLeftLeaf();

		else
			return this;
	}



	public bool CoinFlip()
	{
		return (Random.value >0.5f);
	}


}


