using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour {
	public bool hit ;
	public bool heal;
	int PlayerHpInital =  100;
	public int CurrentPlayerHp;
	public Slider HpSlide;


	Player player; 

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Player>();
		PlayerHpInital = player.health;
		CurrentPlayerHp = PlayerHpInital;



	}



	// Update is called once per frame
	void Update () 
	{
		if (hit)
		{
			UpdateHp();
			hit =false;
		}
	}
	void UpdateHp()
	{
		float HpPercent =  (float)CurrentPlayerHp/ (float)PlayerHpInital;
		Debug.Log(HpPercent);
		HpSlide.value = HpPercent;		
	}


}

