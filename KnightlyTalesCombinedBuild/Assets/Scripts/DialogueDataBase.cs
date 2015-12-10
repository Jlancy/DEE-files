using UnityEngine;
using System.Collections;
using System.Collections.Generic;


	public class DialogueDataBase : MonoBehaviour {

		public List<Dialogue> dialogue = new List<Dialogue>();


		void Awake()
		{
			//dialogue.Add(new Dialogue("bring me",Dialogue.Person.King,Dialogue.SentencePlacement.BeforeRequest,0));
			//dialogue.Add(new Dialogue("in return I will give you",Dialogue.Person.King , Dialogue.SentencePlacement.BeforeReward,1));

			//// king dialogue 
			/// dialogue index 0-6
			dialogue.Add (new Dialogue("Sir Knight, the kingdom faces peril from all sides. " +
										"I ask for your aid in dealing with these threats. Can you help us?",
			                           Dialogue.Person.King,Dialogue.SentencePlacement.DefaultPharase,0));
			dialogue.Add(new Dialogue("We welcome your assistance with this issue.",
			                          Dialogue.Person.King,Dialogue.SentencePlacement.DefaultPharase,1));
			dialogue.Add(new Dialogue("The princess has been kidnapped by a local band of brigands. " +
									  "Please free her and bring her back to the safety of the castle. " +
			                          "There is a small village near them that may offer you some assistance.",
			                          Dialogue.Person.King,Dialogue.SentencePlacement.DefaultPharase,2));
			dialogue.Add(new Dialogue("Knights from one of our neighboring empires have taken control " +
										"of one of our border towns and have begun to threaten other " +
										"parts of our sovereign territory. Drive our enemies back to " +
										"the realm where they belong.",
			                          	Dialogue.Person.King,Dialogue.SentencePlacement.DefaultPharase,3));
			dialogue.Add(new Dialogue("A local Dragon has become a danger to the populace and needs to be put down. " +
										"Track the dragon to its lair and dispatch the fell beast. " +
										"Return with the dragon’s tail as proof of the deed.",
			                            Dialogue.Person.King,Dialogue.SentencePlacement.DefaultPharase,4));
			dialogue.Add(new Dialogue("Our scholars have heard word of a lost artifact that poses " +
										"interest for the kingdom. Tales of the nearby ruins suggest " +
										"a powerful stone idol hides in their depths. Make haste to the " +
										"ruins and secure the item for our empire.",
			                          	Dialogue.Person.King,Dialogue.SentencePlacement.DefaultPharase,5));
			dialogue.Add(new Dialogue("Be well Sir Knight and seen The Kings will done.",
			                          Dialogue.Person.King,Dialogue.SentencePlacement.DefaultPharase,6));


			//// dialogue use [Q0011-Q0018] item database  and the reward is a quest item
			/// dialogue index 7-11
			dialogue.Add(new Dialogue("Good Sir Knight, if I may be allowed to ask a favor of you. " +
			                          "It has been a long time since my last good meal. If you would bring me a",
			                           Dialogue.Person.QuestGiver,Dialogue.SentencePlacement.BeforeRequest,7,2));
			dialogue.Add(new Dialogue("I would gladly lend you my aid. 3 It may not be worth much but this",
			                          Dialogue.Person.QuestGiver,Dialogue.SentencePlacement.BeforeReward,8));
			dialogue.Add(new Dialogue("is yours to take for the effort.",
			                          Dialogue.Person.QuestGiver, Dialogue.SentencePlacement.AfterReward,9));
			//finish quest
			dialogue.Add(new Dialogue("Thank you for the meal my Lord. Please take this",Dialogue.Person.QuestGiver,
			                          Dialogue.SentencePlacement.BeforeGivingReward,10));
			dialogue.Add(new Dialogue("as a token of my gratitude. May it help you on your journey.",
			                          Dialogue.Person.QuestGiver,Dialogue.SentencePlacement.AfterGivingReward,11));

			//// dialogue use [Q0041-Q0044] item database  and the reward is a quest item 
			/// /// dialogue index 12-16
			dialogue.Add(new Dialogue("Your timing is near perfect my lord. " +
										"As a simple farmer I require all my tools to do my work properly. My",
			                          Dialogue.Person.QuestGiver,Dialogue.SentencePlacement.BeforeRequest,12,5));
			dialogue.Add(new Dialogue("has gone missing. I would be happy to give you this",
			                          Dialogue.Person.QuestGiver,Dialogue.SentencePlacement.BeforeReward,13));
			dialogue.Add(new Dialogue("if you could find my missing tools.",
			                          Dialogue.Person.QuestGiver,Dialogue.SentencePlacement.AfterReward,14));
			//finish Quest
			dialogue.Add(new Dialogue("Thank you for going through the trouble of finding my tools Sir Knight. Here is the",
			                           Dialogue.Person.QuestGiver,Dialogue.SentencePlacement.BeforeGivingReward,15));
			dialogue.Add(new Dialogue("as promised. Hopefully you can make use of it during you travels.",
			                          Dialogue.Person.QuestGiver, Dialogue.SentencePlacement.AfterGivingReward,16));

			//// dialogue use [Q0061-Q0065] item database  and the reward is a quest item 
			/// dialogue index 17 -21
			dialogue.Add(new Dialogue("Good Sir Knight, would you be willing to help a struggling parent. " +
										"My young one was playing with their",
			                          Dialogue.Person.QuestGiver, Dialogue.SentencePlacement.BeforeRequest,17,7));
			dialogue.Add(new Dialogue("when a passing animal distracted them and it was lost. I would be happy to trade this",
			                          Dialogue.Person.QuestGiver,Dialogue.SentencePlacement.BeforeReward,18));
			dialogue.Add(new Dialogue("for its safe return.",
			                          Dialogue.Person.QuestGiver,Dialogue.SentencePlacement.AfterReward,19));
			dialogue.Add(new Dialogue("Thank you for bringing this back to me. My little one will be" +
										"overjoyed at its return. Please take this",
			                          Dialogue.Person.QuestGiver,Dialogue.SentencePlacement.BeforeGivingReward,20));
			dialogue.Add(new Dialogue("in return for all you have done for us.",
			                          Dialogue.Person.QuestGiver,Dialogue.SentencePlacement.AfterGivingReward,21));

			/// dialogue use [Q0081-Q0084] item database  and the reward is a  [Q0031-Q0034] (key) 
			/// /// dialogue index 22-26
			dialogue.Add(new Dialogue("Good day Sir Knight. I understand you need past some locked doors nearby. " +
										"I happen to have a ",
			                          Dialogue.Person.QuestGiver,Dialogue.SentencePlacement.BeforeRequest,22,9));
			dialogue.Add(new Dialogue("hidden away for safe keeping. It’s yours for a",
			                          Dialogue.Person.QuestGiver,Dialogue.SentencePlacement.BeforeReward,23));
			dialogue.Add(new Dialogue(". I look forward to doing business with you.",
			                          Dialogue.Person.QuestGiver,Dialogue.SentencePlacement.AfterReward,24));
			dialogue.Add(new Dialogue("This is a nice",
			                          Dialogue.Person.QuestGiver,Dialogue.SentencePlacement.BeforeGivingReward,25));
			dialogue.Add(new Dialogue(". It should fetch a nice price on the open market. " +
										"The key’s all yours. You may want to wash it first though, " +
										"all things considered.",
			                          	Dialogue.Person.QuestGiver,Dialogue.SentencePlacement.AfterGivingReward,26));

			dialogue.Add(new Dialogue("this temp for the sake of testing stuff and what not making this rather l" +
										"to to test what happens when a sentence is longer than the 3 lines" +
										"no idea what will happpend but will need a next buttou or tap or something to " +
										"continue the conversation  this ",Dialogue.Person.Princess
			                          ,Dialogue.SentencePlacement.DefaultPharase,27));

			dialogue.Add(new Dialogue("this temp for the sake of testing stuff and what not making this rather l" +
			                          "to to test what happens when a sentence is longer than the 3 lines" +
			                          "no idea what will happpend but will need a next buttou or tap or something to " +
			                          "continue the conversation  this should proably be long enogh",Dialogue.Person.Knight
			                          ,Dialogue.SentencePlacement.DefaultPharase,28));
		}


	}

