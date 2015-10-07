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
	Vector2 MinOrigin;
	Vector2 MaxOrigin;
	Vector2 MinEnd;
	Vector2 MaxEnd;
	public float TargetY = 100;
	Button button;
	Image image;
	RectTransform ButtomRect;
	bool ButtonSet;
	public NPC npc;
    public Player player;

	// Use this for initialization
	void Start () {
		ButtomRect = GetComponent<RectTransform>();

		MinOrigin = ButtomRect.offsetMin;
		MaxOrigin = ButtomRect.offsetMax;
		//TargetPosition = new Vector2(transform.position.x, transform.position.y - TargetY);
		MinEnd = new Vector2(MinOrigin.x, MinOrigin.y - TargetY);
		MaxEnd = new Vector2(MaxOrigin.x, MaxOrigin.y - TargetY);;
		button = this.GetComponent<Button>();
		buttonState = ButtonState.Attack;
        player = FindObjectOfType<Player>();
        //Initail call of SwitchButton so we can attack at start
        SwitchButton();

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
                button.onClick.AddListener(() => { Attack(); });
                break;
			case ButtonState.Talk:
                button.image.color = Color.red;
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
				ButtomRect.offsetMin = Vector2.Lerp(MinOrigin,MinEnd,Lerp);
				ButtomRect.offsetMax = Vector2.Lerp(MaxOrigin,MaxEnd,Lerp);
				//button.transform.position = Vector2.Lerp(OriginPosition,TargetPosition, Lerp);
			}
			else
			{
				MoveBack = true;
			}

			if(MoveBack )
			{
				timer -=  Time.deltaTime ;
				Lerp = timer/LerpRate;
				ButtomRect.offsetMin = Vector2.Lerp(MinOrigin,MinEnd,Lerp);
				ButtomRect.offsetMax = Vector2.Lerp(MaxOrigin,MaxEnd,Lerp);
				//button.transform.position = Vector2.Lerp(OriginPosition,TargetPosition, Lerp);
			
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
        if (player != null)
        player.Attack();
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
