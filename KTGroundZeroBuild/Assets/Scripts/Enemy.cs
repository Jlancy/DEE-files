using UnityEngine;
using System.Collections;

public class Enemy : MovableObject {
	public float sightRange = 5f;
	private Transform target;	//
	Animator anim;

	protected override void Start () {
		base.Start ();
		anim = GetComponent<Animator> ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	void Update () {
		if (!isMoving) {
			ChaseTarget ();
			AttemptMove<Player> ();
			//Check to make sure the player info was found
			//print("Player Read Coord = (" + target.position.x + ", " + target.position.y + ").");
		}
	}
	//Planned design
	//Most basic idea, mob will travel a set amount of distance back and forth in a given direction
	void PatrolMove(int spaces){
		ChaseTarget ();
		AttemptMove<Player> ();
	}
	//Planned design
	//Use a ray/linecast up to a cirular range
	//Other design
	//RPGmaker style
	void ChaseTarget(){
		currentPosition = transform.position;
		Vector2 directionVector = Vector2.zero;
		Vector2 targetPosition = target.transform.position;
		Vector2 distanceVector = new Vector2 (targetPosition.x - currentPosition.x, targetPosition.y - currentPosition.y);
		//Distance between 2 points
		// D = sqrt((x - X)^2 + (y - Y)^2)
		float playerDistance = Mathf.Sqrt (Mathf.Pow (distanceVector.x, 2) + Mathf.Pow (distanceVector.y, 2));
		//print ("playerDistance = " + playerDistance);

		if (playerDistance <= sightRange) {
			if (distanceVector.x > distanceVector.y) {
				if (targetPosition.x > currentPosition.x)
					directionVector = new Vector3 (1f, 0f);
				else
					directionVector = new Vector3 (-1f, 0f);
			} else if (distanceVector.y > distanceVector.x) {
				if (targetPosition.y > currentPosition.y)
					directionVector = new Vector3 (0f, 1f);
				else
					directionVector = new Vector3 (0f, -1f);
			}
			endPosition = new Vector3 (currentPosition.x + directionVector.x, 
				currentPosition.y + directionVector.y, 0f);
			anim.SetFloat ("xInput", directionVector.x);
			anim.SetFloat ("yInput", directionVector.y);
			anim.SetBool ("isWalking", true);
		} else {
			anim.SetFloat ("xInput", directionVector.x);
			anim.SetFloat ("yInput", directionVector.y);
			anim.SetBool ("isWalking", false);
		}


		/*
		if (playerDistance < sightRange) {
			RaycastHit2D sight;
			sight = Physics2D.Linecast (currentPosition, targetPosition, blockingLayer);
			if (sight == null) {
				endPosition = currentPosition;
				if (distanceVector.x > distanceVector.y) {
					if (targetPosition.x > currentPosition.x)
						endPosition.x = + 1f;
					//endPosition.x = targetPosition.x > currentPosition.x ? 1 : -1;
					else 
						endPosition.x = - 1f;
				} else if (distanceVector.y > distanceVector.x) {
					if (targetPosition.y > currentPosition.y)
						endPosition.y = + 1f;
					else 
						endPosition.y = - 1f;
				}
			}

			//check is LoS is blocked
			//May require an additional blocking layer
		}*/
	}

	protected override void AttemptMove <T> (){
		base.AttemptMove <T> ();
		RaycastHit2D hit;
		/*
		if (Move (out hit)) {
			print ("move successful");	//insert stepping sound
		} else {	
			print ("move failed");		//insert bumping sound
		}
		*/
	}
	protected override void OnCantMove<T> (T component){
		Player hitObj = component as Player;
		hitObj.TakeDamage (10);
	}
}
