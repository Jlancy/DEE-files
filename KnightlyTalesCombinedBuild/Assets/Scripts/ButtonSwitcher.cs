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
	public LayerMask IgnoreMask;

	float Lerp;
	[RangeAttribute(0.0f,1)]
	public float LerpRate =0.1f;
	float timer;
	bool MoveBack = false;
	Vector2 MinOrigin;
	Vector2 MaxOrigin;
	Vector2 MinEnd;
	Vector2 MaxEnd;
	public float TargetY = 100;
	public Button button;
	Image image;
	RectTransform ButtomRect;
	bool ButtonSet =false;
	public NPC npc;
    public Player player;
	public GameObject CurrentTarget;
	ControlMovement controlMovement;
	TextTyper textTyper;

	// Use this for initialization
	void Start () {
		controlMovement = FindObjectOfType<ControlMovement>();
		ButtomRect = GetComponent<RectTransform>();

		MinOrigin = ButtomRect.offsetMin;
		MaxOrigin = ButtomRect.offsetMax;
		//TargetPosition = new Vector2(transform.position.x, transform.position.y - TargetY);
		MinEnd = new Vector2(MinOrigin.x, MinOrigin.y - TargetY);
		MaxEnd = new Vector2(MaxOrigin.x, MaxOrigin.y - TargetY);;
		button = this.GetComponent<Button>();
		buttonState = ButtonState.Attack;
        player = FindObjectOfType<Player>();
		textTyper = FindObjectOfType<TextTyper>();
        //Initail call of SwitchButton so we can attack at start
		buttonState = ButtonState.Attack;
        SwitchButton();

	}
	
	// Update is called once per frame
	void Update () {
		RaycastCase();


		LerpButton();

		if(npc== null)
		{
			textTyper.EndText();
		}
		//SwitchButton();
	}


	void SwitchButton()
	{
			//Debug.Log(buttonState);
			button.onClick.RemoveAllListeners();
		//Debug.Log(buttonState);
		Debug.Log("entered");
			switch(buttonState)
			{

			case ButtonState.Attack:
			Debug.Log("attack");
			if(npc !=null) 
			{
			npc.IsTalking =false;
			npc = null;
			}
				button.image.color = Color.blue;
                button.onClick.AddListener(() => { Attack(); });
                break;
			case ButtonState.Talk:
			Debug.Log("enterTalk");
                button.image.color = Color.red;
				button.onClick.AddListener( () => { Talk();});

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

			ButtonSet =false;
			//Debug.Log(Lerp <= 1);
			if(!MoveBack && Lerp <= 1)
			{
				timer +=  Time.deltaTime ;
				Lerp = timer/LerpRate;

				if(Lerp >=	0.8f)
				{
					Debug.Log(ButtonSet);
					if(!ButtonSet){

					SwitchButton();
					}
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

		//Destroy(CurrentTarget);
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


	void RaycastCase()
	{

		switch(controlMovement.LastKnownDirection)
		{

		case "UP":
			RaycastCheckForObject(Vector2.up);
			break;
			
		case "DOWN":
			RaycastCheckForObject(-Vector2.up);
			break;
			
		case "RIGHT":
			
			RaycastCheckForObject(Vector2.right);
			break;
		case "LEFT":
			//Debug.Log("left");
			RaycastCheckForObject(-Vector2.right);
			break;
			
		default:
			
			break;
		}
	}

	void RaycastCheckForObject(Vector2 RayDirection)
	{
		//make sure the button is already switching
		if(!ButtonChange)
		{	
			RaycastHit2D hit = Physics2D.Raycast(player.transform.position ,RayDirection,1,IgnoreMask);
			// chech if its hitting something
			if(hit.transform != null)
			{
				// check if hit dosent match currentTarget accordingly
				if (hit.transform.gameObject != CurrentTarget)
				{
					// check by tag and set buttonstate according
					if(hit.transform.tag == "NPC")
					{
						CurrentTarget = hit.transform.gameObject;
						npc = hit.transform.gameObject.GetComponent<NPC>();
						buttonState = ButtonState.Talk;
						ButtonChange =true;
						
						
					}

				}
			}
			else if(CurrentTarget != null)
			{
				CurrentTarget = null;
				ButtonChange = true;
				buttonState = ButtonState.Attack;
	
			}
		}
		
	}


}
