using UnityEngine;
using System.Collections;

[System.Serializable]

public class Room
{ 
	public int x;
	public int y;
	public int width;
	public int height;



	public Room()
	{
	}
	public Room(int X, int Y , int Width, int Height)
	{
		x =X;
		y = Y;
		width =Width;
		height =Height;
	}



}
