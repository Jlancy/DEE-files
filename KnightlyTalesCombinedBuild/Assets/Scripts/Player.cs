using UnityEngine;
using System.Collections;

public class Player : GridMovement {
	Animator anim;
	bool isDoingSomething = false;		//can change to busy later, flag that the player is doing something besides moving
	public int health = 100;			//health counter
	public int MaxHealth= 100;
    ControlMovement movement;
    private string direction;

	bool MovementPause =false;

	public int SwordAttack;
	public int BowAttack;
	public int QuiverSize;


	protected override void Start () {
		base.Start ();
		anim = GetComponent<Animator> ();
		movement = FindObjectOfType<ControlMovement>();

	}
	private void LateUpdate()
	{

	}
	private void Update () {
		MovementCase();
        ///////////////////////////////////////////
        /*PLANNED ORDER: ATTACK > MOVEMENT > IDLE*/
        ///////////////////////////////////////////
		//If the object is not in motion, do input check. 
		//isMoving is true while the coroutine Move() is running.
		/*
        if (Input.GetKeyDown (KeyCode.X)) {
			StartCoroutine(Attacking());
		}

        */
		if (!isMoving && !isDoingSomething){
            //orientation = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			//Check if we have worthwhile inputs a.k.a. some sort of input.
			/*
            direction = movement.dir;
            switch (direction)
            {

                case "UP":
                    orientation = new Vector2(0, 1);
                    break;
                case "DOWN":
                    orientation = new Vector2(0, -1);
                    break;
                case "RIGHT":
                    orientation = new Vector2(1, 0);
                    break;
                case "LEFT":
                    orientation = new Vector2(-1, 0);
                    break;
                default:
                    orientation = Vector2.zero;
                    break;
            }
            */

            if (orientation != Vector2.zero)
            {
				//The following if-statements will make it so that the object will move in one direction
				//until that command is let go
				//Check the x input and that the object is not moving vertically
				if (orientation.x != 0 && !movingVert) {
					movingHori = true; 
					orientation.y = 0;		//Ensure that the object is moving horizontally
				} else
					movingHori = false;
				//Check the y input and that hte object is not moving horizontally
				if (orientation.y != 0 && !movingHori) {
					movingVert = true;
					orientation.x = 0;		//Ensure that the object is moving vertically
				} else
					movingVert = false;
				
				//Set animatior parameters
				anim.SetFloat ("xInput", orientation.x);
				anim.SetFloat ("yInput", orientation.y);
				anim.SetBool ("isWalking", true);
				
				//Set currentPosition to rBody's position
				currentPosition = rBody.position;

				//essentially endPosition = currentPosition + (the sign of input(+/-) * grid size	
				endPosition = new Vector3 (currentPosition.x + System.Math.Sign (orientation.x) * gridSize,
				                           currentPosition.y + System.Math.Sign (orientation.y) * gridSize, 
				                           currentPosition.z);
				//print ("current position = " + currentPosition);
				//print ("end position = " + endPosition);
				AttemptMove();
			} else {
				//If nothing is happening, set all animator parameter to false
				//anim.SetBool ("isWalking", false);
			}
		}
	}

    public void Attack(){
        Debug.Log("Attack");
		StartCoroutine(Attacking());
    }

    public void GainHealth(int gain)
    {
        health += gain;
        print("Player gains " + gain + "health!");
    }

	public void TakeDamage(int damage){
		health -= damage;
		print ("Player takes " + damage + "damage!");
	}

	protected IEnumerator Attacking() {
		isDoingSomething = true;
        float t = 0;
		//anim.SetFloat ("xInput", orientation.x);
		//anim.SetFloat ("yInput", orientation.y);
		anim.SetBool ("isAttacking", true);

		/*
        //Get enemy infront of player
        //Send msg to enemy that HE JUST GOT WHACKED
        RaycastHit2D attackTarget;
        bCollider.enabled = false;
        //Add a fix here that would check the a pushed object is not still moving
        attackTarget = Physics2D.Linecast(currentPosition, endPosition, blockingLayer);
        bCollider.enabled = true;
        if (attackTarget)
            attackTarget.
        */

        //Animation duration, look for a better alternative
		
        while (t < 0.517) {
			t += Time.deltaTime;
			//rBody.position = Vector3.MoveTowards(currentPosition, endPosition, t);
			yield return null;
		}
        

        //Possible alternative to the above but only takes int <- not acceptable
        //yield return new WaitForSeconds(1);

        //End the animation
		anim.SetBool ("isAttacking", false);
		isDoingSomething = false;
		yield return 0;
	}

	protected override void AttemptMove (){
		base.AttemptMove ();
		RaycastHit2D hit;
		if (Move (out hit)) {
			print ("move successful");	//insert stepping sound
		} else {	
			print ("move failed");		//insert bumping sound
            //will be removed after add player attaching ot movable object
			MovableObject hitComponent = hit.transform.GetComponent <MovableObject> ();
			if(!canMove && hitComponent != null)
				OnCantMove (hitComponent);
		}

	}
	protected override void OnCantMove<T> (T component){
		if (component.CompareTag ("Object")) {
			MovableObject hitObj = component as MovableObject;
			hitObj.AttemptPush (rBody.position);
		}
	}

	// get the ContolMovement. it raycast forthe Dpad if the player is touching, clicking in that area. 		
	void MovementCase()
	{
		if(movement.InControlRegion  )
		{
            direction = movement.dir;
			//Debug.Log(Direction);

			anim.SetBool ("isWalking", true);

            switch (direction)
			{
				
			case "UP":
				orientation = new Vector2(0,1);
				//transform.position += transform.up * Time.deltaTime * movespeed;
				break;
				
			case "DOWN":
				orientation = new Vector2(0,-1);
				//transform.position -= transform.up * Time.deltaTime * movespeed;
				break;
				
			case "RIGHT":
				orientation = new Vector2(1,0);
				//transform.position += transform.right * Time.deltaTime * movespeed;
				break;
			case "LEFT":
				orientation = new Vector2(-1,0);
				//transform.position -= transform.right * Time.deltaTime * movespeed;
				break;
				
			default:
				orientation = new Vector2(0,0);
				break;
			}
			//Debug.Log("vector"+orientation);
		//	if(orientation != Vector2.zero)

			
		//	else
		//	{

			//}

            if (!MovementPause && direction != "CENTER" && !isMoving)
			StartCoroutine(MovePause(orientation));

		}

		else
		{
			orientation = new Vector2(0,0);
			//Debug.Log("walkfalse");
			anim.SetBool("isWalking", false);
		}




	}
	public IEnumerator MovePause( Vector2 temp)
	{
		MovementPause = true;

		if( !isMoving)
		{
			//Debug.Log("entered");
			//orientation = temp;

			//Debug.Log("temp"+temp);
			currentPosition = rBody.position;
			anim.SetFloat ("xInput", orientation.x);
			anim.SetFloat ("yInput", orientation.y);
			//essentially endPosition = currentPosition + (the sign of input(+/-) * grid size	
			endPosition = new Vector2 (currentPosition.x + System.Math.Sign (orientation.x) * gridSize,
		                           currentPosition.y + System.Math.Sign (orientation.y) * gridSize);
		//print ("current position = " + currentPosition);
		
		AttemptMove();
			// yield return new  WaitForSeconds( 0.1f);
			MovementPause = false;

		}
		else
		{yield return new  WaitForSeconds( 0.1f);
		}

			
	}




}
