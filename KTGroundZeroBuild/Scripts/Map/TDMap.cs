using UnityEngine;	//Needed for Random at line ~141
using System.Collections.Generic;	//Needed for queue

public struct Coord{
	public int x_coord;
	public int y_coord;
}

public enum Doodad : int{Dirt, Gate};

public class TDMap {

	//==========================================================================
	//Variables
	//==========================================================================
	private TDTile[,] _tileData;
	private int _size_x;
	private int _size_y;
	//==========================================================================
	//Constructor, Get, and Set Functions
	//==========================================================================
	//Constructer
	public TDMap(int size_x, int size_y, MapType mapType){
		_size_x = size_x;
		_size_y = size_y;
		_tileData = new TDTile[_size_x, _size_y];
		for (int y = 0; y < size_y; y++)
			for (int x = 0; x < size_x; x++)
				_tileData [x, y] = new TDTile(0);
		//GenerateForestAsWhole();

		switch (mapType) {
		case MapType.Forest:
			GenerateForestAsWhole();
			break;
		case MapType.Cave:
			GenerateCave();
			break;
		case MapType.ForestByChunk:
			GenerateForestByChunks();
			break;
		default:
			break;
		}

	}
	//Get an individual tile
	public int GetTileAt(int x_coord, int y_coord){
		if (x_coord < 0 || x_coord >= _size_x || y_coord < 0 || y_coord >= _size_y)
			return -1;
		else
			return _tileData [x_coord, y_coord].GetTileTypeAsInt();
	}
	//TDTIle translation of above
	public TDTile GetTileDataAt(int x_coord, int y_coord){
		if (x_coord < 0 || x_coord >= _size_x || y_coord < 0 || y_coord >= _size_y)
			return new TDTile(TileType.Empty);
		else
			return _tileData[x_coord, y_coord];
	}

	//Get the map data as int for analysis 
	public int[,] GetMapDataAsInt(){
		int[,] mapData = new int[_size_x, _size_y];
		for (int y = 0; y < _size_y; y++)
			for (int x = 0; x < _size_x; x++)
				mapData [x, y] = _tileData [x, y].GetTileTypeAsInt();
		return mapData;
	}
	//TDTIle translation of above
	public TDTile[,] GetMapDataAsTileData(){
		TDTile[,] tileData = new TDTile[_size_x, _size_y];
		for (int y = 0; y < _size_y; y++)
			for (int x = 0; x < _size_x; x++)
				tileData[x, y] = _tileData[x, y];
		return tileData;
	}

	//Set the map data with an array of int
	public void SetMapData(int[,] mapData){
		_size_x = mapData.GetLength (0);
		_size_y = mapData.GetLength (1);
		_tileData = new TDTile[_size_x, _size_y];
		for (int y = 0; y < _size_y; y++)
			for (int x = 0; x < _size_x; x++)
				_tileData[x, y].SetTDTile(mapData[x, y]);
	}
	//TDTIle translation of above
	public void SetMapData(TDTile[,] tileData){
		_size_x = tileData.GetLength (0);
		_size_y = tileData.GetLength (1);
		_tileData = new TDTile[_size_x, _size_y];
		for (int y = 0; y < _size_y; y++)
			for (int x = 0; x < _size_x; x++)
			_tileData[x, y] = tileData[x, y];
	}

	//Get the map data as int for analysis 
	public int[,] GetCollisionMap(){
		int[,] collisionMap = new int[_size_x, _size_y];
		for (int y = 0; y < _size_y; y++)
			for (int x = 0; x < _size_x; x++)
				collisionMap [x, y] = _tileData [x, y].GetTileTraitAsInt();
		return collisionMap;
	}

