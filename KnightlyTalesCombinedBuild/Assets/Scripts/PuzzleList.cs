using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PuzzleList : MonoBehaviour {
	public List<GameObject> TempTile;
	public List<GameObject> PuzzlePiece;
	public List<PuzzleRow> puzzleRowList = new List<PuzzleRow>();
	public List<PuzzleArea> puzzleAreaList = new List<PuzzleArea>();
	// Use this for initialization
	void Start () {
		
		// -1 are empty spaces. -2 are for points to make blocking walls extend to the wall of the room
		puzzleRowList.Add(new PuzzleRow(new int[]{-2,-1,-2},0));
		puzzleRowList.Add(new PuzzleRow(new int[]{-1,0,-1},1));

		//
		puzzleAreaList.Add(new PuzzleArea( 3,1, 0,1));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
			

}

[System.Serializable]
public class PuzzleRow
{
	public int[] PuzzlePieceId;
	public int rowId;
	public int length;
	public PuzzleRow(int[] IdList, int  RowId )
	{
		length = IdList.Length;
		PuzzlePieceId  = IdList;
		rowId = RowId;
	}
}
[System.Serializable]
public class PuzzleArea
{
	public List<PuzzleRow> combinedRows = new List<PuzzleRow>();
	public delegate void AddToList ()  ;
	public AddToList ListManger ;
	// height from the first row to the back of the room to complete puzzle
	public int topBufferHeight;
	// height from the last row to the entacne to complete the puzzle
	public int bottomBufferHeight;
	// row count
	public int height;
	public int length = 0;
	PuzzleList pList = GameObject.FindObjectOfType<PuzzleList>();


	public PuzzleArea(int TopBufferHeight,int BottomBufferHeight , params int[] values )
	{
		topBufferHeight = TopBufferHeight;
		bottomBufferHeight =BottomBufferHeight;
		for(int i=0; i< values.Length;i++)
		{
			if(pList.puzzleRowList[values[i]].length > length)
				length = pList.puzzleRowList[values[i]].length;
			
			combinedRows.Add(pList.puzzleRowList[values[i]]);
		}
		height = combinedRows.Count;
	}



}
