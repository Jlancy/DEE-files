using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	//public float cameraSpeed = 0.01f;
    public float dampTime = 0.3f;
	public float cameraZoom = 6f;
	public float zDistance = -10;
    private Vector3 velocity = Vector3.zero;
	Camera cam;


	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera> ();
        //Attempt at removing initial motion sickness
        transform.position = target.position + new Vector3(0f, 0f, zDistance);
	}
	
	// Update is called once per frame
	void Update () {

		//Normalize resolution
		//Height / 100.0(to float) / scale facter
		cam.orthographicSize = (Screen.height / 100f / cameraZoom);
		if (target) {
			//Hard Camera Follow
            //from, to, speed
			//add 0, 0, 10 to keep distance to view the scene
            //Vector3 delta = target.position - cam.ViewportToWorldPoint(Vector3(0.5, 0.5, point.z));
			//transform.position = Vector3.Lerp(target.position, target.position, cameraSpeed) + new Vector3(0, 0, zDistance);

            //Added Damp Time for Delay Camera Follow
            Vector3 point = cam.WorldToViewportPoint(target.position);
            //Keep it at 0.5f, 0.5f to keep object of interest in center
            Vector3 delta = target.position - cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));   
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}
	}
}
