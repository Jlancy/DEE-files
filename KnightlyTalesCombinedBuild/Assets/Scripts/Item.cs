using UnityEngine;
using System.Collections;


	[System.Serializable]
	public class Item
	{
		public string itemName;
		public int itemID;
		public string itemDescription;
		public Sprite itemIcon;
		public int itemHealth;
		public int itemValue;
		public bool itemStackable;
		public ItemType itemType;
		public enum ItemType
		{
			Potion,
			QuestItem
		}
		public int itemGroupNumber;

		public Item (string name, int id, string description, int health, int value, bool stackable, ItemType type,int groupNumber )
		{
			itemName = name;
			itemID = id;
			itemDescription = description;
			itemIcon = Resources.Load<Sprite> ("Sprites/" + name);
			itemHealth = health;
			itemValue = value;
			itemStackable = stackable;
			itemType = type;
			itemGroupNumber = groupNumber;

		}

		public Item ()
		{
			itemID = -1;
			itemStackable = false;
		}
	}
