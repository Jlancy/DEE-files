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
		public Quest questVillager;
		string tempInstructions;

		int i = 0 ;


		
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
		Talk = FindObjectOfType<TextTyper>();
		//find The chatBubble
			// need to create one if it doesnt exit

			/*
			int t = 0;
			foreach (Text child in TextContainer.transform)
			{
				Debug.Log(child);
				ChatLine2[t] = child;
				t++;
			}*/

			//ChatLine   = GameObject.FindGameObjectsWithTag("ChatText");

			//LineLength = ChatLine[0].GetComponent<Text>().text.Length;
		
			//ItemList = GameObject.FindGameObjectWithTag ("Item Database").GetComponent<ItemDatabase> ();
			//disableChatBubble();
			TestString = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
				"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
				"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
				"AAAAAAAAAAAAAAAAAAbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb" +
				"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";



		}
		
		// Update is called once per frame

		void Update()
		{
			//Debug.Log(Talk.Typing);
//			Debug.Log(ChatLine[1].GetComponent<Text>().text.Length);
			//questVillager = QuestGenerate.SubQuest[i];
			
		
			if(IsTalking )
			{


				if (!Talk.Typing)
				{

				Debug.Log("derp");
					if(Input.GetMouseButtonDown(0))
					{

						if(PlayerHasItem)
						{
							QuestGenerate.QuestCompletedCheck(questVillager);
						}	
						
						Talk.EndText();
						IsTalking = false;
						

					}


				}


				else if(Input.GetMouseButtonDown(0))
				{
					Talk.SkipText = true;
				}

				
			}
			//QuestGene.QuestCompletedCheck(questVillager);
			//tempQuest = new Quest(ItemList.items[11],1000,Quest.QuestType.main);
			//tempInstructions  = DialogueTemp.QuestDialogue(DialogueList.dialogue[0],tempQuest._RequiredItem,DialogueList.dialogue[1],tempQuest._Gold) ;
			//Debug.Log(DialogueList.dialogue[0]._IndexValue);
			//tempQuest =QuestGene.MakeQuest(ItemList.items[11],1000,Quest.QuestType.main);

		//	Debug.Log(tempQuest._QuestCompleted);


		}

		public void QuestTalk()
		{
			//tempQuest._QuestCompleted = true;
			//ImportanPersonTalking();
			//PortriatCase(DialogueTemp.DialogueEvent[i]._Person);
			if(!IsTalking)
			{	
				if(!PlayerHasItem)
				Talk.StartText(questVillager._QuestInstructions);

				else
				Talk.StartText(questVillager._RewardText);
			}
			

			IsTalking = true;
			//StartCoroutine(JustInCase());



			//StartCoroutine(TypeText(questVillager._QuestInstructions));
			//StartCoroutine(TypeText(questVillager._QuestInstructions));
			//Talk.StartText(questVillager._QuestInstructions);
		}
		//void OnTriggerExit2D(Collider2D other)
		//{
		//	Talk.EndText();
			//StopCoroutine(TypeText(DefaultPharse));
		

			//i++;


		//}

		




	




	}