	//==========================================================================
	//Insert different types of maps here
	//==========================================================================
	public void GenerateForestByChunks(){
		TDTile[,] chunkData;
		chunkData = new TDTile[MAP_CONSTANT.CHUNK_WIDTH, MAP_CONSTANT.CHUNK_HEIGHT];
		FillMap (new TDTile(TileType.Grass));
		RingGenerator (_tileData, new TDTile(TileType.TreeOnGrass), 0);
		GenerateChunkInSpiral (chunkData, new TDTile(TileType.TreeOnGrass), 50, 0);
		FillUnreachable (_tileData, new TDTile(TileType.Grass), new TDTile(TileType.TreeOnGrass));
		FillWithTile (_tileData, new TDTile(TileType.Grass), Doodad.Dirt, 2 * 1 + 2 * 1, 2, 2);
		FillWithTile (_tileData, new TDTile(TileType.Grass), Doodad.Dirt, 2 * 2 + 2 * 2, 3, 3);	
	}
//Method currently used
	public void GenerateForestAsWhole(){
		TDTile[] treeSet = new TDTile[2];
		treeSet [0] = new TDTile (TileType.TreeOnGrass);
		treeSet [1] = new TDTile (TileType.AppleTree);
		//Debug.Log (treeSet.GetLength (0));
		//Debug.Log (Random.Range(0, treeSet.GetLength (0)));
		FillMap (new TDTile(TileType.Grass));
		RingGenerator (_tileData, treeSet, 0);
		//RingGenerator (_tileData, new TDTile(TileType.TreeOnGrass), 0);
		PlaceDoodad (_tileData, Doodad.Gate, (int)Random.Range (_size_x / 2 - 5, _size_x / 2 + 5), _size_y - 1, 2, 1);
		SpiralGeneration (_tileData, treeSet, 40, 0);
		//SpiralGeneration (_tileData, new TDTile(TileType.TreeOnGrass), 40, 0);
		
		FillUnreachable (_tileData, new TDTile(TileType.Grass), treeSet);
		GenerateRandomMass (_tileData, Doodad.Dirt, _size_x / 2, _size_y / 2, 50, 0, 5);
		//Debug.Log (Mathf.Sqrt((_size_x/2 + 3)*(_size_x/2 + 3) + (_size_y/2 + 4)*(_size_y/2 + 4)));
		//FillUnreachable (_tileData, new TDTile(TileType.Grass), new TDTile(TileType.TreeOnGrass));
		FillWithTile (_tileData, new TDTile(TileType.Grass), Doodad.Dirt, 2 * 2 + 2 * 2, 2, 2);
		FillWithTile (_tileData, new TDTile(TileType.Grass), Doodad.Dirt, 2 * 3 + 2 * 3, 3, 3);		
	}
	public void GenerateCave(){
		FillMap (new TDTile(TileType.Dirt));
		RingGenerator (_tileData, new TDTile(TileType.Lava), 0);
		SpiralGeneration (_tileData, new TDTile(TileType.Lava), 20, 5);
		FillUnreachable (_tileData, new TDTile(TileType.Dirt), new TDTile(TileType.Lava));
	}

//////////////////////////////////////////////////////////////////////////////////////////////
/*===================TDTileConversion=======================================================*/
//////////////////////////////////////////////////////////////////////////////////////////////
	

	//==========================================================================
	//FillUnreachable() and its helpers
	//==========================================================================

	public void FillUnreachable(TDTile[,] area, TDTile scanTarget, TDTile replaceTarget){
		//This list will hold the topleft coord of the space of 'scanTarget' or grass
		List<Coord> topLeftCoords = new List<Coord> ();		
		//This Queue is used later to fill the isolated spaces
		Queue<Coord> fillTheseCoords = new Queue<Coord>();
		//First, we find the largest area and that will be the main play area
		Largest_Space (area, scanTarget, topLeftCoords);
		//For every coords left, enqueue them to have them filled
		for (int i = 0; i < topLeftCoords.Count; i++)
			fillTheseCoords.Enqueue (topLeftCoords [i]);
		//Fill the isolated spaces until there are none left in the queue to be filled
		while(fillTheseCoords.Count != 0)
			FillIsolated (area, fillTheseCoords, scanTarget, replaceTarget);
		FillIsolatedPoints (area, replaceTarget);
	}
	//Array Version
	public void FillUnreachable(TDTile[,] area, TDTile scanTarget, TDTile[] replaceTarget){
		//This list will hold the topleft coord of the space of 'scanTarget' or grass
		List<Coord> topLeftCoords = new List<Coord> ();		
		//This Queue is used later to fill the isolated spaces
		Queue<Coord> fillTheseCoords = new Queue<Coord>();
		//First, we find the largest area and that will be the main play area
		Largest_Space (area, scanTarget, topLeftCoords);
		//For every coords left, enqueue them to have them filled
		for (int i = 0; i < topLeftCoords.Count; i++)
			fillTheseCoords.Enqueue (topLeftCoords [i]);
		//Fill the isolated spaces until there are none left in the queue to be filled
		while(fillTheseCoords.Count != 0)
			FillIsolated (area, fillTheseCoords, scanTarget, replaceTarget);
		FillIsolatedPoints (area, replaceTarget);
	}

