using UnityEngine;
using System.Collections;

public class Player : GridMovement {
	Animator anim;
	bool isDoingSomething = false;		//can change to busy later, flag that the player is doing something besides moving
	public int health = 20;			    //health counter
	public int MaxHealth= 20;
    ControlMovement movement;
    private string direction;

	bool MovementPause =false;

	public int SwordAttack;
	public int BowAttack;
	public int QuiverSize;
	ButtonSwitcher buttonSwitcher;

	protected override void Start () {
		base.Start ();
		anim = GetComponent<Animator> ();
		movement = FindObjectOfType<ControlMovement>();
		buttonSwitcher = FindObjectOfType<ButtonSwitcher>();

	}

	private void Update () {
		MovementCase();
        //==========================================================================================
        // added to later test health lost
        //==========================================================================================
        if (health <= 0)
        {
            //==========================================================================================
            // Insert game over
            //==========================================================================================
            Destroy(this.gameObject);
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
        StartCoroutine(ColorFlash());
        health -= damage;
		//print ("Player takes " + damage + "damage!");
	}

	protected IEnumerator Attacking() {
		buttonSwitcher.button.enabled =false;
		isDoingSomething = true;
       
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
		
        while (anim.GetBool("isAttacking")) {
			//t += Time.deltaTime;
			//rBody.position = Vector3.MoveTowards(currentPosition, endPosition, t);
			yield return null;
		}
        

        //Possible alternative to the above but only takes int <- limited
        //yield return new WaitForSeconds(1);

        //End the animation
		buttonSwitcher.button.enabled =true;
		isDoingSomething = false;
		yield return 0;
	}

	protected override void AttemptMove (){
		base.AttemptMove ();
		RaycastHit2D hit;
		if (Move (out hit)) {
			//print ("move successful");	//insert stepping sound
		} else {	
			//print ("move failed");		//insert bumping sound
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
		
			if (orientation != Vector2.zero)
			{
            if ( direction != "CENTER" && !isMoving)
			{
				currentPosition = rBody.position;
				anim.SetFloat ("xInput", orientation.x);
				anim.SetFloat ("yInput", orientation.y);
				//essentially endPosition = currentPosition + (the sign of input(+/-) * grid size	
				endPosition = new Vector2 ((int)currentPosition.x + System.Math.Sign (orientation.x) * gridSize,
											(int)currentPosition.y + System.Math.Sign (orientation.y) * gridSize);
				//print ("current position = " + currentPosition);
				
				AttemptMove();
			}
			}
			else
				anim.SetBool("isWalking", false);

			//StartCoroutine(MovePause(orientation));

		}

		else
		

			//Debug.Log("walkfalse");
			anim.SetBool("isWalking", false);
		




	}
	public IEnumerator MovePause( Vector2 temp)
	{
		MovementPause = true;

		if( !isMoving)
		{
			//Debug.Log("entered");
			//orientation = temp;

			//Debug.Log("temp"+temp);

			yield return new  WaitForSeconds( 0f);
			MovementPause = false;

		}


			
	}

	// get called as an event in each Attack animation
	void EndAttackAnim()
	{
		anim.SetBool ("isAttacking", false);
	}

	void RaycastForEnemy()
	{

			
	StartCoroutine(RayCheck());

		Vector2 playerDirection = new Vector2(anim.GetFloat("xInput"),anim.GetFloat("yInput"));
		Debug.Log("dir"+ playerDirection);
		RaycastHit2D hit = Physics2D.Raycast(this.transform.position ,playerDirection,1,blockingLayer);
	    if(hit.transform.tag == "Enemy")
	    {
		
		    hit.transform.GetComponent<Enemy>().LoseHp(SwordAttack);
	    }

		

	}


IEnumerator RayCheck()
{
	bool foundEnemy = false;
	while(!foundEnemy)
	{
		
		Vector2 playerDirection = new Vector2(anim.GetFloat("xInput"),anim.GetFloat("yInput"));
		RaycastHit2D hit = Physics2D.Raycast(this.transform.position ,playerDirection,1,blockingLayer);
		if(hit.transform !=null)
		{
			if(hit.transform.tag == "Enemy")
			{
				hit.transform.GetComponent<Enemy>().LoseHp(SwordAttack);
				foundEnemy =true;
			}
			if(!anim.GetBool("isAttacking"))
			{
				foundEnemy =true;
			}
		}
		yield return null;
	}
}

    IEnumerator ColorFlash()
    {
        Color32 flash = new Color32(255, 116, 116, 255);
        SpriteRenderer playerSprite = this.GetComponent<SpriteRenderer>();

        for (int i = 0; i < 3; i++)
        {
            Debug.Log("R");

            playerSprite.color = flash;
            yield return new WaitForSeconds(.06f);
            Debug.Log("W");
            playerSprite.color = Color.white;
            yield return new WaitForSeconds(.06f);
        }
    }


}
