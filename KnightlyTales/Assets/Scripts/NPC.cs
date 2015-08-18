using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace KnightlyTales{
	public class NPC : MonoBehaviour {
		public string DefaultPharse;
		string TestString;


		public ItemDatabase ItemList;
		public DialogueDataBase DialogueList;
		public DialogueGenerator DialogueTemp;
		public QuestManager QuestGene;
		Quest questVillager;
		string tempInstructions;

		int i = 0 ;


		public Inventory inv;
		public TextTyper Talk;

		Image Portrait;
		int t = 0;
		bool IsTalking = false;
		// Use this for initialization
		void Start () {
	
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
			questVillager = QuestGene.SubQuest[i];

		
			if(IsTalking )
			{
				if(DialogueTemp.DialogueEvent[i]._Person != Dialogue.Person.QuestGiver)
				{
					Talk.PortriatCase(DialogueTemp.DialogueEvent[i]._Person);
				}

				if (!Talk.Typing)
				{


						if(Input.GetMouseButtonDown(0))
						{

								
							if(i+1 < DialogueTemp.DialogueEvent.Count)
							{
								Talk.SkipText =false;
								i++;
								Talk.StartText(DialogueTemp.DialogueEvent[i]._Phrase);
							}
							else
							{
								i = 0;
								Talk.EndText();
								IsTalking = false;
							}

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
		void OnTriggerEnter2D(Collider2D other)
		{
			//tempQuest._QuestCompleted = true;
			//ImportanPersonTalking();
			//PortriatCase(DialogueTemp.DialogueEvent[i]._Person);
			Talk.StartText(DialogueTemp.DialogueEvent[i]._Phrase);
			IsTalking = true;
			//StartCoroutine(JustInCase());



			//StartCoroutine(TypeText(questVillager._QuestInstructions));
			//StartCoroutine(TypeText(questVillager._QuestInstructions));
			//Talk.StartText(questVillager._QuestInstructions);
		}
		void OnTriggerExit2D(Collider2D other)
		{
			Talk.EndText();
			//StopCoroutine(TypeText(DefaultPharse));
		

			i++;


		}

		void ImportanPersonTalking()
		{





				
				
		}




		IEnumerator JustInCase()
		{


			while(IsTalking)
			{
				if (Talk.Typing)
				{
					IsTalking = Talk.Typing;
				}
			}

			yield return new WaitForSeconds(25);
			IsTalking = true;

		}




	}
}
