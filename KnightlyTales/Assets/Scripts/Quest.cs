using UnityEngine;
using System.Collections;

namespace KnightlyTales{
	[System.Serializable]
	public class Quest {

		// in progress
		public string _QuestInstructions; 
		public string _RewardText;
		public Item _RequiredItem;
		public Item _RewardItem;
		public int _Gold;
		public QuestType _QuestType;
		public enum QuestType
		{
			Item_Gold,
			Item_Reward,
			Objective_Gold,
			Objective_Reward
		}
		public bool _QuestCompleted;
		public GameObject _QuestObjective;
		public bool _ActiveQuest;



		public Quest(string QuestInstructions,string RewardText, Item RequiredItem, int Gold , QuestType type, bool ActiveQuest)
		{
			_QuestInstructions 	= QuestInstructions;
			_RewardText			= RewardText;
			_RequiredItem      	= RequiredItem;
			_Gold              	= Gold;
			_QuestType         	= type;
			_QuestCompleted	   	= false;
			_ActiveQuest 		= ActiveQuest;

		}

		public Quest(string QuestInstructions,string RewardText, Item RequiredItem, Item RewardItem, QuestType type, bool ActiveQuest)
		{
			_QuestInstructions = QuestInstructions;
			_RewardText			= RewardText;
			_RequiredItem      = RequiredItem;
			_RewardItem        = RewardItem; 
			_QuestType         = type;
			_QuestCompleted	   = false;
			_ActiveQuest 		= ActiveQuest;
		}


		public Quest(string QuestInstructions,string RewardText, GameObject QuestObjective, Item RewardItem, QuestType type, bool ActiveQuest)
		{
			_QuestInstructions = QuestInstructions;
			_RewardText			= RewardText;
			_QuestObjective    = QuestObjective;
			_RewardItem        = RewardItem; 
			_QuestType         = type;
			_QuestCompleted	   = false;
			_ActiveQuest 		= ActiveQuest;
		}

		public Quest(string QuestInstructions,string RewardText, GameObject QuestObjective, int Gold, QuestType type, bool ActiveQuest)
		{
			_QuestInstructions = QuestInstructions;
			_RewardText			= RewardText;
			_QuestObjective    = QuestObjective;
			_Gold     		   = Gold; 
			_QuestType         = type;
			_QuestCompleted	   = false;
			_ActiveQuest 		= ActiveQuest;
		}
	}
}
