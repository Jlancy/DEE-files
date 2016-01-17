using UnityEngine;
using System.Collections;

public enum EnemyType : int {
	Melee, Ranged
};

public class Enemy : MovableObject {
	public float sightRange = 5f;
	public int Hp = 10;
	private Transform target;	//
	public EnemyType enemyType = EnemyType.Melee;
	private Vector2 targetPosition;
	private Vector2 directionVector ;
	private bool reachedTarget =false;
	public GameObject Arrow;

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
			else  if (enemyType == EnemyType.Ranged)
			{
				

			
				ChaseTarget();
				if(reachedTarget)
				{
					PlayerInSight();
				}
				else{
					anim.SetBool ("isAttacking", false);
				}

			}
		}
		if( Hp <= 0)
		{
			Destroy(this.gameObject);
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
		//Vector2 directionVector ;
		targetPosition = target.transform.position; //Player Position
		if(enemyType == EnemyType.Ranged)
		{
			SetTargetPosition();
		}
		Vector2 distanceVector = new Vector2 (targetPosition.x - currentPosition.x, targetPosition.y - currentPosition.y); //Player pos - Entity pos


        //Distance between 2 points
		// D = sqrt((x - X)^2 + (y - Y)^2)
		float playerDistance = Mathf.Sqrt (Mathf.Pow (distanceVector.x, 2) + Mathf.Pow (distanceVector.y, 2));
		reachedTarget = playerDistance <= .9f ? true : false;


		if (playerDistance <= sightRange && !reachedTarget) {
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
			Vector2 BowAim = target.position - this.transform.position;
			directionVector = BowAim.normalized;
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
			//print ("enemy move successful");	//insert stepping sound
		} else {	
			//print ("enemy move failed");		//insert bumping sound
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
	public void LoseHp(int damage)
	{
		StartCoroutine(ColorFlash());
		Hp -= damage;
	}
	IEnumerator ColorFlash()
	{	Color32 flash = new Color32(255,116,116,255);
		SpriteRenderer enemySprite=  this.GetComponent<SpriteRenderer>();

		for(int i = 0; i <3; i++)
		{
			Debug.Log("R");

			enemySprite.color = flash;
			yield return new WaitForSeconds(.06f);
			Debug.Log("W");
			enemySprite.color = Color.white;
			yield return new WaitForSeconds(.06f);
		}
	}

	//called in RangedEnemyAttack Animation
	void FireArrow()
	{
		GameObject SpawnArrow;
		Vector2 SpawnPoint = (Vector2)this.transform.position + directionVector;
		// adjust
		SpawnPoint = new Vector2(SpawnPoint.x +.5f,SpawnPoint.y+.2f);
		float SpawnAngle = Mathf.Atan2(directionVector.x,directionVector.y) * 180/Mathf.PI;

		Quaternion SpawnDirection =  Quaternion.Euler(new Vector3(0,0,SpawnAngle-90));
		//SpawnDirection = new Quaternion(SpawnDirection.x,SpawnDirection.y,SpawnDirection.z +90,SpawnDirection.w);
		SpawnArrow = Instantiate(Arrow,SpawnPoint,SpawnDirection) as GameObject;

		
	}
	void PlayerInSight()
	{
			

		anim.SetFloat ("xInput", directionVector.x);
		anim.SetFloat ("yInput", directionVector.y);
		anim.SetBool ("isAttacking", true);
	}

	void SetTargetPosition()
	{
		// temp  
		//adjust to find close x or y axis from enemy to player,
		// then  make a point that is a some distance away from the player 
		// as the new target
		targetPosition =  targetPosition +  new Vector2 (3,0);


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