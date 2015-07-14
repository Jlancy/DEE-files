using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	private TileMap map;


	// Use this for initialization
	void Start () {
		map.BuildMesh ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
