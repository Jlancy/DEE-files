using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class QuestManager : MonoBehaviour {

	public List<Quest> SubQuest =  new List<Quest>();
	public List<Item> QuestItems = new List<Item>();
	int QuestItemAmount =2;
	public Quest Output;
	public DialogueGenerator genrator;   // find these in script, instead of manualy linking
	public ItemDatabase itemDatabase;
	public Inventory inventory;	//mess with this scirpt  and others to auto add missing componet so they dont whine
	public int AmmountOfQuests =2;
	//public int QuestNumber;

	bool QuestAdded;
	int count;
	int CurrentItemAmmount;
	
	[HideInInspector]
	public bool MakeItems = true;
	[HideInInspector]
	public bool GenerateQuest = true;



	// Use this for initialization
	void Start () {
	inventory =FindObjectOfType<Inventory>();
	 
	}
	
	// Update is called once per frame
	void Update () {
		// testing


		if(MakeItems)
		{
		QuestItemAmount = AmmountOfQuests +1;
			// this count is temporay till we get more item groups.
			// it make there are a mostly even number of items to avoid issues  
			count = QuestItemAmount / (genrator.ItemGroups.Count- 1);
			if( count >=1)
			count = 2;
			//Debug.Log(MakeItems);
			// function to generate the quest items
			QuestItemGenerator();
			//KeyAmmount(1);
			MakeItems = false;
			CurrentItemAmmount= 0;
		
		}

		if(GenerateQuest)
		{
			
			// make quest list
			QuestList(QuestItems.Count);


			//GenerateQuest =false;

	
		}
		
		// temp testing invetory checking 
		//inventory.Items.Add(SubQuest[0]._RequiredItem);
						
	}

	// make  a Quest
	private Quest MakeQuest( string Instructions,string RewardDialogue,Item RequiredItem, Item RewardItem , Quest.QuestType type,bool ActiveQuest)
	{
		// the instructions for the quest
		// store the info for the quest
		Output = new Quest(Instructions ,RewardDialogue , RequiredItem,RewardItem,type,ActiveQuest);
		return Output;
	}
	// generate a lsit of sub quest to complete 
	private void QuestList(int HowManyQuests)
	{
		// make the the first item in quest item the required item
		// then remove it to use the rest for reward;
		Item currentQuestItem = QuestItems[0];
		QuestItems.RemoveAt(0);

		// loop for the ammount of quest
		for(int i = 0; i < HowManyQuests; i++)
		{

		//Debug.Log(currentQuestItem.itemName);
			// loop  to match the item to the right dialogue 
			for(int d = 0 ; d < genrator.QuestDialogueStartIndex.Count; d++)
			{

				// check watch dialogue matches the current item 
				if(currentQuestItem.itemGroupNumber == genrator.dialogueDataBase.dialogue[genrator.QuestDialogueStartIndex[d]]._ItemGroupNumber)
				{
				
					//loop through the questItems to check for an item that is not the same itme group
					for (int t = 0; t < QuestItems.Count; t++)
					{
						// make sure it not the sam item group
						if(currentQuestItem.itemGroupNumber != QuestItems[t].itemGroupNumber)
						{
							// quest instructions
							string temp = genrator.QuestDialogueInstructions(genrator.QuestDialogueStartIndex[d],currentQuestItem, 
							                                                 QuestItems[t]);
							// quest reward dialogue
							string temp2 = genrator.QuestDialougeGiveReward(genrator.QuestDialogueStartIndex[d],QuestItems[t] );
							//the quest
							Quest tempQuest = MakeQuest(temp,temp2,currentQuestItem, QuestItems[t],Quest.QuestType.Item_Reward,
							                            false);
							// make the reward item the next required item for the next quest
							currentQuestItem= QuestItems[t];
							//remove it from the list
							QuestItems.RemoveAt(t);
							// add to sub quest list
							SubQuest.Add(tempQuest);
							// break out of the loop checking to make sure that the item groups are
							// not the same
							break;
						
						}
		

					}
					// break out of the loop checking 
					break;
				}
			}
		}
		//KeyQuest();
		StartCoroutine(StartKeyGen());

	}
	// generates quest item list
	private void QuestItemGenerator()
	{
		// loop for the amout of items you want to generate for thequest

		for(int i = 0; i < QuestItemAmount; i++)
		{
			// should reconsider name
			// call itemGroupAdd
			ItemGroupAdd();
	
		}
		
	}
	//adjust

	void KeyQuest()
	{	KeyAmmount(1);

		int LastQuestIndex = SubQuest.Count -1;
		int LastIndex = genrator.QuestDialogueStartIndex.Count -1;

		Item tempItem = SubQuest[LastQuestIndex]._RequiredItem;

		// quest instructions
		string temp = genrator.QuestDialogueInstructions(genrator.QuestDialogueStartIndex[LastIndex],QuestItems[0], 
		                                                 tempItem);
		//Debug.Log(temp);
		// quest reward dialogue
		string temp2 = genrator.QuestDialougeGiveReward(genrator.QuestDialogueStartIndex[LastIndex],tempItem );
		//the quest
		Quest tempQuest = MakeQuest(temp,temp2,tempItem, QuestItems[0],Quest.QuestType.Item_Reward,
		                            false);
		SubQuest[LastQuestIndex] = tempQuest;
		//Debug.Log(QuestItems[0].itemName);
		QuestItems.RemoveAt(0);
	}

	IEnumerator StartKeyGen()
	{
		KeyQuest();

		GenerateQuest =false;
		yield return new WaitForSeconds (0.2f);

	}

	// random number is selected from the available itemGroup number
	// then it is passed into ItemGroupCase
	void ItemGroupAdd()
	{
		//int groupIndex = Random.Range(0,genrator.ItemGroups.Count);
		// randomly slecting the case number to add it from that group
		int groupNumber = 0;



		// cycle throught for count times through available itemGroupNumber
		for(int i = 0 ; i < count; i++)
		{
			groupNumber = genrator.ItemGroups[i];
			//Debug.Log(groupNumber);

			ItemGroupCase(groupNumber);
		
		}
	}
	// gonna change this up so it use last reward item from the Subques list
	void KeyAmmount(int key)
	{
		// MAKE SURE ITS THE RIGHT GROUP NUMBER FOR KEYS
		int keyGroupNumber = 9;
		for(int i = 0; i<key;i++)
			ItemGroupCase(keyGroupNumber);
	}

	// the super case that will hold the index range of each itemgroup
	void ItemGroupCase(int groupNumber)
	{
		//MAKE SURE THE INDEX RANGE IS CORRECT FOR EACH GROUP
		switch(groupNumber)
		{
		case 0:
			
			break;
		case 1:
			
			break;
		case 2:
			SearchForItem(11 , 18);
			break;
		case 3:
			
			break;
		case 4:
			
			break;
		case 5:
			SearchForItem(41 , 44);
			break;
		case 6:
			
			break;
		case 7:
			SearchForItem(61 , 64);
			break;
		case 8:
			
			break;
		case 9:
			SearchForItem(81,84);
			break;
		case 10:
			break;
			
			
		}
	}
	/*using the start and end index it will randomly select 
	 *it aslo makes sure to have duplicate items
	 *possible issue may arise since there is a while loop to make sure it adds an item
	 *it may get caught in infinite loop due to using every item in that item 
	 *but since we are going to add more items shouldnt
	 *happedn but maybe
	 */

	void SearchForItem(int StartIndex, int EndIndex)
	{
		bool itemAdded = false;
		// loop to make sure an item 
		while(!itemAdded)
		{
			// random item index plus one to include endIndex into range
			int tempIndex = Random.Range(StartIndex,EndIndex+1);
			bool dupe = false;
			// check if the item is already in the quest item
			for(int q = 0;q < QuestItems.Count; q++)
			{
				if(QuestItems[q].itemID == tempIndex)
				{

					dupe = true;
					break;
				}


			}
			// if item isnt already in quest item add it
			if(!dupe)
			{
				// temp till full database
				// since the database inst full looping through to find matching index
				for(int i = 0; i < itemDatabase.items.Count; i++ )
				{
					if(itemDatabase.items[i].itemID == tempIndex)
					{
						//Debug.Log("enter");
						CurrentItemAmmount++;

						// to stop random bug that would give 1 extra quest radomly
						// note new radom bug umm gets stuck an infinite loop if 
						// it doest have this check
						// make to only add if it hasnt reach it limit
						if(CurrentItemAmmount <= QuestItemAmount)
						QuestItems.Add(itemDatabase.items[i]);
						
							itemAdded = true;
					}
					
				}
			}

		}


	}


	// quest completion check	
	public void QuestCompletedCheck(Quest CurrentQuest)
	{
		// case check for the different types of quest
		switch(CurrentQuest._QuestType)
		{
		case Quest.QuestType.Item_Reward :
			//if (CurrentQuest._RequiredItem == CurrentQuest._RequiredItem)
			//{
			Debug.Log(inventory.Items.Contains(CurrentQuest._RequiredItem));
			if(inventory.Items.Contains(CurrentQuest._RequiredItem))
			{
				CurrentQuest._QuestCompleted = true;
				//inventory
				inventory.Items.Remove(CurrentQuest._RequiredItem);
				inventory.AddItem(CurrentQuest._RewardItem.itemID);
				//Debug.Log(
				// notify player
				Debug.Log("yay");
			}
			else
			{
				//Debug.Log("dam");
			}
				// wait to give reward till the player talks to npc 
				
			//}
			break;
		case Quest.QuestType.Item_Gold:
			break;

		case Quest.QuestType.Objective_Gold:
			break;

		case Quest.QuestType.Objective_Reward:
			break;


		}
	

	}

	// clear inventory of quest type items
	void ClearInventoryWhenMainQuest()
	{
		int  invSize = inventory.Items.Count;

		for(int i = 0; i < invSize; i++)
		{
			if(inventory.Items[i].itemType == Item.ItemType.QuestItem)
			{
				inventory.Items.RemoveAt(i);
			}
		}
	}
}
