using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {
	public Transform targetTeleport;
	private Vector2  newPosition;

	bool movingPlayer;


	void Start()
	{
		newPosition = (Vector2)targetTeleport.position + new Vector2(0,-1);
	}


	public void MovePlayer ( Transform player)
	{ 
		player.position = newPosition;
	}


}
