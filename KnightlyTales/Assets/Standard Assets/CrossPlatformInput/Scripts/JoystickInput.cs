using UnityEngine;
using System.Collections;


namespace UnityStandardAssets.CrossPlatformInput
{
public class JoystickInput : MonoBehaviour {
	
	public Joystick joystick;           // Reference to joystick prefab
	public float speed = .25f;             // Movement speed
	public bool useAxisInput = true;   // Use Input Axis or Joystick
	private float h, v;         // Horizontal and Vertical values
	private float originalH, originalV;
	
	void Start () {
			originalH = joystick.transform.position.x;
			originalV = joystick.transform.position.y;
	}

		// Update is called once per frame
	void Update () {
		// Get horizontal and vertical input values from either axis or the joystick.
		if (!useAxisInput) {
				h = joystick.transform.position.x - originalH;
				v = joystick.transform.position.y - originalV;
		}
		else {
			h = Input.GetAxis("Horizontal");
			v = Input.GetAxis("Vertical");
		}

			if (Mathf.Abs (h) > Mathf.Abs (v)) {
				if(h>0) {
				// Apply horizontal velocity
				GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
				} else {
					// Apply horizontal velocity
					GetComponent<Rigidbody2D>().velocity = new Vector2(-1*speed, 0);
				}
			} else {
				if(Mathf.Abs (h) < Mathf.Abs (v))
				{
					if(v>0) {
						// Apply vertical velocity
						GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
					} else {
						// Apply vertical velocity
						GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1*speed);
					}
				} else {
					GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
				}
			}
	}
}
}