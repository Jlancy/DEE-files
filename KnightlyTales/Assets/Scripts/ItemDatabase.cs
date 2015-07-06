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
			items.Add (new Item ("attack", 0, "foodie", 30, 5, 1, true, Item.ItemType.Ammo)); // ID of 0 must be ammo.
			items.Add (new Item ("attack", 1, "Food", 15, 2, 1, true, Item.ItemType.Potion));
			items.Add (new Item ("Defense", 2, "Soda", 20, 2, 1, false, Item.ItemType.QuestItem));
			items.Add (new Item ("Map", 3, "food3", 22, 2, 1, false, Item.ItemType.QuestItem));
			items.Add (new Item ("Sword", 4, "food4", 23, 2, 1, true, Item.ItemType.Potion));
			items.Add (new Item ("Sword", 5, "food5", 23, 2, 1, true, Item.ItemType.Potion));
			items.Add (new Item ("Map", 6, "food6", 24, 2, 1, false, Item.ItemType.QuestItem));
		}
	}
}