	public int Largest_Space(TDTile[,] area, TDTile scanTarget, List<Coord> topLeftCoords){
		int[,] scanArea = new int[area.GetLength (0),area.GetLength (1)];
		int largestCount = 0;
		int currentCount = 0;
		Queue<Coord> coordQ = new Queue<Coord> ();
		Coord current;
		//This coord will be removed at the end of this function
		Coord largestCoord;
		largestCoord.x_coord = 0;
		largestCoord.y_coord = 0;
		//Set scanArea to area so that we can easily scan the map
		for (int y = 0; y < scanArea.GetLength(1); y++) {
			for (int x = 0; x < scanArea.GetLength(0); x++) {
				scanArea[x, y] = area[x, y].GetTileTypeAsInt();
			}
		}
		//Scan the map for the 'scanTarget', when one is found, inspect its area and then compare sizes
		//-1 signifies that the tile has been scaned
		for (int y = 0; y < scanArea.GetLength(1); y++) {
			for (int x = 0; x < scanArea.GetLength(0); x++){
				
				if(scanArea[x, y] == scanTarget.GetTileTypeAsInt()){
					currentCount = 0;
					current.x_coord = x;
					current.y_coord = y;
					scanArea[x, y] = -1;
					
					coordQ.Enqueue (current);
					topLeftCoords.Add (current);
					currentCount++;
					//Keep scan until queue is empty
					while(coordQ.Count != 0){
						ScanMap (scanArea, coordQ, scanTarget, ref currentCount);
					}
					//Compare if this area is the largest
					if(currentCount > largestCount){
						largestCoord.x_coord = x;
						largestCoord.y_coord = y;
						largestCount = currentCount;
					}
				}
			}
		}
		//Remove the coord responsible for the largest from the list 
		topLeftCoords.Remove (largestCoord);
		return largestCount;
	}
	//Scan neighboring tiles, if it matches the scanTarget, mark and enqueue. Also increment the count
	private void ScanMap(int[,] area, Queue<Coord> coordQ, TDTile scanTarget, ref int currentCount){
		Coord current = coordQ.Dequeue ();
		Coord nextPoint;
		//Scan left
		if (current.x_coord > 0) {
			if (area[current.x_coord - 1, current.y_coord] == scanTarget.GetTileTypeAsInt()){
				nextPoint.x_coord = current.x_coord - 1;
				nextPoint.y_coord = current.y_coord;
				area[nextPoint.x_coord, nextPoint.y_coord] = -1;
				coordQ.Enqueue (nextPoint);
				currentCount++;
			}
		}
		//Scan right
		if (current.x_coord < area.GetLength(0) - 1) {
			if (area[current.x_coord + 1, current.y_coord] == scanTarget.GetTileTypeAsInt()){
				nextPoint.x_coord = current.x_coord + 1;
				nextPoint.y_coord = current.y_coord;
				area[nextPoint.x_coord, nextPoint.y_coord] = -1;
				coordQ.Enqueue (nextPoint);
				currentCount++;
			}
		}
		//Scan up
		if (current.y_coord > 0) {
			if (area[current.x_coord, current.y_coord - 1] == scanTarget.GetTileTypeAsInt()){
				nextPoint.x_coord = current.x_coord;
				nextPoint.y_coord = current.y_coord - 1;
				area[nextPoint.x_coord, nextPoint.y_coord] = -1;
				coordQ.Enqueue (nextPoint);
				currentCount++;
			}
		}
		//Scan down
		if (current.y_coord < area.GetLength(1) - 1) {
			if (area[current.x_coord, current.y_coord + 1] == scanTarget.GetTileTypeAsInt()){
				nextPoint.x_coord = current.x_coord;
				nextPoint.y_coord = current.y_coord + 1;
				area[nextPoint.x_coord, nextPoint.y_coord] = -1;
				coordQ.Enqueue (nextPoint);
				currentCount++;
			}
		}
	}
	//Similar to ScanMap(), but interacts with the actual map so that changes can be made. replace scanTargets
	//to replaceTargets
	private void FillIsolated(TDTile[,] area, Queue<Coord> topLeftCoords, TDTile scanTarget, TDTile replaceTarget){
		Coord current = topLeftCoords.Dequeue ();
		Coord nextPoint;
		//Scan left
		if (current.x_coord > 0) {
			if (area[current.x_coord - 1, current.y_coord] == scanTarget){
				nextPoint.x_coord = current.x_coord - 1;
				nextPoint.y_coord = current.y_coord;
				area[nextPoint.x_coord, nextPoint.y_coord] = replaceTarget;
				topLeftCoords.Enqueue (nextPoint);
			}
		}
		//Scan right
		if (current.x_coord < area.GetLength(0) - 1) {
			if (area[current.x_coord + 1, current.y_coord] == scanTarget){
				nextPoint.x_coord = current.x_coord + 1;
				nextPoint.y_coord = current.y_coord;
				area[nextPoint.x_coord, nextPoint.y_coord] = replaceTarget;
				topLeftCoords.Enqueue (nextPoint);
			}
		}
		//Scan up
		if (current.y_coord > 0) {
			if (area[current.x_coord, current.y_coord - 1] == scanTarget){
				nextPoint.x_coord = current.x_coord;
				nextPoint.y_coord = current.y_coord - 1;
				area[nextPoint.x_coord, nextPoint.y_coord] = replaceTarget;
				topLeftCoords.Enqueue (nextPoint);
			}
		}
		//Scan down
		if (current.y_coord < area.GetLength(1) - 1) {
			if (area[current.x_coord, current.y_coord + 1] == scanTarget){
				nextPoint.x_coord = current.x_coord;
				nextPoint.y_coord = current.y_coord + 1;
				area[nextPoint.x_coord, nextPoint.y_coord] = replaceTarget;
				topLeftCoords.Enqueue (nextPoint);
			}
		}
	}
	//Array Verson
	private void FillIsolated(TDTile[,] area, Queue<Coord> topLeftCoords, TDTile scanTarget, TDTile[] replaceTargets){
		Coord current = topLeftCoords.Dequeue ();
		Coord nextPoint;
		//Scan left
		if (current.x_coord > 0) {
			if (area[current.x_coord - 1, current.y_coord] == scanTarget){
				nextPoint.x_coord = current.x_coord - 1;
				nextPoint.y_coord = current.y_coord;
				area[nextPoint.x_coord, nextPoint.y_coord] = replaceTargets[(int)Random.Range (0, replaceTargets.GetLength(0))];
				topLeftCoords.Enqueue (nextPoint);
			}
		}
		//Scan right
		if (current.x_coord < area.GetLength(0) - 1) {
			if (area[current.x_coord + 1, current.y_coord] == scanTarget){
				nextPoint.x_coord = current.x_coord + 1;
				nextPoint.y_coord = current.y_coord;
				area[nextPoint.x_coord, nextPoint.y_coord] = replaceTargets[(int)Random.Range (0, replaceTargets.GetLength(0))];
				topLeftCoords.Enqueue (nextPoint);
			}
		}
		//Scan up
		if (current.y_coord > 0) {
			if (area[current.x_coord, current.y_coord - 1] == scanTarget){
				nextPoint.x_coord = current.x_coord;
				nextPoint.y_coord = current.y_coord - 1;
				area[nextPoint.x_coord, nextPoint.y_coord] = replaceTargets[(int)Random.Range (0, replaceTargets.GetLength(0))];
				topLeftCoords.Enqueue (nextPoint);
			}
		}
		//Scan down
		if (current.y_coord < area.GetLength(1) - 1) {
			if (area[current.x_coord, current.y_coord + 1] == scanTarget){
				nextPoint.x_coord = current.x_coord;
				nextPoint.y_coord = current.y_coord + 1;
				area[nextPoint.x_coord, nextPoint.y_coord] = replaceTargets[(int)Random.Range (0, replaceTargets.GetLength(0))];
				topLeftCoords.Enqueue (nextPoint);
			}
		}
	}


