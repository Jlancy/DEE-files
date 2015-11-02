using UnityEngine;
using System.Collections;

public enum EnemyType : int {
	Melee, Ranged
};

public class Enemy : MovableObject {
	public float sightRange = 5f;
	private Transform target;	//
	public EnemyType enemyType;
	Animator anim;

	protected override void Start () {
		if (enemyType == null)
			enemyType = EnemyType.Melee;
		base.Start ();
		anim = GetComponent<Animator> ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	void Update () {
		if (!isMoving) {
			if (enemyType == EnemyType.Melee) {
				ChaseTarget ();
				AttemptMove ();
				//Check to make sure the player info was found
				//print("Player Read Coord = (" + target.position.x + ", " + target.position.y + ").");
		
			}
		}
	}
	//Planned design
	//Most basic idea, mob will travel a set amount of distance back and forth in a given direction
	void PatrolMove(int spaces){
		ChaseTarget ();
		AttemptMove ();
	}
	//Planned design
	//Use a ray/linecast up to a cirular range
	//Other design
	//RPGmaker style
	void ChaseTarget(){
		currentPosition = transform.position;
		Vector2 directionVector = Vector2.zero;
		Vector2 targetPosition = target.transform.position; //Player Position 
		Vector2 distanceVector = new Vector2 (targetPosition.x - currentPosition.x, targetPosition.y - currentPosition.y); //Player pos - Entity pos
		
        //Distance between 2 points
		// D = sqrt((x - X)^2 + (y - Y)^2)
		float playerDistance = Mathf.Sqrt (Mathf.Pow (distanceVector.x, 2) + Mathf.Pow (distanceVector.y, 2));

		if (playerDistance <= sightRange) {
            RaycastHit2D wallQuery;
            bCollider.enabled = false;

			if (Mathf.Abs(distanceVector.x) > Mathf.Abs(distanceVector.y)) {
                directionVector = new Vector3(Mathf.Sign(distanceVector.x), 0f);
                endPosition = new Vector3(currentPosition.x + directionVector.x,
                    currentPosition.y + directionVector.y, 0f);
                wallQuery = Physics2D.Linecast(currentPosition, endPosition, LayerMask.NameToLayer("Wall"));
                if (wallQuery == null)
                {
                    directionVector = new Vector3(0f, Mathf.Sign(distanceVector.y));
                    endPosition = new Vector3(currentPosition.x + directionVector.x,
                        currentPosition.y + directionVector.y, 0f);
                }

			} else {
                directionVector = new Vector3(0f, Mathf.Sign(distanceVector.y));
                endPosition = new Vector3(currentPosition.x + directionVector.x,
                    currentPosition.y + directionVector.y, 0f);
                wallQuery = Physics2D.Linecast(currentPosition, endPosition, LayerMask.NameToLayer("Wall"));
                if (wallQuery == null)
                {
                    directionVector = new Vector3(Mathf.Sign(distanceVector.x), 0f);
                    endPosition = new Vector3(currentPosition.x + directionVector.x,
                        currentPosition.y + directionVector.y, 0f);
                }
			}
			
            bCollider.enabled = true;

			anim.SetFloat ("xInput", directionVector.x);
			anim.SetFloat ("yInput", directionVector.y);
			anim.SetBool ("isWalking", true);
		} else {
			anim.SetFloat ("xInput", directionVector.x);
			anim.SetFloat ("yInput", directionVector.y);
			anim.SetBool ("isWalking", false);
		}
	}

	protected override void AttemptMove (){
		base.AttemptMove ();
		RaycastHit2D hit;
		if (Move (out hit)) {
			print ("enemy move successful");	//insert stepping sound
		} else {	
			print ("enemy move failed");		//insert bumping sound
			Player hitComponent = hit.transform.GetComponent <Player> ();
			if(!canMove && hitComponent != null)
				OnCantMove (hitComponent);
		}

	}
	protected override void OnCantMove<T> (T component){
		if(component.CompareTag("Player")){
			Player hitObj = component as Player;
			hitObj.TakeDamage (10);
		}
	}
}

//back up

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
}
 */