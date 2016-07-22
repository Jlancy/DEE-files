using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ControlMovement : MonoBehaviour ,IPointerDownHandler, IPointerUpHandler{

	public bool InControlRegion =false;
	public LayerMask layer;
	public string dir;
	public string LastKnownDirection;
	private GameObject player;
	ButtonSwitcher buttonSwitcher;
	private bool nearEdge;
	private bool firstTouch;
	// Use this for initialization

	
	// Update is called once per frame
	void Update () {
	//	Debug.Log ("in"+InControlRegion);

		if (InControlRegion); 
			RaycastCheck();
			//Debug.Log(dir);

		//Debug.Log(dir);
	}



	public void OnPointerDown(PointerEventData data)
	{
		InControlRegion = true;
		dir = "null";
	}
	public void OnPointerUp(PointerEventData data)
	{
		InControlRegion =false;
	}

	public void RaycastCheck()
	{

		Vector2 ray =  Camera.main.ScreenPointToRay(Input.mousePosition).origin ;
		RaycastHit2D hit =Physics2D.Raycast(ray,Vector3.forward*200 ,200,layer);
		
		//Debug.DrawRay(ray, Vector3.forward *200 ,Color.red,15);
		float distanceFromCenter = Vector2.Distance(this.transform.position, ray );

		if (distanceFromCenter >= .6f )
			nearEdge = true;
		else
			nearEdge = false;

		if (hit.collider !=null) {

			dir = hit.transform.name;


		//	if (nearEdge && !firstTouch)
		//		dir = LastKnownDirection;

			if (hit.transform.name !="CENTER")
				LastKnownDirection = hit.transform.name;

			firstTouch = false;
			// Do something with the object that was hit by the raycast.
		}
		else
		{
			if(!nearEdge)
			dir ="CENTER";
		}

	}

	
}

