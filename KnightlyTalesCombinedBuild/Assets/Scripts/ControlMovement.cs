using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ControlMovement : MonoBehaviour ,IPointerEnterHandler , IPointerExitHandler, IPointerUpHandler , IPointerDownHandler{

	public bool InControlRegion =false;
	public LayerMask layer;
	public string dir;
	string LastKnownDirection;
	private GameObject player;
	ButtonSwitcher buttonSwitcher;

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {

		if(InControlRegion)
		{
			 
			RaycastCheck();
			//Debug.Log(dir);
		}
		//Debug.Log("derp");
	}

	public void OnPointerEnter(PointerEventData data)
	{
		//Debug.Log("inRegion");
		InControlRegion = true;


	}
	public void OnPointerDown(PointerEventData data)	
	{
		//Debug.Log("inRegion");
		InControlRegion = true;
		
		
	}
	public void OnPointerExit(PointerEventData data)
	{
		InControlRegion = false;
		dir = "CENTER";
	}
	public void OnPointerUp(PointerEventData data)
	{
		InControlRegion = false;
		dir = "CENTER";
	}


	public void RaycastCheck()
	{

		Vector2 ray =  Camera.main.ScreenPointToRay(Input.mousePosition).origin ;
		RaycastHit2D hit =Physics2D.Raycast(ray,Vector3.forward,20,layer);
		
		//Debug.Log(hit.transform.name);
		//Debug.Log(ray.origin);
		if (hit.collider !=null) {

			dir = hit.transform.name;
			if (hit.transform.name !="CENTER")
				LastKnownDirection = hit.transform.name;
			// Do something with the object that was hit by the raycast.
		}
		else
		{
			dir = "CENTER";
		}
	}

	
}

