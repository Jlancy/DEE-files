using UnityEngine;	//Needed for Random at line ~141
using System.Collections.Generic;	//Needed for queue

public enum MapType : int {
	Forest, Cave, ForestByChunk
};

public enum TileType : int {
	Null, Water, Grass, Dirt, Tree, Poop
};
//Redo this later on
public enum Doodad : int {
	Dirt, Gate
};

public class TDMap {
	/////////////////////////////
	//Replace this with Vector2//
	/////////////////////////////
	public struct Coord{
		public int x_coord;
		public int y_coord;
	}
	//==========================================================================
	//Variables
	//==========================================================================
	private TileType[,] _tileData;
	private int _size_x;
	private int _size_y;
	//==========================================================================
	//Constructor, Get, and Set Functions
	//==========================================================================
	//Constructor
	public TDMap(int size_x, int size_y, MapType mapType){
		_size_x = size_x;
		_size_y = size_y;
		_tileData = new TileType[_size_x, _size_y];
		for (int y = 0; y < size_y; y++)
			for (int x = 0; x < size_x; x++)
				_tileData [x, y] = TileType.Null ;
		
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
	//Gets the tile type at the given index
	public TileType GetTileTypeAt(int x_coord, int y_coord){
		if (x_coord < 0 || x_coord >= _size_x || y_coord < 0 || y_coord >= _size_y)
			return TileType.Null;
		else
			return _tileData[x_coord, y_coord];
	}
	//Gets the map data
	public TileType[,] GetMapData(){
		return _tileData;
	}
	
	//==========================================================================
	//Insert different types of maps here
	//==========================================================================
	public void GenerateForestByChunks(){
		/*
		TileType[,] chunkData;
		chunkData = new TDTile[MAP_CONSTANT.CHUNK_WIDTH, MAP_CONSTANT.CHUNK_HEIGHT];
		FillMap (new TDTile(TileType.Grass));
		RingGenerator (_tileData, new TDTile(TileType.TreeOnGrass), 0);
		GenerateChunkInSpiral (chunkData, new TDTile(TileType.TreeOnGrass), 50, 0);
		FillUnreachable (_tileData, new TDTile(TileType.Grass), new TDTile(TileType.TreeOnGrass));
		FillWithTile (_tileData, new TDTile(TileType.Grass), Doodad.Dirt, 2 * 1 + 2 * 1, 2, 2);
		FillWithTile (_tileData, new TDTile(TileType.Grass), Doodad.Dirt, 2 * 2 + 2 * 2, 3, 3);	
		*/
	}
	//Method currently used
	public void GenerateForestAsWhole(){
		FillMap (TileType.Grass);
		RingGenerator (_tileData, TileType.Tree, 0);
		//PlaceDoodad (_tileData, Doodad.Gate, (int)Random.Range (_size_x / 2 - 5, _size_x / 2 + 5), _size_y - 1, 2, 1);
		SpiralGeneration (_tileData, TileType.Tree, 40, 0);
		//SpiralGeneration (_tileData, new TDTile(TileType.TreeOnGrass), 40, 0);
		
		FillUnreachable (_tileData, TileType.Grass, TileType.Tree);
		GenerateRandomMass (_tileData, Doodad.Dirt, _size_x / 2, _size_y / 2, 50, 0, 5);
		//FillWithTile (_tileData, TileType.Grass, Doodad.Dirt, 2 * 2 + 2 * 2, 2, 2);
		//FillWithTile (_tileData, TileType.Grass, Doodad.Dirt, 2 * 3 + 2 * 3, 3, 3);		
	}
	public void GenerateCave(){
		/*
		FillMap (new TDTile(TileType.Dirt));
		RingGenerator (_tileData, new TDTile(TileType.Lava), 0);
		SpiralGeneration (_tileData, new TDTile(TileType.Lava), 20, 5);
		FillUnreachable (_tileData, new TDTile(TileType.Dirt), new TDTile(TileType.Lava));
		*/
	}
	
	//////////////////////////////////////////////////////////////////////////////////////////////
	/*===================TDTileConversion=======================================================*/
	//////////////////////////////////////////////////////////////////////////////////////////////
	
	
	//==========================================================================
	//FillUnreachable() and its helpers
	//==========================================================================
	
	public void FillUnreachable(TileType[,] area, TileType scanTarget, TileType replaceTarget){
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
	
	public int Largest_Space(TileType[,] area, TileType scanTarget, List<Coord> topLeftCoords){
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
				scanArea[x, y] = (int)area[x, y];
			}
		}
		//Scan the map for the 'scanTarget', when one is found, inspect its area and then compare sizes
		//-1 signifies that the tile has been scaned
		for (int y = 0; y < scanArea.GetLength(1); y++) {
			for (int x = 0; x < scanArea.GetLength(0); x++){
				
				if(scanArea[x, y] == (int)scanTarget){
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
	private void ScanMap(int[,] area, Queue<Coord> coordQ, TileType scanTarget, ref int currentCount){
		Coord current = coordQ.Dequeue ();
		Coord nextPoint;
		//Scan left
		if (current.x_coord > 0) {
			if (area[current.x_coord - 1, current.y_coord] == (int)scanTarget){
				nextPoint.x_coord = current.x_coord - 1;
				nextPoint.y_coord = current.y_coord;
				area[nextPoint.x_coord, nextPoint.y_coord] = -1;
				coordQ.Enqueue (nextPoint);
				currentCount++;
			}
		}
		//Scan right
		if (current.x_coord < area.GetLength(0) - 1) {
			if (area[current.x_coord + 1, current.y_coord] == (int)scanTarget){
				nextPoint.x_coord = current.x_coord + 1;
				nextPoint.y_coord = current.y_coord;
				area[nextPoint.x_coord, nextPoint.y_coord] = -1;
				coordQ.Enqueue (nextPoint);
				currentCount++;
			}
		}
		//Scan up
		if (current.y_coord > 0) {
			if (area[current.x_coord, current.y_coord - 1] == (int)scanTarget){
				nextPoint.x_coord = current.x_coord;
				nextPoint.y_coord = current.y_coord - 1;
				area[nextPoint.x_coord, nextPoint.y_coord] = -1;
				coordQ.Enqueue (nextPoint);
				currentCount++;
			}
		}
		//Scan down
		if (current.y_coord < area.GetLength(1) - 1) {
			if (area[current.x_coord, current.y_coord + 1] == (int)scanTarget){
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
	private void FillIsolated(TileType[,] area, Queue<Coord> topLeftCoords, TileType scanTarget, TileType replaceTarget){
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
	//Fills in points with 3 or more bording tiles
	//An attempt to remove unreachable area
	private void FillIsolatedPoints(TileType[,] area, TileType replaceTarget)
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
	//==========================================================================
	//Functions taken from MapGenerator; General Tool Functions
	//==========================================================================
	/*Clear*/
	//Fill the map with a single tile type
	private void FillMap(TileType replaceTarget){
		
		for (int y = 0; y < _size_y; y++){			//For every point in the map,
			for (int x = 0; x < _size_x; x++)		//replace that point with the
				_tileData[x, y] = replaceTarget;	//designated TileType
		}
	}
	/*Clear*/
	//Generates a ring/rectangle in how ever increments in 
	private void RingGenerator(TileType[,] area, TileType replaceTarget, int increment){
		//Top & Bottom
		for (int x = increment; x < area.GetLength(0) - increment; x++) {
			area[x, increment] = replaceTarget;
			area[x, area.GetLength(1) - 1 - increment] = replaceTarget;
		}
		//Left & Right
		for (int y = increment; y < area.GetLength(1) - increment; y++) {
			area[increment, y] = replaceTarget;
			area[area.GetLength (0) - 1 - increment, y] = replaceTarget;
		}
	}
	/*Clear*/
	//General in a clockwise (square)spiral from the outside base on neighbouring tiles
	private void SpiralGeneration(TileType[,] area, TileType replaceTarget, int kFactor, int cFactor){
		int increment = 1;
		bool reachedCenter = false;
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
				reachedCenter = true;
			else
				increment++;
		} while (!reachedCenter);
	}
	/*Clear*/
	//Generates in a clockwise (circular)spiral from the inside based on neighbouring tiles
	private void InverseSpiralGeneration(TileType[,] area, TileType replaceTarget, int x_coord, int y_coord, int kFactor, int cFactor, int radius){
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
	
	
	/*Clear*/
	//Replaces tile if the neighbouring tiles are alike
	private void GenerateTileByRelation( TileType[,] area, TileType replaceTarget, int x_coord, int y_coord, int kFactor, int cFactor){
		//(Modifier x Neighbouring Trees) / 100 
		if (((kFactor * DetectSimilarBorderTiles(area, replaceTarget, x_coord, y_coord)) + cFactor) > (int)Random.Range(0,100) &&
		    x_coord > 0 && x_coord < area.GetLength (0) - 1 && y_coord > 0 && y_coord < area.GetLength (1) - 1)
			area[x_coord, y_coord] = replaceTarget;
	}
	
	//Doodad may have their own struct later on and moved into a global
	private void PlaceDoodad(TileType[,] area, Doodad fill, int x_coord, int y_coord, int size_x, int size_y){
		/* 
		if (area.GetLength (0) > x_coord + size_x - 1 && area.GetLength (1) > y_coord + size_y - 1) {
			switch(fill){
			case Doodad.Gate:
				area[x_coord, y_coord] = TileType.NorthGate1;
				area[x_coord + 1, y_coord] = TileType.NorthGate2;
				break;
			default:
				break;
			}
		}
		*/
	}
	
	//Reserved a plot of land for example a larger tree or a building or a bandit camp
	private void FillWithTile(TileType[,] area, TileType scanTarget, Doodad fill, int acceptedClearance, int size_x, int size_y){
		/*
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
					area[x_coord, y_coord] = TileType.DirtToGrassBL;
					area[x_coord + size_x - 1, y_coord] = TileType.DirtToGrassBR;
					area[x_coord, y_coord + size_y - 1] = TileType.DirtToGrassTL;
					area[x_coord + size_x - 1, y_coord + size_y - 1] = TileType.DirtToGrassTR;
					for (int y = y_coord + 1; y < y_coord + size_y - 1; y++){
						area[x_coord, y] = TileType.DirtToGrassL;
						area[x_coord + size_x - 1 , y] = TileType.DirtToGrassR;
					}
					for (int x = x_coord + 1; x < x_coord + size_x - 1; x++){
						area[x, y_coord] = TileType.DirtToGrassB;
						area[x, y_coord + size_y - 1] = TileType.DirtToGrassT;
					}
					for (int y = y_coord + 1; y < y_coord + size_y - 1; y++)
						for (int x = x_coord +1; x < x_coord + size_x - 1; x++)
							area[x, y] = TileType.Dirt;
					break;
				default:
					break;
					
				}
			}
		}
		*/
	}
	//Reserved a plot of land for example a larger tree or a building or a bandit camp
	private void PlaceDoodadHolder(TileType[,] area, int size_x, int size_y, int x_coord, int y_coord){
		for (int y = y_coord; y < y_coord + size_y; y++)
			for (int x = x_coord; x < x_coord + size_x; x++)
				area[x, y] = TileType.Null;
	}
	
	//Detect if there is a similar tile next to the bordering tile
	private int DetectSimilarBorderTiles(TileType[,] area, TileType replaceTarget, int x_coord, int y_coord){
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
	
	
	
	//Scan the edges to see how clear it is
	//***Need to make exseptions, or change it so that it scans if the tile is passable
	private int ScanEdge(TileType[,] area, TileType scanTarget, int size_x, int size_y, int x_coord, int y_coord){
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
	
	private void GenerateRandomMass(TileType[,] area, Doodad fill, int x_coord, int y_coord, int kFactor, int cFactor, int radius){
		area[x_coord, y_coord] = TileType.Poop;
		InverseSpiralGeneration (area, TileType.Poop, x_coord, y_coord, kFactor, cFactor, radius);
	}
	
	
	//==========================================================================
	//Specialized Tool Functions
	//==========================================================================
	//Spiral around the map by chunks and develop within those chunks
	private void GenerateChunkInSpiral(TileType[,] area, TileType fill, int kFactor, int cFactor){
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
	private void GenerateChunk(TileType[,] area, TileType fill, int x_coord, int y_coord, int kFactor, int cFactor){
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
	
	private bool SafeCoord(TileType[,] area, int x_coord, int y_coord){
		return(x_coord >= 0 && x_coord < area.GetLength(0) && y_coord >= 0 && y_coord < area.GetLength(1));
	}
	private bool ContainedInCircle(int x_origin, int y_origin, int x_coord, int y_coord, int radius){
		return(radius > Mathf.Sqrt (Mathf.Pow (x_coord - x_origin, 2) + Mathf.Pow (y_coord - y_origin, 2)));
	}
}
