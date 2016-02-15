using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour {
	public bool hit ;
	public bool heal;
	int PlayerHpInital =  100;
	public int CurrentPlayerHp;
	public Slider HpSlide;



	Vector2 MinOrigin;
	Vector2 MaxOrigin;
	public Player player; 


	// Use this for initialization
	void Start () {
		//player = FindObjectOfType<Player>();
        //player = gameObject.tag == "Player";
        PlayerHpInital = player.health;
		CurrentPlayerHp = PlayerHpInital;



	}



	// Update is called once per frame
	void Update () 
	{



		if(hit)
		{
			UpdateHp();
			hit =false;
		}
	}
	void UpdateHp()
	{
		//float HpPercent =  (float)CurrentPlayerHp/ (float)PlayerHpInital;
        //get the health data straight from the player data
        float HpPercent = (float)player.health / (float)player.MaxHealth;
        Debug.Log(HpPercent);
		HpSlide.value = HpPercent;		
	}


}

