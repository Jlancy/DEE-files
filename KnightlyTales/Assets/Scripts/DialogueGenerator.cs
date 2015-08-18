using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace KnightlyTales
{
	public class DialogueGenerator : MonoBehaviour {

		public DialogueDataBase dialogueDataBase;
		public List<Dialogue> DialogueEvent = new List<Dialogue>();
		string Output;
		public List<int> QuestDialogueStartIndex = new List<int>();
		public List<int> ItemGroups  = new List<int>();


		// Use this for initialization
		void Start () {
			DialogueEvent.Add(dialogueDataBase.dialogue[27]);
			DialogueEvent.Add(dialogueDataBase.dialogue[28]);
			DialogueEvent.Add(dialogueDataBase.dialogue[27]);
			DialogueEvent.Add(dialogueDataBase.dialogue[28]);
			FindIndexOfQuestStart();
		}
		
		// Update is called once per frame
		void Update () {

		}

		// the basic dialogue for a quest giver 
		//old way
		/*public string QuestDialogueInstructions(Dialogue BeforeRequest,Item RequestedItem, Dialogue BeforeReward, Item ItemReward,
		                            Dialogue AfterReward)
		{ 

			Output = BeforeRequest._Phrase+" "+ RequestedItem.itemName+" "+ BeforeReward._Phrase +" "+ ItemReward+
						" "+ AfterReward._Phrase;
			return Output;
		}
		public string QuestDialougeGiveReward(Dialogue BefroeGivingReward, Item RewardItem, Dialogue AfterGivingReward)
		{
			Output = BefroeGivingReward._Phrase +" "+ RewardItem.itemName +" "+ AfterGivingReward._Phrase;
			return Output;
		}*/

		/*string for the quest instructions
		 *the index is the index of the start of a sentence from the dailogur database
		 *since each are grouped in of 5.
		 *the 1 and 2 are used to mark that this part will be colored for keyitems
		 */  
		public string QuestDialogueInstructions(int index,Item RequestedItem, Item RewardItem)
		{
			Output = dialogueDataBase.dialogue[index]._Phrase+" "+"1"+ RequestedItem.itemName+"2"+" "+ dialogueDataBase.dialogue[index+1]._Phrase
				+" "+"1"+RewardItem.itemName+"2"+" "+ dialogueDataBase.dialogue[index+2]._Phrase;
			return Output;

		}
		//same as the above sting 
		public string QuestDialougeGiveReward( int index, Item RewardItem)
		{
			Output = dialogueDataBase.dialogue[index+3]._Phrase+" "+"1"+RewardItem.itemName+"2"+" "+dialogueDataBase.dialogue[index+4]._Phrase;
			return Output;
		}


		// making a list of index values with the SentencePlacement type BeforeRequest
		public void FindIndexOfQuestStart()
		{
			for(int i = 0 ; i <dialogueDataBase.dialogue.Count; i++ )
			{
				if(dialogueDataBase.dialogue[i]._SentencePlacement == Dialogue.SentencePlacement.BeforeRequest)
				{
					QuestDialogueStartIndex.Add(dialogueDataBase.dialogue[i]._IndexValue);
					ItemGroups.Add(dialogueDataBase.dialogue[i]._ItemGroupNumber);
				}
			}
		}
	}
}
