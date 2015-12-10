using UnityEngine;

using System.Collections;

public class Fairy : Item {

	public int StatModifier;
	public bool ActiveFairy ;
	public bool CooldownActive ;
	public int Duration;
	public int Cooldown;
	public FairyType fairyType;
	public int CoolDownIndex;
	public enum FairyType
	{
		Heal,
		AttackBoost,
		DefenseBoost,
		GearProtection
	}

	public Fairy(string name, int id, string description, int health, int value, bool stackable, ItemType type,Sprite ItemIcon,int groupNumber,
	     	 	int statModifier, int duration, int cooldown, FairyType typeF,int coolDownIndex )
	{

		itemName = name;
		itemID = id -1;
		itemDescription = description;
		itemIcon = ItemIcon;
		itemHealth = health;
		itemValue = value;
		itemStackable = stackable;
		itemType = type;
		itemGroupNumber = groupNumber;

		StatModifier = statModifier;
		Duration = duration;
		Cooldown = cooldown;
		fairyType = typeF;
		ActiveFairy = false;
		CooldownActive = false;
		CoolDownIndex = coolDownIndex;
	}


}

