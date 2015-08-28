using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ControlMovment : MonoBehaviour ,IDragHandler {

	public bool InControlRegion =false;
	RaycastHit2D hit;
	public LayerMask layer;
	public string dir;

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {

		//Debug.Log("derp");
	}

	public void OnDrag (PointerEventData data)

	{
		InControlRegion = true;
		Vector2 ray =  Camera.main.ScreenPointToRay(Input.mousePosition).origin ;
		RaycastHit2D hit =Physics2D.Raycast(ray,Vector3.forward,20,layer);


		//Debug.Log(ray.origin);
		if (hit.collider !=null) {
			Debug.Log("wellthen");
			Debug.Log(hit.transform.gameObject.name);
			dir = hit.transform.gameObject.name;
			
			// Do something with the object that was hit by the raycast.
		}

	}

	
}

