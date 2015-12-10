using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ItemDatabase : MonoBehaviour
{
	public List<Item> items = new List<Item> ();
	private SpriteLoader  IconLoader;
	void Start ()
	{		
		IconLoader = FindObjectOfType<SpriteLoader>();
		//Potion Items
		items.Add (new Item ("attack", 1, "Food", 15, 2, true, Item.ItemType.Potion,IconLoader.ItemImage(20),0));
		items.Add (new Item ("Defense", 2, "Soda", 20, 1, false, Item.ItemType.Potion,IconLoader.ItemImage(20),0));
		items.Add (new Item ("Map", 3, "food3", 22, 1, false, Item.ItemType.Potion,IconLoader.ItemImage(20),0));
		items.Add (new Item ("Sword", 4, "food4", 23, 1, true, Item.ItemType.Potion,IconLoader.ItemImage(20),0));
		items.Add (new Item ("Sword", 5, "food5", 23, 1, true, Item.ItemType.Potion,IconLoader.ItemImage(20),0));
		items.Add (new Item ("Map", 6, "food6", 24, 1, false, Item.ItemType.Potion,IconLoader.ItemImage(20),0));


		//QuestItems
		items.Add (new Item ("Bill of Sale", 10, "N/A", 0, 1, false, Item.ItemType.QuestItem,IconLoader.ItemImage(18),1));

		items.Add (new Item ("Rat Tail Stew", 11, "N/A", 0, 1, true, Item.ItemType.QuestItem,IconLoader.ItemImage(4),2));
		items.Add (new Item ("Potato Plant", 12, "N/A", 0, 1, true, Item.ItemType.QuestItem,IconLoader.ItemImage(2),2));
		items.Add (new Item ("Loaf of Bread", 13, "N/A", 0, 1, true, Item.ItemType.QuestItem,IconLoader.ItemImage(3),2));
		items.Add (new Item ("Wheel of Cheese", 14, "N/A", 0, 1, true, Item.ItemType.QuestItem,IconLoader.ItemImage(0),2));
		items.Add (new Item ("Rack of Beef", 15, "N/A", 0, 1, true, Item.ItemType.QuestItem,IconLoader.ItemImage(1),2));
		items.Add (new Item ("Flask of Water", 16, "N/A", 0, 1, true, Item.ItemType.QuestItem,IconLoader.ItemImage(9),2));
		items.Add (new Item ("Jar of Milk", 17, "N/A", 0, 1, true, Item.ItemType.QuestItem,IconLoader.ItemImage(3),2));
		items.Add (new Item ("Chicken", 18, "N/A", 0, 1, true, Item.ItemType.QuestItem,IconLoader.ItemImage(7),2));

		items.Add (new Item ("Fish", 21, "N/A", 0, 1, true, Item.ItemType.QuestItem,IconLoader.ItemImage(6),3));
		items.Add (new Item ("Fishing Pole", 22, "N/A", 0, 1, false, Item.ItemType.QuestItem,IconLoader.ItemImage(11),3));

		items.Add (new Item ("Iron Ore", 31, "N/A", 0, 1, true, Item.ItemType.QuestItem,IconLoader.ItemImage(20),4));
		items.Add (new Item ("Gold Ore", 32, "N/A", 0, 1, true, Item.ItemType.QuestItem,IconLoader.ItemImage(20),4));
		items.Add (new Item ("Ruby", 33, "N/A", 0, 1, true, Item.ItemType.QuestItem,IconLoader.ItemImage(19),4));
		items.Add (new Item ("Saphire", 34, "N/A", 0, 1, true, Item.ItemType.QuestItem,IconLoader.ItemImage(20),4));

		items.Add (new Item ("Axe", 41, "N/A", 0, 1, false, Item.ItemType.QuestItem,IconLoader.ItemImage(14),5));
		items.Add (new Item ("Mining Pick", 42, "N/A", 0, 1, false, Item.ItemType.QuestItem,IconLoader.ItemImage(12),5));
		items.Add (new Item ("Shovel", 43, "N/A", 0, 1, false, Item.ItemType.QuestItem,IconLoader.ItemImage(10),5));
		items.Add (new Item ("Hoe", 44, "N/A", 0, 1, false, Item.ItemType.QuestItem,IconLoader.ItemImage(20),5));

		items.Add (new Item ("Blacksmith Hammer", 51, "N/A", 0, 1, false, Item.ItemType.QuestItem,IconLoader.ItemImage(13),6));
		items.Add (new Item ("Broken Armor", 52, "N/A", 0, 1, false, Item.ItemType.QuestItem,IconLoader.ItemImage(20),6));
		items.Add (new Item ("Torch", 53, "N/A", 0, 1, false, Item.ItemType.QuestItem,IconLoader.ItemImage(17),6));

		items.Add (new Item ("Wooden Sword", 61, "N/A", 0, 1, false, Item.ItemType.QuestItem,IconLoader.ItemImage(16),7));
		items.Add (new Item ("Wooden Shield", 62, "N/A", 0, 1, false, Item.ItemType.QuestItem,IconLoader.ItemImage(15),7));
		items.Add (new Item ("Wooden Knight", 63, "N/A", 0, 1, false, Item.ItemType.QuestItem,IconLoader.ItemImage(20),7));
		items.Add (new Item ("Child's Doll", 64, "N/A", 0, 1, false, Item.ItemType.QuestItem,IconLoader.ItemImage(8),7));

		items.Add (new Item ("Pet Pig", 71, "N/A", 0, 1, false, Item.ItemType.QuestItem,IconLoader.ItemImage(21),8));
		items.Add (new Item ("Stone Dragon Idol", 72, "N/A", 0, 1, false, Item.ItemType.QuestItem,IconLoader.ItemImage(20),8));
		items.Add (new Item ("Dragon's Tail", 73, "N/A", 0, 1, false, Item.ItemType.QuestItem,IconLoader.ItemImage(20),8));
		items.Add (new Item ("Bandits Signet Ring", 74, "N/A", 0, 1, false, Item.ItemType.QuestItem,IconLoader.ItemImage(20),8));

		items.Add (new Item ("Bronze Key", 81, "N/A", 0, 1, false, Item.ItemType.QuestItem,IconLoader.ItemImage(20),9));
		items.Add (new Item ("Silver Key", 82, "N/A", 0, 1, false, Item.ItemType.QuestItem,IconLoader.ItemImage(20),9));
		items.Add (new Item ("Gold Key", 83, "N/A", 0, 1, false, Item.ItemType.QuestItem,IconLoader.ItemImage(20),9));
		items.Add (new Item ("Skeleton Key", 84, "N/A", 0, 1, false, Item.ItemType.QuestItem,IconLoader.ItemImage(20),9));
		
				
		items.Add (new Item ("Wagon Wheel", 91, "N/A", 0, 1, false, Item.ItemType.QuestItem,IconLoader.ItemImage(20),10));

		items.Add(new Fairy("Heal Fairy I",92,"heals 5 HP every second.",5,1,true,Item.ItemType.Fairy,IconLoader.ItemImage(20),11,0,10,10,Fairy.FairyType.Heal,0));
		items.Add(new Fairy("Gear Protection Fairy I",93,"Protects gear 3 times.",5,1,true,Item.ItemType.Fairy,IconLoader.ItemImage(20),11,3,10,10,Fairy.FairyType.GearProtection,1));
		items.Add(new Fairy("Defense Fairy I",94,"Boost HP by 3.",5,1,true,Item.ItemType.Fairy,IconLoader.ItemImage(20),11,3,10,10,Fairy.FairyType.DefenseBoost,2));
		items.Add(new Fairy("Attack Fairy I",95,"Boost Attack by 3.",5,1,true,Item.ItemType.Fairy,IconLoader.ItemImage(20),11,3,10,10,Fairy.FairyType.AttackBoost,3));
	}
}
