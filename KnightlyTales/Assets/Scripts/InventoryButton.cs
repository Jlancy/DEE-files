using UnityEngine;
using System.Collections;
using UnityEngine.UI;

	public class InventoryButton : MonoBehaviour {
		public AnimationCurve SlideCurve;
		float GrowthValue;
		bool b_grow;
		float GrowTime;

		SlotManger inventoryManger;
		public RectTransform InventoryRect;

		// Use this for initialization
		void Start () {
			inventoryManger = GameObject.FindGameObjectWithTag("SlotManger").GetComponent<SlotManger>();
			GrowthValue = Mathf.Abs(this.GetComponent<RectTransform>().offsetMax.x);
			b_grow = false;
			//GrowTime =1;
			InventoryRect.offsetMin = new Vector2(GrowthValue ,0);
			InventoryRect.offsetMax = new Vector2(GrowthValue ,0);
		}

		float SlideOut()
		{
			return SlideCurve.Evaluate(GrowTime);
		}
		// Update is called once per frame
		void Update () {

			float slide = Mathf.Abs(SlideOut()* GrowthValue -GrowthValue);
			//Debug.Log(this.GetComponent<RectTransform>().offsetMin.x);
			//Debug.Log(this.GetComponent<RectTransform>().offsetMin.y);
			//Debug.Log(this.GetComponent<RectTransform>().offsetMax.x);
			//Debug.Log(this.GetComponent<RectTransform>().offsetMax.y);
			if(b_grow)
			{
				if (GrowTime <1)
				{

				GrowTime += Time.deltaTime;

				InventoryRect.offsetMin = new Vector2(slide ,0);
				InventoryRect.offsetMax = new Vector2(slide ,0);
				}
				else 
					GrowTime = 1;

			}
			else
			{
				if(GrowTime>0)
				{
					GrowTime -= Time.deltaTime;
					
					InventoryRect.offsetMin = new Vector2(slide ,0);
					InventoryRect.offsetMax = new Vector2(slide ,0);
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
				
				inventoryManger.updateCheck = true;
				b_grow = true;
			}
		}
	}

