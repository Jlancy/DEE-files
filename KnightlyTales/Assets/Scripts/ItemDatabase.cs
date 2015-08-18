using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace KnightlyTales
{
	public class ItemDatabase : MonoBehaviour
	{
		public List<Item> items = new List<Item> ();

		void Start ()
		{		

			//Potion Items
			items.Add (new Item ("attack", 1, "Food", 15, 1, true, Item.ItemType.Potion,0));
			items.Add (new Item ("Defense", 2, "Soda", 20, 1, false, Item.ItemType.Potion,0));
			items.Add (new Item ("Map", 3, "food3", 22, 1, false, Item.ItemType.Potion,0));
			items.Add (new Item ("Sword", 4, "food4", 23, 1, true, Item.ItemType.Potion,0));
			items.Add (new Item ("Sword", 5, "food5", 23, 1, true, Item.ItemType.Potion,0));
			items.Add (new Item ("Map", 6, "food6", 24, 1, false, Item.ItemType.Potion,0));


			//QuestItems
			items.Add (new Item ("Bill of Sale", 10, "N/A", 0, 1, false, Item.ItemType.QuestItem,1));

			items.Add (new Item ("Rat Tail Stew", 11, "N/A", 0, 1, true, Item.ItemType.QuestItem,2));
			items.Add (new Item ("Potato Plant", 12, "N/A", 0, 1, true, Item.ItemType.QuestItem,2));
			items.Add (new Item ("Loaf of Bread", 13, "N/A", 0, 1, true, Item.ItemType.QuestItem,2));
			items.Add (new Item ("Wheel of Cheese", 14, "N/A", 0, 1, true, Item.ItemType.QuestItem,2));
			items.Add (new Item ("Rack of Beef", 15, "N/A", 0, 1, true, Item.ItemType.QuestItem,2));
			items.Add (new Item ("Flask of Water", 16, "N/A", 0, 1, true, Item.ItemType.QuestItem,2));
			items.Add (new Item ("Jar of Milk", 17, "N/A", 0, 1, true, Item.ItemType.QuestItem,2));
			items.Add (new Item ("Chicken", 18, "N/A", 0, 1, true, Item.ItemType.QuestItem,2));

			items.Add (new Item ("Fish", 21, "N/A", 0, 1, true, Item.ItemType.QuestItem,3));
			items.Add (new Item ("Fishing Pole", 22, "N/A", 0, 1, false, Item.ItemType.QuestItem,3));

			items.Add (new Item ("Iron Ore", 31, "N/A", 0, 1, true, Item.ItemType.QuestItem,4));
			items.Add (new Item ("Gold Ore", 32, "N/A", 0, 1, true, Item.ItemType.QuestItem,4));
			items.Add (new Item ("Ruby", 33, "N/A", 0, 1, true, Item.ItemType.QuestItem,4));
			items.Add (new Item ("Saphire", 34, "N/A", 0, 1, true, Item.ItemType.QuestItem,4));

			items.Add (new Item ("Axe", 41, "N/A", 0, 1, false, Item.ItemType.QuestItem,5));
			items.Add (new Item ("Mining Pick", 42, "N/A", 0, 1, false, Item.ItemType.QuestItem,5));
			items.Add (new Item ("Shovel", 43, "N/A", 0, 1, false, Item.ItemType.QuestItem,5));
			items.Add (new Item ("Hoe", 44, "N/A", 0, 1, false, Item.ItemType.QuestItem,5));

			items.Add (new Item ("Blacksmith Hammer", 51, "N/A", 0, 1, false, Item.ItemType.QuestItem,6));
			items.Add (new Item ("Broken Armor", 52, "N/A", 0, 1, false, Item.ItemType.QuestItem,6));
			items.Add (new Item ("Torch", 53, "N/A", 0, 1, false, Item.ItemType.QuestItem,6));

			items.Add (new Item ("Wooden Sword", 61, "N/A", 0, 1, false, Item.ItemType.QuestItem,7));
			items.Add (new Item ("Wooden Shield", 62, "N/A", 0, 1, false, Item.ItemType.QuestItem,7));
			items.Add (new Item ("Wooden Knight", 63, "N/A", 0, 1, false, Item.ItemType.QuestItem,7));
			items.Add (new Item ("Child's Doll", 64, "N/A", 0, 1, false, Item.ItemType.QuestItem,7));

			items.Add (new Item ("Pet Pig", 71, "N/A", 0, 1, false, Item.ItemType.QuestItem,8));
			items.Add (new Item ("Stone Dragon Idol", 72, "N/A", 0, 1, false, Item.ItemType.QuestItem,8));
			items.Add (new Item ("Dragon's Tail", 73, "N/A", 0, 1, false, Item.ItemType.QuestItem,8));
			items.Add (new Item ("Bandits Signet Ring", 74, "N/A", 0, 1, false, Item.ItemType.QuestItem,8));

			items.Add (new Item ("Bronze Key", 81, "N/A", 0, 1, false, Item.ItemType.QuestItem,9));
			items.Add (new Item ("Silver Key", 82, "N/A", 0, 1, false, Item.ItemType.QuestItem,9));
			items.Add (new Item ("Gold Key", 83, "N/A", 0, 1, false, Item.ItemType.QuestItem,9));
			items.Add (new Item ("Skeleton Key", 84, "N/A", 0, 1, false, Item.ItemType.QuestItem,9));
			

			items.Add (new Item ("Wagon Wheel", 91, "N/A", 0, 1, false, Item.ItemType.QuestItem,10));
		}
	}
}