	//Fills in points with 3 or more bording tiles
	//An attempt to remove unreachable area
	private void FillIsolatedPoints(TDTile[,] area, TDTile replaceTarget)
	{
		for (int x = 1; x < area.GetLength(0) - 1; x++)
			for (int y = 1; y < area.GetLength(1) - 1; y++)
				if (DetectSimilarBorderTiles(area, replaceTarget, x, y) >= 3)
					area[x, y] = replaceTarget;
		
		for (int x = area.GetLength(0) - 2; x > 0; x--)
			for (int y = area.GetLength(1) - 2; y > 0; y--)
				if (DetectSimilarBorderTiles(area, replaceTarget, x, y) >= 3)
					area[x, y] = replaceTarget;
	}
	//Array Version
	private void FillIsolatedPoints(TDTile[,] area, TDTile[] replaceTargets)
	{
		for (int x = 1; x < area.GetLength(0) - 1; x++)
			for (int y = 1; y < area.GetLength(1) - 1; y++)
				if (DetectSimilarBorderTiles(area, replaceTargets, x, y) >= 3)
					area[x, y] = replaceTargets[(int)Random.Range (0, replaceTargets.GetLength(0))];
		
		for (int x = area.GetLength(0) - 2; x > 0; x--)
			for (int y = area.GetLength(1) - 2; y > 0; y--)
				if (DetectSimilarBorderTiles(area, replaceTargets, x, y) >= 3)
					area[x, y] = replaceTargets[(int)Random.Range (0, replaceTargets.GetLength(0))];
	}
	//==========================================================================
	//Functions taken from MapGenerator; General Tool Functions
	//==========================================================================
	//Fill the map of a single type of tile
	private void FillMap(TDTile replaceTarget){
		//For every point in the map,
		for (int y = 0; y < _size_y; y++){
			for (int x = 0; x < _size_x; x++)
				//that point will equal to the designated fill.
				_tileData[x, y] = replaceTarget;
		}
	}
	//Fill the map of a multiple types of tile
	//Array version
	private void FillMap(TDTile[] replaceTargets){
		//For every point in the map,
		for (int y = 0; y < _size_y; y++){
			for (int x = 0; x < _size_x; x++)
				//that point will equal to the designated fill.
				_tileData[x, y] = replaceTargets[(int)Random.Range (0, replaceTargets.GetLength(0))];
		}
	}


