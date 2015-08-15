using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	Rigidbody2D rBody;
	Animator anim;
	public int speedFactor = 1;
	public int xSpawn = 0;
	public int ySpawn = 0;
	public float tileSize = 1;
	//static int[,] _collisionMap;
	// Use this for initialization
	void Start () {
		rBody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		//Prevents the player from spinning
		rBody.fixedAngle = true;
		//Set spawn location
		rBody.position = new Vector3 (xSpawn * tileSize, ySpawn * tileSize, 0f);
		//_collisionMap = TileMap.GetCollisionMap ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 movementVector = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		//Vector2 currentPosition = rBody.position;
		//Vector2 displacement = new Vector2(0f, 0f);

		if (movementVector != Vector2.zero) {
			anim.SetBool ("isWalking", true);
			anim.SetFloat ("inputX", movementVector.x);

			anim.SetFloat ("inputY", movementVector.y);



			/*
			 * attempt 1 at locked movement
			do{
				displacement += rBody.position + speedFactor * movementVector * Time.deltaTime;
				rBody.MovePosition (rBody.position + speedFactor * movementVector * Time.deltaTime);
			}while(displacement.x < 1 || displacement.x > -1 || displacement.y < 1 || displacement.y > -1);
			*/
		} 
		else
			anim.SetBool ("isWalking", false);

		if(Input.GetKey(KeyCode.T))
			anim.SetBool ("tIsPress", true);
		else
		   	anim.SetBool ("tIsPress", false);


		rBody.MovePosition (rBody.position + speedFactor * movementVector * Time.deltaTime);
	}

	/*
	public void SetCollisionMap(int[,] collisionMap){
		_collisionMap = new int[collisionMap.GetLength (0), collisionMap.GetLength (1)];	//needed?
		_collisionMap = collisionMap;
	}
*/
}
