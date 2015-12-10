using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ControlMovement : MonoBehaviour , IPointerUpHandler , IPointerDownHandler{

	public bool InControlRegion =false;
	public LayerMask layer;
	public string dir;
	public string LastKnownDirection;
	private GameObject player;
	ButtonSwitcher buttonSwitcher;

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void LateUpdate () {
	//	Debug.Log ("in"+InControlRegion);
		if(InControlRegion)
		{
			 
			RaycastCheck();
			//Debug.Log(dir);
		}
		//Debug.Log(dir);
	}


	public void OnPointerDown(PointerEventData data)	
	{
		//Debug.Log("inRegion");
		InControlRegion = true;
		
		
	}

	public void OnPointerUp(PointerEventData data)
	{
		InControlRegion = false;
		dir = "CENTER";
	}


	public void RaycastCheck()
	{

		Vector2 ray =  Camera.main.ScreenPointToRay(Input.mousePosition).origin ;
		RaycastHit2D hit =Physics2D.Raycast(ray,Vector3.forward*200 ,200,layer);
		
		Debug.DrawRay(ray, Vector3.forward *200 ,Color.red,15);

		if (hit.collider !=null) {

			dir = hit.transform.name;
			if (hit.transform.name !="CENTER")
				LastKnownDirection = hit.transform.name;
			// Do something with the object that was hit by the raycast.
		}
		else
			dir ="CENTER";

	}

	
}

