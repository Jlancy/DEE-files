using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class Leaf   {

	public const int MIN_LEAF_SIZE = 10;
	public const int MAX_LEAF_SIZE = 20;
	public int x;
	public int y; 
	public int width;
	public int height;
	public Leaf leftChild;
	public Leaf rightChild;
	public Rect room;
	public Vector2 hall;



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
	public bool CoinFlip()
	{
		return (Random.value >0.5f);
	}


}