	//Ring generator starting from the top-left and then generates clockwise
	private void RingGenerator(TDTile[,] area, TDTile replaceTarget, int increment){
		//This function will use an incrementing system so that we can navagate the map
		//Expand from there by density and build it spirally
		//Top
		for (int x = increment; x < area.GetLength(0) - increment; x++) {
			area[x, increment] = replaceTarget;
			area[x, area.GetLength(1) - 1 - increment] = replaceTarget;
		}
		//Right
		for (int y = increment; y < area.GetLength(1) - increment; y++) {
			area[increment, y] = replaceTarget;
			area[area.GetLength (0) - 1 - increment, y] = replaceTarget;
		}
	}
	//Ring generator starting from the top-left and then generates clockwise
	//Array version
	private void RingGenerator(TDTile[,] area, TDTile[] replaceTargets, int increment){
		//This function will use an incrementing system so that we can navagate the map
		//Expand from there by density and build it spirally
		//Top
		for (int x = increment; x < area.GetLength(0) - increment; x++) {
			area[x, increment] = replaceTargets[(int)Random.Range (0, replaceTargets.GetLength(0))];
			area[x, area.GetLength(1) - 1 - increment] = replaceTargets[(int)Random.Range (0, replaceTargets.GetLength(0))];
		}
		//Right
		for (int y = increment; y < area.GetLength(1) - increment; y++) {
			area[increment, y] = replaceTargets[(int)Random.Range (0, replaceTargets.GetLength(0))];
			area[area.GetLength (0) - 1 - increment, y] = replaceTargets[(int)Random.Range (0, replaceTargets.GetLength(0))];
		}
	}


	//Spiral generation starting from the top-left and then generation clockwise
	private void SpiralGeneration(TDTile[,] area, TDTile replaceTarget, int kFactor, int cFactor){
		//This function will use an incrementing system so that we can navagate the map
		int increment = 1;
		bool space = true;
		//Expand from there by density and build it spirally
		do
		{
			//Top
			for (int x = increment; x < area.GetLength(0) - 1 - increment; x++)
				GenerateTileByRelation(area, replaceTarget, 
				                       x, 			//x-coord
				                       increment, 	//y-coord
				                       kFactor, cFactor);
			//Right
			for (int y = increment; y < area.GetLength(1) - 1 - increment; y++)
				GenerateTileByRelation(area, replaceTarget, 
				                       area.GetLength(0) - 1 - increment,	//x-coord
				                       y, 									//y-coord
				                       kFactor, cFactor);
			//Bottom
			for (int x = area.GetLength(0) - 1 - increment; x >= increment; x--)
				GenerateTileByRelation(area, replaceTarget, 
				                       x, 									//x-coord
				                       area.GetLength(1) - 1 - increment, 	//y-coord
				                       kFactor, cFactor);
			//Left
			for (int y = area.GetLength(1) - 1 - increment - 1; y >= increment + 1; y--)
				GenerateTileByRelation(area, replaceTarget, 
				                       increment,	//x-coord
				                       y, 			//y-coord
				                       kFactor, cFactor);
			//Increment or stop?
			if (2 * increment > area.GetLength(0) || 2 * increment > area.GetLength(1))
				space = false;
			else
				increment++;
		} while (space);
	}
	//Spiral generation starting from the top-left and then generation clockwise
	//Array Version
	private void SpiralGeneration(TDTile[,] area, TDTile[] replaceTargets, int kFactor, int cFactor){
		//This function will use an incrementing system so that we can navagate the map
		int increment = 1;
		bool space = true;
		//Expand from there by density and build it spirally
		do
		{
			//Top
			for (int x = increment; x < area.GetLength(0) - 1 - increment; x++)
				GenerateTileByRelation(area, replaceTargets, 
				                       x, 			//x-coord
				                       increment, 	//y-coord
				                       kFactor, cFactor);
			//Right
			for (int y = increment; y < area.GetLength(1) - 1 - increment; y++)
				GenerateTileByRelation(area, replaceTargets, 
				                       area.GetLength(0) - 1 - increment,	//x-coord
				                       y, 									//y-coord
				                       kFactor, cFactor);
			//Bottom
			for (int x = area.GetLength(0) - 1 - increment; x >= increment; x--)
				GenerateTileByRelation(area, replaceTargets, 
				                       x, 									//x-coord
				                       area.GetLength(1) - 1 - increment, 	//y-coord
				                       kFactor, cFactor);
			//Left
			for (int y = area.GetLength(1) - 1 - increment - 1; y >= increment + 1; y--)
				GenerateTileByRelation(area, replaceTargets, 
				                       increment,	//x-coord
				                       y, 			//y-coord
				                       kFactor, cFactor);
			//Increment or stop?
			if (2 * increment > area.GetLength(0) || 2 * increment > area.GetLength(1))
				space = false;
			else
				increment++;
		} while (space);
	}



	
	//Spiral generation starting from the top-left and then generation clockwise
	private void InverseSpiralGeneration(TDTile[,] area, TDTile replaceTarget, int x_coord, int y_coord, int kFactor, int cFactor, int radius){
		//Expand from there by density and build it spirally
		for(int i = 1; i < radius; i++){
			//Top
			for (int x = x_coord - i + 1; x <= x_coord + i - 1; x++)
				if(SafeCoord(area, x, y_coord + i) && ContainedInCircle(x_coord, y_coord, x, y_coord + i, radius))
					GenerateTileByRelation(area, replaceTarget, 
					                       x, 				//x-coord
					                       y_coord + i, 	//y-coord
					                       kFactor, cFactor);
			//TopRight
			if(SafeCoord(area, x_coord + i, y_coord + i) && ContainedInCircle(x_coord, y_coord, x_coord + i, y_coord + i, radius))
				GenerateTileByRelation(area, replaceTarget, 
				                       x_coord + i, 		//x-coord
				                       y_coord + i,			//y-coord
				                       kFactor, cFactor);
			//Right
			for (int y = y_coord + i - 1; y >= y_coord - i + 1; y--)
				if(SafeCoord(area, x_coord + i, y) && ContainedInCircle(x_coord, y_coord, x_coord + i, y, radius))				
					GenerateTileByRelation(area, replaceTarget, 
					                       x_coord + i, 	//x-coord
					                       y, 				//y-coord
					                       kFactor, cFactor);
			//BottomRight
			if(SafeCoord(area, x_coord + i, y_coord + i) && ContainedInCircle(x_coord, y_coord, x_coord + i, y_coord + i, radius))
				GenerateTileByRelation(area, replaceTarget, 
				                       x_coord + i, 		//x-coord
				                       y_coord - i,			//y-coord
				                       kFactor, cFactor);
			//Bottom
			for (int x = x_coord + i - 1; x >= x_coord - i + 1; x--)
				if(SafeCoord(area, x, y_coord - i) && ContainedInCircle(x_coord, y_coord, x, y_coord - i, radius))				
					GenerateTileByRelation(area, replaceTarget, 
					                       x, 				//x-coord
					                       y_coord - i, 	//y-coord
					                       kFactor, cFactor);
			//BottomLeft
			if(SafeCoord(area, x_coord + i, y_coord + i) && ContainedInCircle(x_coord, y_coord, x_coord + i, y_coord + i, radius))
				GenerateTileByRelation(area, replaceTarget, 
				                       x_coord - i, 		//x-coord
				                       y_coord - i,			//y-coord
				                       kFactor, cFactor);
			//Left
			for (int y = y_coord - i + 1; y <= y_coord + i - 1; y++)
				if(SafeCoord(area, x_coord - i, y) && ContainedInCircle(x_coord, y_coord, x_coord - i, y, radius))				
					GenerateTileByRelation(area, replaceTarget, 
					                       x_coord - i, 	//x-coord
					                       y, 				//y-coord
					                       kFactor, cFactor);
			//TopLeft
			if(SafeCoord(area, x_coord + i, y_coord + i) && ContainedInCircle(x_coord, y_coord, x_coord + i, y_coord + i, radius))
				GenerateTileByRelation(area, replaceTarget, 
				                       x_coord - i, 		//x-coord
				                       y_coord + i,			//y-coord
				                       kFactor, cFactor);
		}
		
	}



