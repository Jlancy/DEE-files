using UnityEngine;
using System.Collections;


namespace UnityStandardAssets.CrossPlatformInput
{
public class JoystickInput : MonoBehaviour {
	
	public Joystick joystick;          // Reference to joystick prefab
	public float speed = .25f;         // Movement speed
	public bool useAxisInput = true;   // Use Input Axis or Joystick
	private float h, v;                // Horizontal and Vertical values
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
				} 
				else {
					// Apply horizontal velocity
					// Note: you can just place a - instead of -1*
					GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
				}
			} 
			// Note: you can use and esle if to instead of using else and then nesting if 
			/* watch out it  will complie in order so if its not going to the else if its 
			 * because it still meets
			 * if(!taco)
			 * {
			 * 
			 * }
			 * 
			 * else
			 * {
			 * 	if(taco)
			 * 	{
			 * 
			 * 	}
			 * }
			 * same as 
			 * if(!taco)
			 * {
			 * }
			 * else if(taco)
			 * {
			 * }
			 * else if (pizza)
			 * {
			 * }
			*/
			else if(Mathf.Abs (h) < Mathf.Abs (v))
			{
				if(v>0) {
					// Apply vertical velocity
					GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
				} else {
					// Apply vertical velocity
					// Note: you can just place a - instead of -1*
					GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
				}
			} 
			else {
				GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
			}
			
	}
}
}