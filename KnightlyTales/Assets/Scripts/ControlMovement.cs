using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ControlMovement : MonoBehaviour ,IPointerEnterHandler , IPointerExitHandler, IPointerUpHandler{

	public bool InControlRegion =false;
	public LayerMask layer;
	public string dir;
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
		}
		//Debug.Log("derp");
	}

	public void OnPointerEnter(PointerEventData data)

	{
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
		
		
		//Debug.Log(ray.origin);
		if (hit.collider !=null) {

			dir = hit.transform.gameObject.name;
			
			// Do something with the object that was hit by the raycast.
		}
		else
		{
			dir = "CENTER";
		}
	}

	
}

