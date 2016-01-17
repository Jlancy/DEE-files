using UnityEngine;
using System.Collections;

public class ArrowMove : MonoBehaviour {

	public float speed = 3;
	// Update is called once per frame
	void Update () {
		transform.position += transform.right * Time.deltaTime * speed;
	}
}
