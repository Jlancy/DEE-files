using UnityEngine;
using System.Collections;

public class MovableObject : GridMovement {

	protected override void Start () {
        //base.Start ();
        bCollider = GetComponent<BoxCollider2D>();
        rBody = GetComponent<Rigidbody2D>();
    }

	/// <summary>
	/// Current problem: Rock will not be affixed to the grid
	/// Possible solution: Frezze all other objects eg enemies and player.
	/// </summary>

	private void Update () {
		if (!isMoving) {
			//Update currentPosition
			//currentPosition = rBody.position;
			//AttemptMove ();
		}
	}

    public void SetSpawn(int newX, int newY)
    {
        xSpawn = newX;
        ySpawn = newY;
    }

	public bool IsMoving(){
		return isMoving;
	}

	public void AttemptPush(Vector2 attempterPosition){
		if (!isMoving) {
			//Update endPosition
			if (Mathf.Abs(attempterPosition.x - currentPosition.x) < float.Epsilon) {
				endPosition = new Vector3 (currentPosition.x,
										   currentPosition.y + System.Math.Sign (currentPosition.y - attempterPosition.y) * gridSize, 
										   currentPosition.z);
			} else if (Mathf.Abs(attempterPosition.y - currentPosition.y) < float.Epsilon) {
				endPosition = new Vector3 (currentPosition.x + System.Math.Sign (currentPosition.x - attempterPosition.x) * gridSize,
										   currentPosition.y, 
										   currentPosition.z);
			}
			print("endPosition = " + endPosition.x + ", " + endPosition.y);

			currentPosition = rBody.position;
			AttemptMove ();
		}
	}
		
	//	fodder
	protected override void AttemptMove (){
		base.AttemptMove ();
		//RaycastHit2D hit;
		/*
		if (Move (out hit)) {
			print ("move successful");	//insert stepping sound
		} else {	
			print ("move failed");		//insert bumping sound
		}
		*/	
		
	}
	protected override void OnCantMove<T> (T component){
		MovableObject hitObj = component as MovableObject;
		hitObj.AttemptPush (rBody.position);
	}
	/*
	private void OnTriggerEnter2D (Collider2D other){
		if (other.tag = "Object") {
			
		}
	}
	*/
}
