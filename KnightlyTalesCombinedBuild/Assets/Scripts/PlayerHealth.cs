using UnityEngine;
using System.Collections;
using UnityEngine.UI;


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
	public bool heal;
	int PlayerHpInital =  20;
	public int CurrentPlayerHp;
	public Slider HpSlide;

	Vector2 MinOrigin;
	Vector2 MaxOrigin;
	Player player; 

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Player>();
		PlayerHpInital = player.health;
		CurrentPlayerHp = PlayerHpInital;
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
			
			}
			else 
			{
				border.offsetMax = MaxOrigin;
				border.offsetMin = MinOrigin;
				time = 0;
				hit = false;
			}
		}

		if( heal)
		{
			if(time<.3f)
			{
				ShakeHeal();
				
			}
			else 
			{
				border.offsetMax = MaxOrigin;
				border.offsetMin = MinOrigin;
				time = 0;
				heal = false;
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
		UpdateHp();
		float slide_X = (X_AxisCurve()* X_AxisDistance - X_AxisDistance) ;

		time += Time.deltaTime;
		
		border.offsetMax = new Vector2(slide_X + MaxOrigin.x , MaxOrigin.y);
		border.offsetMin = new Vector2(slide_X + MinOrigin.x , MaxOrigin.y);

	}

	void ShakeHeal()
	{
		UpdateHp();
		float slide_Y = Mathf.Abs(Y_AxisCurve()* Y_AxisDistance - Y_AxisDistance);

		
		time += Time.deltaTime;
		
		border.offsetMax = new Vector2( MaxOrigin.x ,slide_Y + MaxOrigin.y);
		border.offsetMin = new Vector2( MinOrigin.x ,slide_Y + MaxOrigin.y);
	}

}

