using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class tempMovement : MonoBehaviour {
	public int movespeed = 8;
	string tempDirection;
	public ControlMovment  movement;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {

		if(movement.InControlRegion)
		{
			tempDirection = movement.dir;
			Debug.Log(tempDirection);
			switch(tempDirection)
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
