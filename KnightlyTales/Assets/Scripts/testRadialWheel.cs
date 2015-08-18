using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class testRadialWheel : MonoBehaviour {

	RectTransform rect;
	bool Up1 = false;
	// Use this for initialization
	void Start () {
		rect = this.GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
	

		if(Up1)
		{
			rect.Rotate(0,0,-Time.deltaTime * 50);
		}
		else
		{
			rect.Rotate(0,0,Time.deltaTime * 50);
		}
	}
	void Up()
	{
		Up1 = true;
	}

	void Down()
	{
		Up1 = false;
		Debug.Log("down");
	}
}
