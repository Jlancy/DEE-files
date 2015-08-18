using UnityEngine;
using System.Collections;
namespace KnightlyTales{
	[System.Serializable]
	public class Dialogue  {

		public string _Phrase; 	//what npc will say
		public int _IndexValue; //index in the list
		public Person _Person; 	// whos saying it
		public enum Person		// list of  possible people
		{
			King,
			Princess,
			Knight,
			QuestGiver,
			ShopKeeper

		}
	
		public SentencePlacement _SentencePlacement; // placement in the sentence
		public enum SentencePlacement				// list of possible sentence placements
		{
			BeforeRequest,
			BeforeReward,
			AfterReward,
			BeforeGivingReward,
			AfterGivingReward,
			DefaultPharase,
			
		}
		public int _ItemGroupNumber;
	


		public Dialogue (string Pharse, Person PersonType,SentencePlacement SentencePlacementType,int IndexValue)
		{
			_Phrase 			= Pharse;
			_Person 			= PersonType;
			_SentencePlacement 	= SentencePlacementType;
			_IndexValue     	= IndexValue;
		}
		public Dialogue (string Pharse, Person PersonType,SentencePlacement SentencePlacementType,int IndexValue
		                 ,int ItemGroupNumber)
		{
			_Phrase 			= Pharse;
			_Person 			= PersonType;
			_SentencePlacement 	= SentencePlacementType;
			_IndexValue     	= IndexValue;
			_ItemGroupNumber 	= ItemGroupNumber;

		}


	}
}
