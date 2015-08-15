using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float cameraSpeed = 0.1f;
	public float cameraZoom = 6f;
	public float zDistance = -10;
	Camera cam;


	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera> ();

	}
	
	// Update is called once per frame
	void Update () {

		//Normalize resolution
		//Height / 100.0(to float) / scale facter
		cam.orthographicSize = (Screen.height / 100f / cameraZoom);
		if (target) {
			//from, to, speed
			//add 0, 0, 10 to keep distance to view the scene
			transform.position = Vector3.Lerp(target.position, target.position, cameraSpeed) + new Vector3(0, 0, zDistance);
		}
	}
}