	//Based on the tiles around the selected point, a fill will take its place
	private void GenerateTileByRelation( TDTile[,] area, TDTile replaceTarget, int x_coord, int y_coord, int kFactor, int cFactor){
		//(Modifier x Neighboring Trees) / 100 
		if (((kFactor * DetectSimilarBorderTiles(area, replaceTarget, x_coord, y_coord)) + cFactor) > (int)Random.Range(0,100) &&
		    x_coord > 0 && x_coord < area.GetLength (0) - 1 && y_coord > 0 && y_coord < area.GetLength (1) - 1)
			area[x_coord, y_coord] = replaceTarget;
	}
	//Array Version
	private void GenerateTileByRelation( TDTile[,] area, TDTile[] replaceTargets, int x_coord, int y_coord, int kFactor, int cFactor){
		//(Modifier x Neighboring Trees) / 100 
		if (((kFactor * DetectSimilarBorderTiles(area, replaceTargets, x_coord, y_coord)) + cFactor) > (int)Random.Range(0,100) &&
		    x_coord > 0 && x_coord < area.GetLength (0) - 1 && y_coord > 0 && y_coord < area.GetLength (1) - 1)
			area[x_coord, y_coord] = replaceTargets[(int)Random.Range (0, replaceTargets.GetLength(0))];
	}
//Doodad may have their own struct later on and moved into a global
	private void PlaceDoodad(TDTile[,] area, Doodad fill, int x_coord, int y_coord, int size_x, int size_y){
		if (area.GetLength (0) > x_coord + size_x - 1 && area.GetLength (1) > y_coord + size_y - 1) {
			switch(fill){
			case Doodad.Gate:
				area[x_coord, y_coord] = new TDTile(TileType.NorthGate1);
				area[x_coord + 1, y_coord] = new TDTile(TileType.NorthGate2);
				break;
			default:
				break;
			}
		}
	}

