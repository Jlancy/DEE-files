using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class MapSave : MonoBehaviour {

	public  List<MapInfo> MapList = new List<MapInfo>();
	public GameObject block;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.E))
			SaveMapData();

		if(Input.GetKeyDown(KeyCode.R))
			LoadMapData();
		if(Input.GetKeyDown(KeyCode.T))
			draw();
	}

	void SaveMapData()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath+ "/MapSave.dat");
		List<MapInfo> data = new List<MapInfo>(MapList);
		bf.Serialize (file ,data);
		file.Close();
		
	}
	void LoadMapData()
	{
		if( File.Exists(Application.persistentDataPath+ "/MapSave.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath+ "/MapSave.dat",FileMode.Open);
			List<MapInfo> data = (List<MapInfo>)bf.Deserialize(file);
			MapList = new List<MapInfo>(data);
		}
		
	}

	void draw()
	{
		// just to visually see rooms for now
		//Color colorArea = new Color32( (byte)Random.Range(0,255), (byte)Random.Range(0,255), (byte)Random.Range(0,255),255);

		// start postion 
		Vector2 CurrentPos =new Vector2( MapList[0].x,  MapList[0].y);
		Color ColorArea;
		// build room
		int i = 0;
		for(int row = 0; row < MapList[0].width; row++ )
		{
			
			for(int col = 0; col < MapList[0].height; col++)
			{
				
				GameObject tile;
				tile = Instantiate(block);
				tile.transform.parent = this.transform;
				//Debug.Log(CurrentPos);
				if(MapList[0].TileID[i] ==1)
					ColorArea = Color.black;
				else
					ColorArea =Color.white;

				tile.GetComponent<SpriteRenderer>().color = ColorArea;
				tile.transform.position = CurrentPos;
				CurrentPos = CurrentPos + new Vector2(0,1);
				i++;
			}
			CurrentPos = CurrentPos + new Vector2(1,-MapList[0].height);
		}


	}
}

[System.Serializable]

public class MapInfo {

	public List<int> TileID;
	public int width;
	public int height;
	public int x;
	public int y;

	public MapInfo()
	{
		TileID = new List<int>();
		width = 0;
		height =0;
		x = 0;
		y = 0;
	
	}

	public MapInfo(int[,] MapArray, Vector2 StartPoint)
	{
		TileID= new List<int>();
		width = MapArray.GetLength(0);
		height = MapArray.GetLength(1);
		x = (int)StartPoint.x;
		y = (int)StartPoint.y;
	
		for(int w = 0; w< width; w++)
		{
			for(int h=0 ; h < height;h++) 
			{
				TileID.Add(MapArray[w,h]);
			}	
		}
		
	}

}