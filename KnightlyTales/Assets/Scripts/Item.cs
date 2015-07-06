using UnityEngine;
using System.Collections;

namespace KnightlyTales
{
	[System.Serializable]
	public class Item
	{
		public string itemName;
		public int itemID;
		public string itemDescription;
		public Sprite itemIcon;
		public int itemHealth;
		public int itemSpeed;
		public int itemValue;
		public bool itemStackable;
		public ItemType itemType;
		public enum ItemType
		{
			Ammo,
			Potion,
			QuestItem
		}

		public Item (string name, int id, string description, int health, int speed, int value, bool stackable, ItemType type)
		{
			itemName = name;
			itemID = id;
			itemDescription = description;
			itemIcon = Resources.Load<Sprite> ("Sprites/" + name);
			itemHealth = health;
			itemSpeed = speed;
			itemValue = value;
			itemStackable = stackable;
			itemType = type;

		}

		public Item ()
		{
			itemID = -1;
			itemStackable = false;
		}
	}
}