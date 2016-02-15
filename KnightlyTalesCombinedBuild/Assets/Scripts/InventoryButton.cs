using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour {
	public AnimationCurve SlideCurve;
	public bool InventoryRect = false;
	float GrowthValue;
	bool b_grow;
	float GrowTime;

	SlotManger inventoryManger;
public RectTransform SlideRect;

	// Use this for initialization
	// growth value use the anchor of a rectTrasnform to get the distance to travle 
	void Start () {
		if(InventoryRect)
		{
		inventoryManger = GameObject.FindGameObjectWithTag("SlotManger").GetComponent<SlotManger>();
		GrowthValue = (this.GetComponent<RectTransform>().offsetMax.x);
		}
		else
		GrowthValue = this.GetComponent<RectTransform>().offsetMin.x;

		b_grow = false;
		//GrowTime =1;
		//SlideRect.offsetMin = new Vector2(GrowthValue ,0);
		//SlideRect.offsetMax = new Vector2(GrowthValue ,0);
	}

	float SlideOut()
	{
		return SlideCurve.Evaluate(GrowTime);
	}
	// Update is called once per frame
	void Update () {

		float slide = SlideOut()* GrowthValue -GrowthValue;
		//Debug.Log(this.GetComponent<RectTransform>().offsetMin.x);
		//Debug.Log(this.GetComponent<RectTransform>().offsetMin.y);
		//Debug.Log(this.GetComponent<RectTransform>().offsetMax.x);
		//Debug.Log(this.GetComponent<RectTransform>().offsetMax.y);
		if(b_grow)
		{
			if (GrowTime <1)
			{

			GrowTime += Time.deltaTime;

			SlideRect.offsetMin = new Vector2(slide ,0);
			SlideRect.offsetMax = new Vector2(slide ,0);
			}
			else 
				GrowTime = 1;

		}
		else
		{
			if(GrowTime>0)
			{
				GrowTime -= Time.deltaTime;
				
			SlideRect.offsetMin = new Vector2(slide ,0);
			SlideRect.offsetMax = new Vector2(slide ,0);
			}
			else
				GrowTime = 0;

		}
	
	}
	public void SlideBool()
	{
		if(b_grow)
			b_grow = false;
		
		else
		{
			if(InventoryRect)
			inventoryManger.updateCheck = true;
			
			b_grow = true;
		}
	}
}