	//Reserved a plot of land for example a larger tree or a building or a bandit camp
	private void FillWithTile(TDTile[,] area, TDTile scanTarget, Doodad fill, int acceptedClearance, int size_x, int size_y){
		int x_coord = 0;
		int y_coord = 0;
		int perimeter = 2 * size_x + 2 * size_y;
		bool clearance = false;

		if(acceptedClearance <= perimeter){
			do{
				x_coord = (int)Random.Range (0, _tileData.GetLength(0) - size_x);
				y_coord = (int)Random.Range (0, _tileData.GetLength(1) - size_y);
				if(ScanEdge(area, scanTarget, size_x, size_y, x_coord, y_coord) >= acceptedClearance)
					clearance = true;
			}while(!clearance);
			if(clearance){
				//Check
				PlaceDoodadHolder (area, size_x, size_y, x_coord, y_coord);

				switch(fill){
				case Doodad.Dirt:
					area[x_coord, y_coord].SetTDTile(TileType.DirtToGrassBL);
					area[x_coord + size_x - 1, y_coord].SetTDTile(TileType.DirtToGrassBR);
					area[x_coord, y_coord + size_y - 1].SetTDTile(TileType.DirtToGrassTL);
					area[x_coord + size_x - 1, y_coord + size_y - 1].SetTDTile(TileType.DirtToGrassTR);
					for (int y = y_coord + 1; y < y_coord + size_y - 1; y++){
						area[x_coord, y].SetTDTile(TileType.DirtToGrassL);
						area[x_coord + size_x - 1 , y].SetTDTile(TileType.DirtToGrassR);
					}
					for (int x = x_coord + 1; x < x_coord + size_x - 1; x++){
						area[x, y_coord].SetTDTile(TileType.DirtToGrassB);
						area[x, y_coord + size_y - 1].SetTDTile(TileType.DirtToGrassT);
					}
					for (int y = y_coord + 1; y < y_coord + size_y - 1; y++)
						for (int x = x_coord +1; x < x_coord + size_x - 1; x++)
							area[x, y].SetTDTile(TileType.Dirt);
					break;
				default:
					break;
					
				}
			}
		}
	}
	//Reserved a plot of land for example a larger tree or a building or a bandit camp
	private void PlaceDoodadHolder(TDTile[,] area, int size_x, int size_y, int x_coord, int y_coord){
		for (int y = y_coord; y < y_coord + size_y; y++)
			for (int x = x_coord; x < x_coord + size_x; x++)
				area[x, y] = new TDTile(TileType.Empty);
				//area[x, y].SetTDTile(TileType.Empty); 	//Weird and buggy
	}
	
	//Detect if there is a similar tile next to the bordering tile
	private int DetectSimilarBorderTiles(TDTile[,] area, TDTile replaceTarget, int x_coord, int y_coord){
		int count = 0;
		//Check if we are at the edge of the map, since taht would break the program
		if (x_coord > 0 && x_coord < area.GetLength (0) - 1 && y_coord > 0 && y_coord < area.GetLength (1) - 1) {
			//Directly touching the point
			if (area [x_coord - 1, y_coord + 0] == replaceTarget)
				count++;
			if (area [x_coord + 0, y_coord - 1] == replaceTarget)
				count++;
			if (area [x_coord + 1, y_coord + 0] == replaceTarget)
				count++;
			if (area [x_coord + 0, y_coord + 1] == replaceTarget)
				count++;
		}
		return count;
	}
	//Detect if there is a similar tile next to the bordering tile
	//Array Version
	private int DetectSimilarBorderTiles(TDTile[,] area, TDTile[] replaceTargets, int x_coord, int y_coord){
		int count = 0;
		//Check if we are at the edge of the map, since taht would break the program
		for (int i = 0; i < replaceTargets.GetLength(0); i++) {
			if (x_coord > 0 && x_coord < area.GetLength (0) - 1 && y_coord > 0 && y_coord < area.GetLength (1) - 1) {
				//Directly touching the point
				if (area [x_coord - 1, y_coord + 0] == replaceTargets[i])
					count++;
				if (area [x_coord + 0, y_coord - 1] == replaceTargets[i])
					count++;
				if (area [x_coord + 1, y_coord + 0] == replaceTargets[i])
					count++;
				if (area [x_coord + 0, y_coord + 1] == replaceTargets[i])
					count++;
			}
		}
		return count;
	}



