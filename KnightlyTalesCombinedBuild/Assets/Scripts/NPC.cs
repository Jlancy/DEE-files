using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class NPC : MonoBehaviour {
	public string DefaultPharse;
	string TestString;


	
	DialogueDataBase DialogueList;
	DialogueGenerator DialogueTemp;
	QuestManager QuestGenerate;
	Inventory inventory;
	public Quest questVillager;
	string tempInstructions;

	//int i = 0 ;
	public Vector2 centerPointMap = new Vector2 (20,20);

	
	TextTyper Talk;

	Image Portrait;
	public bool PlayerHasItem = false;
	public bool IsTalking = false;

	string NPC_Name;
	
	// Use this for initialization
	void Start () {


	DialogueList = FindObjectOfType<DialogueDataBase>();
	DialogueTemp = GameObject.FindGameObjectWithTag("Item Database").GetComponent<DialogueGenerator>();
	QuestGenerate    = GameObject.FindGameObjectWithTag("Item Database").GetComponent<QuestManager>();
	inventory = FindObjectOfType<Inventory>();
	Talk = FindObjectOfType<TextTyper>();

		TestString = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
			"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
			"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
			"AAAAAAAAAAAAAAAAAAbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb" +
			"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";



	}
	
	// Update is called once per frame

	void Update()
	{

	/*
		if(IsTalking )
		{
			if (!Talk.Typing)
			{

				if(FinishChat)
				{
					Talk.SkipText =false;
					Talk.EndText();
					if(PlayerHasItem)
					{
						QuestGenerate.QuestCompletedCheck(questVillager);
					}	
					Debug.Log("hmmm");


						if( !Talk.TalkContinued){
						Talk.EndText();
						IsTalking = false;
						SkipChat = false;
						FinishChat = false;
						}


				}


			}
			else if (SkipChat)
			{
				Talk.SkipText =true;
				SkipChat =false;
			}

		}
*/
	}

	public void QuestTalk()
	{
		// have the npc start talking
		if(!PlayerHasItem)
		{
			PlayerHasItem = inventory.CheckItem(questVillager._RequiredItem);
		}

		if(!IsTalking && !Talk.Typing )
		{
			
				if(!PlayerHasItem)
				Talk.StartText(questVillager._QuestInstructions);

				else
				Talk.StartText(questVillager._RewardText);
				
				IsTalking = true;
	
		}

		// end chat with npc or contine npc next sentnce 
		else if( !Talk.Typing)
		{
			

			Talk.SkipText =false;
			Talk.EndText();
			if(PlayerHasItem)
			{
				QuestGenerate.QuestCompletedCheck(questVillager);
			}	
		
	
			if( !Talk.TalkContinued){
				Talk.EndText();
				IsTalking = false;
			
			}
			
			if(!Talk.EndOfSentence)
			{
				Talk.TalkContinued = false;
			}

		
		}
		// skip chat 
		else 
		{
			Talk.SkipText = true;
		}

		


	
	}


	void OnTriggerEnter2D (Collider2D wallObject)
	{
		//if (wallObject.transform.tag == "Wall") 
		//{
		Debug.Log ("npc" + wallObject);
		Vector2 dir = centerPointMap - (Vector2)this.transform.position ;
		Vector2 targetPosition = (Vector2)this.transform.position + new Vector2(Mathf.Round(dir.normalized.x) ,Mathf.Round(dir.normalized.y)); 
		Debug.Log ((int)dir.normalized.x);
		this.gameObject.transform.position =targetPosition;
		//}
	}





}

