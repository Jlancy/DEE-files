using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	private TileMap map;
	private int[,] collisionMap;
	public static GameManager instance = null;
	// Use this for initialization
	void Start () {
		map.BuildMesh ();
		collisionMap = map.GetCollisionMap ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
