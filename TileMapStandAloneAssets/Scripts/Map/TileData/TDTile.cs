//Enumerator
enum TileType{
	 WATER,
	 EMPTY,
	 GRASS,
	 TREE,
	 LAVA,
	 TLDIRT,
	 TMDIRT,
	 TRDIRT,
	 WALL,
	 LMDIRT,
	 CMDIRT,
	 RMDIRT,
	 CHECK,
	 BLDIRT,
	 BMDIRT,
	 BRDIRT
};

public class TDTile {
	//Constructor
	public TDTile(int type){ _tileType = (TileType)type; }

	//Get the tile type as int
	public int GetTileTypeAsInt() { return (int)_tileType; }

	//Set the tile using an int
	public void SetTileTypeByInt(int type) { _tileType = (TileType)type; }

	public void SetTileType(TDTile type) { _tileType = type._tileType; }

	//Members
	private TileType _tileType;
}
