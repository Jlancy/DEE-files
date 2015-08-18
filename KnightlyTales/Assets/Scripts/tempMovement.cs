using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class tempMovement : MonoBehaviour {
	public int movespeed = 8;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if(Input.GetKey(KeyCode.W))
		   transform.position += transform.up * Time.deltaTime * movespeed;
	
	if(Input.GetKey(KeyCode.S))
			transform.position -= transform.up * Time.deltaTime * movespeed;

		if(Input.GetKey(KeyCode.D))
			transform.position += transform.right * Time.deltaTime * movespeed;
		if(Input.GetKey(KeyCode.A))
			transform.position -= transform.right * Time.deltaTime * movespeed;

	}
}
