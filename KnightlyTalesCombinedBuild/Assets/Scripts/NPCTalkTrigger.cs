using UnityEngine;
using System.Collections;

public class NPCTalkTrigger : MonoBehaviour {

	public LayerMask NPC_Layer;
	bool RaycastForNPC; 
	ControlMovement controlMovement;
	ButtonSwitcher buttonSwitcher;
	Vector2 PlayerPos;
	bool FoundNPC;
	NPC npc;
	Inventory inventroy;
	// Use this for initialization
	void Start () 
	{
		controlMovement = FindObjectOfType<ControlMovement>();
		buttonSwitcher = FindObjectOfType<ButtonSwitcher>();
		npc = this.transform.GetComponentInParent<NPC>();
		//Debug.Log(npc.name);
		inventroy = FindObjectOfType<Inventory>();
	}
	void Update()
	{
		if(!FoundNPC)
		{
			if(RaycastForNPC)
			{
				RaycastCase();
			}
		}
	}



	void OnTriggerEnter2D(Collider2D player)
	{
		if(player.transform.tag == "Player")
		{
			CheckIfPLayerHasItem();
			PlayerPos = player.transform.position;
			RaycastForNPC = true;
		}
	}

	void OnTriggerExit2D(Collider2D player)
	{
		if(player.transform.tag == "Player")
		{
			RaycastForNPC = false;
			FoundNPC = false;
			buttonSwitcher.buttonState = ButtonSwitcher.ButtonState.Attack;
			buttonSwitcher.ButtonChange = true;
		}
	}


	
	void CheckIfFacingNPC(Vector2 RayDirection)
	{
		
		RaycastHit2D hit =Physics2D.Raycast(PlayerPos ,RayDirection,10,NPC_Layer);


		if(hit.transform != null)
		{
			//Debug.Log(hit.transform.tag);
			if(hit.transform.tag == "NPC")
			{
				FoundNPC = true;
				buttonSwitcher.npc =  hit.transform.gameObject.GetComponent<NPC>();
				buttonSwitcher.buttonState = ButtonSwitcher.ButtonState.Talk;
				buttonSwitcher.ButtonChange = true;


			}
			
		}
		
		
	}
	void CheckIfPLayerHasItem()
	{
		//Debug.Log(inventroy.Items.Contains(npc.questVillager._RequiredItem));
		if(inventroy.Items.Contains(npc.questVillager._RequiredItem));
		{
			npc.PlayerHasItem = true;
		}
		  
	}
	
	void RaycastCase()
	{
		switch(controlMovement.dir)
		{

		case "UP":
			CheckIfFacingNPC(Vector2.up);
			break;
			
		case "DOWN":
			CheckIfFacingNPC(-Vector2.up);
			break;
			
		case "RIGHT":

			CheckIfFacingNPC(Vector2.right);
			break;
		case "LEFT":
			CheckIfFacingNPC(-Vector2.right);
			break;
			
		default:
			
			break;
		}
	}
}
