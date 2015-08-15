using UnityEngine;
using System.Collections;

public class Player : GridMovement {
	Animator anim;

	private int health = 100;

	protected override void Start () {
		base.Start ();
		anim = GetComponent<Animator> ();
	}

	private void Update () {
		//If the object is not in motion, do input check. 
		//isMoving is true while the coroutine Move() is running.
		if (!isMoving) {
			Vector2 input = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
			//Check if we have worthwhile inputs a.k.a. some sort of input.
			///////////////////////////////////////////
			/*PLANNED ORDER: ATTACK > MOVEMENT > IDLE*/
			///////////////////////////////////////////
			
			if (input != Vector2.zero) {
				//The following if-statements will make it so that the object will move in one direction
				//until that command is let go
				//Check the x input and that the object is not moving vertically
				if (input.x != 0 && !movingVert) {
					movingHori = true; 
					input.y = 0;		//Ensure that the object is moving horizontally
				} else
					movingHori = false;
				//Check the y input and that hte object is not moving horizontally
				if (input.y != 0 && !movingHori) {
					movingVert = true;
					input.x = 0;		//Ensure that the object is moving vertically
				} else
					movingVert = false;
				
				//Set animatior parameters
				anim.SetFloat ("xInput", input.x);
				anim.SetFloat ("yInput", input.y);
				anim.SetBool ("isWalking", true);
				
				//Set currentPosition to rBody's position
				currentPosition = rBody.position;

				//essentially endPosition = currentPosition + (the sign of input(+/-) * grid size	
				endPosition = new Vector3 (currentPosition.x + System.Math.Sign (input.x) * gridSize,
				                           currentPosition.y + System.Math.Sign (input.y) * gridSize, 
				                           currentPosition.z);
				//print ("current position = " + currentPosition);
				//print ("end position = " + endPosition);
				AttemptMove<MovableObject>();
			} else {
				//If nothing is happening, set all animator parameter to false
				anim.SetBool ("isWalking", false);
			}
		}
	}

	public void TakeDamage(int damage){
		health -= damage;
	}

	protected override void AttemptMove <T> (){
		base.AttemptMove <T> ();
		RaycastHit2D hit;
		if (Move (out hit)) {
			print ("move successful");	//insert stepping sound
		} else {	
			print ("move failed");		//insert bumping sound
		}
		
	}
	protected override void OnCantMove<T> (T component){
		MovableObject hitObj = component as MovableObject;
		//Need something to force player to freeze
		hitObj.AttemptPush (rBody.position);
		/*
		if(hitObj.IsMoving()){
			StartCoroutine (Moving ());
		}
		print ("player push done");
		//*/
	}
}
