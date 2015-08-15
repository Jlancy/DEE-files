//Enumerator
public enum TileType : int{
	Water, 			Empty, 			Grass, 			TreeOnGrass,
	Lava, 			DirtToGrassTL, 	DirtToGrassT, 	DirtToGrassTR,
	Wall, 			DirtToGrassL, 	Dirt, 			DirtToGrassR,
	Check, 			DirtToGrassBL, 	DirtToGrassB, 	DirtToGrassBR,
	DirtToGrassTRi,	DirtToGrassTLi,	DirtToGrassBRi,	DirtToGrassBLi,
	NorthGate1,		NorthGate2,		Shit,			AppleTree,

};
public enum TileTrait : int{
	PASSABLE, IMPASSABLE
};

public class TDTile {
	//Constructor
	public TDTile(int type, int trait){ 
		_tileType = (TileType)type;
		_tileTrait = (TileTrait)trait;
	}

	public TDTile(int type){ 
		_tileType = (TileType)type;
		GrabTrait ();
	}

	public TDTile(TileType type){ 
		_tileType = type;
		GrabTrait ();
	}

	//Get the tile type as int
	public int GetTileTypeAsInt() { 
		return (int)_tileType; 
	}
	public TileType GetTileType() { 
		return _tileType; 
	}
	//Set the tile trait as int
	public int GetTileTraitAsInt() {
		return (int)_tileTrait;
	}
	
	public void SetTDTile(TDTile tile) { 
		_tileType = tile._tileType;
		_tileTrait = tile._tileTrait;
	}

	public void SetTDTile(TileType type) { 
		_tileType = type;
		GrabTrait ();
	}

	public void SetTDTile(int type) { 
		_tileType = (TileType)type;
		GrabTrait ();
	}

	public static bool operator ==(TDTile lhs, TDTile rhs){
		return lhs._tileType == rhs._tileType;
	}

	public static bool operator !=(TDTile lhs, TDTile rhs){
		return lhs._tileType != rhs._tileType;
	}

	private void GrabTrait(){
		switch (_tileType) {
		case TileType.Grass:
		case TileType.DirtToGrassTL:
		case TileType.DirtToGrassT:
		case TileType.DirtToGrassTR:
		case TileType.DirtToGrassL:
		case TileType.Dirt:
		case TileType.DirtToGrassR:
		case TileType.DirtToGrassBL:
		case TileType.DirtToGrassB:
		case TileType.DirtToGrassBR:
			_tileTrait = TileTrait.PASSABLE;
			break;
			/*
		 * Save for later
		case TileType.TreeOnGrass:
		case TileType.Wall:
		case TileType.Water:
		case TileType.Lava:
		*/
		default:
			_tileTrait = TileTrait.IMPASSABLE;
			break;
		}
	}

	//Members
	private TileType _tileType;
	private TileTrait _tileTrait;
}
