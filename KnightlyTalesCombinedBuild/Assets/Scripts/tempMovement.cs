using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class tempMovement : MonoBehaviour {
	public int movespeed = 8;
	string Direction;
	string LastKnownDirection;
	ControlMovement  movement;



	// Use this for initialization
	void Start () {
		movement = FindObjectOfType<ControlMovement>();
	}
	
	// Update is called once per frame
	void Update () {

		MovementCase();
	}


	void MovementCase()
	{
		if(movement.InControlRegion)
		{
			Direction = movement.dir;
		//	Debug.Log(tempDirection);
			if(Direction != "CENTER")
				LastKnownDirection = Direction;

		
			switch(Direction)
			{
				
			case "UP":
				transform.position += transform.up * Time.deltaTime * movespeed;
				break;
				
			case "DOWN":
				transform.position -= transform.up * Time.deltaTime * movespeed;
				break;
				
			case "RIGHT":
				transform.position += transform.right * Time.deltaTime * movespeed;
				break;
			case "LEFT":
				
				transform.position -= transform.right * Time.deltaTime * movespeed;
				break;
				
			default:
				
				break;
			}
		}
	}
}