	//Scan the edges to see how clear it is
	//***Need to make exseptions, or change it so that it scans if the tile is passable
	private int ScanEdge(TDTile[,] area, TDTile scanTarget, int size_x, int size_y, int x_coord, int y_coord){
		int count = 0;
		for (int x = x_coord; x < x_coord + size_x; x++) {
			if (y_coord - 1 > 0)
				if(area [x, y_coord - 1] == scanTarget)
					count++;
			if (y_coord + size_y + 1 < area.GetLength (1) - 1)
				if(area [x, y_coord + size_y + 1] == scanTarget)
					count++;
		}
		for (int y = y_coord; y < y_coord + size_y; y++) {
			if (x_coord - 1 > 0)
				if(area [x_coord - 1, y] == scanTarget)
					count++;
			if (x_coord + size_x + 1 < area.GetLength (0) - 1)
				if(area [x_coord + size_x + 1, y_coord + 1] == scanTarget)
					count++;
		}
		return count;
	}
	
	private void GenerateRandomMass(TDTile[,] area, Doodad fill, int x_coord, int y_coord, int kFactor, int cFactor, int radius){
		area[x_coord, y_coord] = new TDTile(TileType.Shit);
			InverseSpiralGeneration (area, new TDTile (TileType.Shit), x_coord, y_coord, kFactor, cFactor, radius);
	}
	
	
	//==========================================================================
	//Specialized Tool Functions
	//==========================================================================
	//Spiral around the map by chunks and develop within those chunks
	private void GenerateChunkInSpiral(TDTile[,] area, TDTile fill, int kFactor, int cFactor){
		//map_x and map_y represent the dimension of grids
		int map_x = _size_x / MAP_CONSTANT.CHUNK_WIDTH;
		int map_y = _size_y / MAP_CONSTANT.CHUNK_HEIGHT;
		int increment = 0;
		bool space = true;
		//Expand from there by density and build it spirally
		do
		{
			//Top
			for (int x = increment; x < map_x - 1 - increment; x++)
				GenerateChunk(area, fill, 
				              x * MAP_CONSTANT.CHUNK_WIDTH, 			//x-coord
				              increment * MAP_CONSTANT.CHUNK_HEIGHT, 	//y-coord
				              kFactor, cFactor);
			//Right
			for (int y = increment; y < map_y - 1 - increment; y++)
				GenerateChunk(area, fill, 
				              (map_x - 1 - increment) * MAP_CONSTANT.CHUNK_WIDTH,	//x-coord
				              y * MAP_CONSTANT.CHUNK_HEIGHT, 						//y-coord
				              kFactor, cFactor);
			//Bottom
			for (int x = map_x - 1 - increment; x >= increment; x--)
				GenerateChunk(area, fill, 
				              x * MAP_CONSTANT.CHUNK_WIDTH, 						//x-coord
				              (map_y - 1 - increment) * MAP_CONSTANT.CHUNK_HEIGHT, 	//y-coord
				              kFactor, cFactor);
			//Left
			for (int y = map_y - 1 - increment - 1; y >= increment + 1; y--)
				GenerateChunk(area, fill, 
				              increment * MAP_CONSTANT.CHUNK_WIDTH,	//x-coord
				              y * MAP_CONSTANT.CHUNK_HEIGHT, 		//y-coord
				              kFactor, cFactor);
			//Increment or stop?
			if (2 * increment >= map_x || 2 * increment >= map_y)
				space = false;
			else
				increment++;
		} while (space);
	}
	//Individual chunk is developed starting from the top-left
	private void GenerateChunk(TDTile[,] area, TDTile fill, int x_coord, int y_coord, int kFactor, int cFactor){
		//Start with the outer edge
		for (int y = y_coord; y < y_coord + MAP_CONSTANT.CHUNK_HEIGHT; y++)
			for (int x = x_coord; x < x_coord + MAP_CONSTANT.CHUNK_WIDTH; x++)
				//Set the chunk to be a part of the map data
				area[x - x_coord, y - y_coord] = _tileData[x, y];
		//What are we going to do in this chuck?
		//A: Spiral generate the fill and then fill in the isolate areas
		SpiralGeneration (area, fill, kFactor, cFactor); 
		FillIsolatedPoints (area, fill);
		//Set the chunk back into the map data
		for (int y = y_coord; y < y_coord + MAP_CONSTANT.CHUNK_HEIGHT; y++) 
			for (int x = x_coord; x < x_coord + MAP_CONSTANT.CHUNK_WIDTH; x++) 
				_tileData[x, y] = area[x - x_coord, y - y_coord];
	}

	private bool SafeCoord(TDTile[,] area, int x_coord, int y_coord){
		return(x_coord >= 0 && x_coord < area.GetLength(0) && y_coord >= 0 && y_coord < area.GetLength(1));
	}
	private bool ContainedInCircle(int x_origin, int y_origin, int x_coord, int y_coord, int radius){
		return(radius > Mathf.Sqrt (Mathf.Pow (x_coord - x_origin, 2) + Mathf.Pow (y_coord - y_origin, 2)));
	}
}
