using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonSwitcher : MonoBehaviour {

	public bool ButtonChange  = false;
	public float ButtonFlipRate = 100;
	public ButtonState buttonState;
	public enum ButtonState
	{
		Attack,
		Talk,
		Shop
	}

	float Lerp;
	[RangeAttribute(0.1f,1)]
	public float LerpRate =0.1f;
	float timer;
	bool MoveBack = false;
	Vector2 OriginPosition;
	Vector2 TargetPosition;
	public float TargetY = 100;
	Button button;
	Image image;

	bool ButtonSet;
	public NPC npc;

	// Use this for initialization
	void Start () {
		OriginPosition = this.transform.position;
		TargetPosition = new Vector2(transform.position.x, transform.position.y - TargetY);
		button = this.GetComponent<Button>();
		buttonState = ButtonState.Attack;




	}
	
	// Update is called once per frame
	void Update () {
		LerpButton();
		//SwitchButton();
	}


	void SwitchButton()
	{
			//Debug.Log(buttonState);
			button.onClick.RemoveAllListeners();
				
			switch(buttonState)
			{
			case ButtonState.Attack:
			npc = null;
				button.image.color = Color.blue;
				break;
			case ButtonState.Talk:
				
				button.onClick.AddListener(() => { Talk();});

				break;
			case ButtonState.Shop:
			npc= null;
				button.image.color = Color.green;
				break;
			default:
				break;
			}
		ButtonSet = true;
	}

	void LerpButton()
	{	
		if(ButtonChange)
		{

			//Debug.Log(Lerp <= 1);
			if(!MoveBack && Lerp <= 1)
			{
				timer +=  Time.deltaTime ;
				Lerp = timer/LerpRate;

				if(Lerp >=	0.8f)
				{
					if(!ButtonSet)
					SwitchButton();
				}

				button.transform.position = Vector2.Lerp(OriginPosition,TargetPosition, Lerp);
			}
			else
			{
				MoveBack = true;
			}

			if(MoveBack )
			{
				timer -=  Time.deltaTime ;
				Lerp = timer/LerpRate;
				button.transform.position = Vector2.Lerp(OriginPosition,TargetPosition, Lerp);
			
				if(Lerp <= 0)
				{
					ButtonChange =false;
					MoveBack =false;
					timer = 0;
					ButtonSet =false;
				}
			}

		}
		//else 
		//	timer = 0;
	}

	void Attack()
	{
	}

	void Talk()
	{
//		Debug.Log(npc.questVillager._RequiredItem.itemName);
		if(npc != null)
		npc.QuestTalk();
	}

	void Shop()
	{
	}
}
