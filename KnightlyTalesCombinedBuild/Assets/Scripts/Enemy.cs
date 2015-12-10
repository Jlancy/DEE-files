using UnityEngine;
using System.Collections;

public enum EnemyType : int {
	Melee, Ranged
};

public class Enemy : MovableObject {
	public float sightRange = 5f;
	private Transform target;	//
	public EnemyType enemyType = EnemyType.Melee;
	Animator anim;

	protected override void Start () {
		base.Start ();
		anim = GetComponent<Animator> ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	void Update () {
		if (!isMoving) {
			if (enemyType == EnemyType.Melee) {
				ChaseTarget ();
				//AttemptMove (); // moved into ChaseTarget()
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
                wallQuery = Physics2D.Linecast(currentPosition, endPosition, blockingLayer - LayerMask.NameToLayer("Player"));
                //===================================================================================================                
                //Fix/check the above, this should check if the collider is that of a blockinglayer except the player
                //===================================================================================================
                if (wallQuery.transform != null)
                {
                    directionVector = new Vector3(0f, Mathf.Sign(distanceVector.y));
                    endPosition = new Vector3(currentPosition.x + directionVector.x,
                        currentPosition.y + directionVector.y, 0f);
                    wallQuery = Physics2D.Linecast(currentPosition, endPosition, blockingLayer - LayerMask.NameToLayer("Player"));
                    if (wallQuery.transform != null)
                    {
                        directionVector = new Vector3(0f, Mathf.Sign(-distanceVector.y));
                        endPosition = new Vector3(currentPosition.x + directionVector.x,
                            currentPosition.y + directionVector.y, 0f);
                    }
                }

			} else {
                directionVector = new Vector3(0f, Mathf.Sign(distanceVector.y));
                endPosition = new Vector3(currentPosition.x + directionVector.x,
                    currentPosition.y + directionVector.y, 0f);
                wallQuery = Physics2D.Linecast(currentPosition, endPosition, blockingLayer - LayerMask.NameToLayer("Player"));
                if (wallQuery.transform != null)
                {
                    directionVector = new Vector3(Mathf.Sign(distanceVector.x), 0f);
                    endPosition = new Vector3(currentPosition.x + directionVector.x,
                        currentPosition.y + directionVector.y, 0f);
                    wallQuery = Physics2D.Linecast(currentPosition, endPosition, blockingLayer - LayerMask.NameToLayer("Player"));
                    if (wallQuery.transform != null)
                    {
                        directionVector = new Vector3(Mathf.Sign(-distanceVector.x), 0f);
                        endPosition = new Vector3(currentPosition.x + directionVector.x,
                            currentPosition.y + directionVector.y, 0f);
                    }
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

        AttemptMove();
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
        if(component.CompareTag ("Player")){
			Player hitObj = component as Player;
			hitObj.TakeDamage (10);
            // fixed the disease problem, (consistent damge ove time)
            // now need to only apply getting hit once every so often
            // either if we have enemy attack animation or something like that
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