using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace KnightlyTales
{
	public class PlayerHealth : MonoBehaviour {
		RectTransform border;

		public AnimationCurve X_Axis;
		public AnimationCurve Y_Axis;
		[Range(0,30)]
		public float Y_AxisDistance;
		[Range(0,30)]
		public float X_AxisDistance;
		float time;
		public bool hit ;
		int PlayerHpInital =  20;
		public int CurrentPlayerHp;
		public Slider HpSlide;

		Vector2 MinOrigin;
		Vector2 MaxOrigin;


		// Use this for initialization
		void Start () {
			border = this.gameObject.GetComponent<RectTransform>();
			MinOrigin = border.offsetMin;
			MaxOrigin = border.offsetMax;


		}


		float X_AxisCurve()
		{
			return X_Axis.Evaluate(time);
		}
		float Y_AxisCurve()
		{
			return Y_Axis.Evaluate(time);
		}
		// Update is called once per frame
		void Update () 
		{

			if(hit)
			{

				if(time<.5f)
				{
				ShakeHit();
				UpdateHp();
				}
				else 
				{
					border.offsetMax = MaxOrigin;
					border.offsetMin = MinOrigin;
					time = 0;
					hit = false;
				}
			}
		}
		void UpdateHp()
		{
			float HpPercent =  (float)CurrentPlayerHp/ (float)PlayerHpInital;
			Debug.Log(HpPercent);
			HpSlide.value = HpPercent;		
		}

		void ShakeHit()
		{
			float slide_Y = (Y_AxisCurve()* Y_AxisDistance - Y_AxisDistance);
			float slide_X = (X_AxisCurve()* X_AxisDistance - X_AxisDistance) ;

			time += Time.deltaTime;
			
			border.offsetMax = new Vector2(slide_X + MaxOrigin.x ,slide_Y + MaxOrigin.y);
			border.offsetMin = new Vector2(slide_X + MinOrigin.x ,slide_Y + MaxOrigin.y);

		}
	}
}
