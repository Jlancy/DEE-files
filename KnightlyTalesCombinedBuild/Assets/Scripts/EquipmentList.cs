using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EquipmentList : MonoBehaviour {


	public Sword sword ;
	public Armor armor;
	public Bow bow;
	public Quiver quiver;
	[HideInInspector]
	public int SwordUpgradeIndex = 0;
	[HideInInspector]
	public int ArmorUpgradeIndex = 0;
	[HideInInspector]
	public int BowUpgradeIndex = 0;
	[HideInInspector]
	public int QuiverUpgradeIndex = 0;

	public List<Sword> SwordUpgrades = new List<Sword>();
	public List<Armor> ArmorUpgrades = new List<Armor>();
	public List<Bow> BowUpgrades = new List<Bow>();
	public List<Quiver> QuiverUpgrades = new List<Quiver>(); 

	public bool WeaponProtection;

	private Player player;


	void Start()
	{
		WeaponProtection = false;
		player = FindObjectOfType<Player>();
		//GearDurablityDown();
	}

	public void UpgradeSword(int UpgradeIndex)
	{
		SwordUpgradeIndex = UpgradeIndex;
		sword = SwordUpgrades[SwordUpgradeIndex];
		player.SwordAttack = sword.Attack;
	}

	public void UpgradeArmor(int UpgradeIndex)
	{
		ArmorUpgradeIndex = UpgradeIndex;
		armor = ArmorUpgrades[ArmorUpgradeIndex];
		player.health = armor.HP;
	}

	public void UpgradeBow(int UpgradeIndex)
	{
		BowUpgradeIndex = UpgradeIndex;
		bow = BowUpgrades[BowUpgradeIndex];
		player.BowAttack = bow.Attack;
	}
	public void UpgradeQuiver( int UpgradeIndex)
	{
		QuiverUpgradeIndex = UpgradeIndex;
		quiver = QuiverUpgrades[QuiverUpgradeIndex];
		player.QuiverSize = quiver.QuiverSize;
	}

	public Sword CurrentSword()
	{
		return sword;
	}
	public Armor CurrentArmor()
	{
		return armor;
	}
	public Bow CurrentBow()
	{
		return bow;
	}
	public Quiver CurrentQuiver()
	{
		return quiver;
	}

	void GearDurablityDown()
	{
		if(!WeaponProtection)
		{
			sword.Durablity	-=1;
			bow.Durablity		-=1;
			quiver.Durablity 	-=1;
			armor.Durablity	-=1;
		}
	}

	void GearDurablityBreak(int DurablityDown, Equipment equipment)
	{
		equipment.Durablity -= DurablityDown;
	}
}

