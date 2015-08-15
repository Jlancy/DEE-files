//original source: http://wiki.unity3d.com/index.php?title=GridMove
using System.Collections;
using UnityEngine;

public abstract class GridMovement : MonoBehaviour {
	//Public variables
	public float moveSpeed = 2f;		//Speed factor
	public float gridSize = 1f;			//Size of grid
	public float xSpawn = 0f;			//Object x-coord spawn
	public float ySpawn = 0f;			//Object y-coord spawn
	public LayerMask blockingLayer;		//Blocking layer to the object

	//Use later for interaction
	protected bool canMove = false;

	//Control variables
	protected bool isMoving = false;
	protected bool movingHori = false;
	protected bool movingVert = false;
	protected Vector3 currentPosition;
	protected Vector3 endPosition;

	//Components
	protected BoxCollider2D bCollider;
	protected Rigidbody2D rBody;

	protected virtual void Start() {
		//Get components 
		//anim = GetComponent<Animator> ();
		bCollider = GetComponent<BoxCollider2D> ();
		rBody = GetComponent<Rigidbody2D> ();

		//Set spawn location
		rBody.position = new Vector2 (xSpawn, ySpawn);
	}

	protected bool Move(out RaycastHit2D hit){
		bCollider.enabled = false;
		//Add a fix here that would check the a pushed object is not still moving
		hit = Physics2D.Linecast (currentPosition, endPosition, blockingLayer);
		bCollider.enabled = true;
		if (hit.transform == null) {
			StartCoroutine (Moving ());
			return true;
		} else 
			return false;
	}

	protected IEnumerator Moving() {
		isMoving = true; 
		float t = 0;

		while (t < 1f) {
			t += Time.deltaTime * (moveSpeed/gridSize);
			rBody.position = Vector3.MoveTowards(currentPosition, endPosition, t);
			yield return null;
		}
		print ("move complete");
		isMoving = false;
		yield return 0;
	}

	protected virtual void AttemptMove<T> ()
		where T : Component
	{
		RaycastHit2D hit;
		canMove = Move (out hit);
		if(hit.transform == null)
			return;
		T hitComponent = hit.transform.GetComponent <T> ();
		if(!canMove && hitComponent != null)
			OnCantMove (hitComponent);
	}

	protected abstract void OnCantMove <T> (T Component)
		where T : Component;
}