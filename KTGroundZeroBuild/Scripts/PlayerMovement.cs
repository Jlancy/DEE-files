using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	Rigidbody2D rBody;
	Animator anim;
	// Use this for initialization
	void Start () {
		rBody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 movementVector = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
	
		if (movementVector != Vector2.zero) {
			anim.SetBool ("isWalking", true);
			anim.SetFloat ("inputX", movementVector.x);
			anim.SetFloat ("inputY", movementVector.y);
		} 
		else
			anim.SetBool ("isWalking", false);

		if(Input.GetKey(KeyCode.T))
			anim.SetBool ("tIsPress", true);
		else
		   	anim.SetBool ("tIsPress", false);

		rBody.MovePosition (rBody.position + movementVector * Time.deltaTime);
	}
}
