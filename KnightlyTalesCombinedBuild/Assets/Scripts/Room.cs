using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]

public class Room
{ 
	public int x;
	public int y;
	public int width;
	public int height;
	public Vector2 centerPoint;
    public Vector2 boardVector;
   [System.NonSerialized]
    public List<Room> adjacentRoom = new List<Room>();
   
    public bool endRoom;


	public Room()
	{
        boardVector = new Vector2(0, 0);
	}


	public Room(int X, int Y , int Width, int Height)
	{
		x =X;
		y = Y;
		width =Width;
		height =Height;
        centerPoint = new Vector2((int)(X+Width / 2), (int)(Y+Height / 2));
        endRoom = false;     
	}

  



   

	public static Rect BuildHallway(Room l, Room r)
	{
        //Vector2 point_1 = new Vector2 (l.x, l.y);
        //Vector2 point_2 = new Vector2 (r.x, r.y);

        Vector2 point_1 = new Vector2(l.centerPoint.x, l.centerPoint.y);
        Vector2 point_2 = new Vector2(r.centerPoint.x, r.centerPoint.y);
        //Vector2 point_1 = new Vector2 (l.x, l.y);
        //Vector2 point_2 = new Vector2 (r.x, r.y);

        l.adjacentRoom.Add(r);
        r.adjacentRoom.Add(l);

		float  w = point_2.x - point_1.x;
		float  h = point_2.y - point_1.y;
		float angle = Mathf.Atan2(w,h) * (180/ Mathf.PI);

		
        if (angle >= 0 && angle<180) 
		{
			if (0 <= angle && angle <= 45) 
			{
               
                return new Rect (point_1.x, point_1.y, 1, Mathf.Abs(h));
			}
			else if (46 <= angle && angle <= 90) 
			{
                
                return new Rect (point_1.x, point_1.y, Mathf.Abs (w), 1);
			}
            else if (91 <= angle && angle <= 135) 
            {
                
                return new Rect (point_1.x, point_1.y, Mathf.Abs (w), 1);
            }

			else 
			{
             
                return new Rect(point_2.x, point_2.y,1,  Mathf.Abs(h));
			}	

		}
		// -
		else 
		{
            if (-1 >= angle && angle >= -45) 
            {

                return new Rect (point_1.x, point_1.y, 1,  Mathf.Abs(h));
            }
            else if (-46 >= angle && angle >= -90) 
            {

                return new Rect (point_2.x, point_2.y, Mathf.Abs (w), 1);
            }
            else if (-91 >= angle && angle >= -135) 
            {
               
                return new Rect (point_2.x, point_2.y, Mathf.Abs (w), 1);
            }

            else 
            {
                
                return new Rect(point_2.x, point_2.y, 1,  Mathf.Abs(h));
            }   
		}
	}

	

}
