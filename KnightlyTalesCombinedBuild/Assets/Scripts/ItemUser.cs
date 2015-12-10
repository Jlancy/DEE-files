using UnityEngine;
using System.Collections;


public class ItemUser : MonoBehaviour
{
	private Player player;
	private Inventory inventory;
	private EquipmentList equipList;
	SlotManger slotmanger;

	public bool StopFairy =false;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
		equipList = FindObjectOfType<EquipmentList>();
		slotmanger = FindObjectOfType<SlotManger>();
	}
	public void UseItem (Item usedItem, int slot)
	{
		switch (usedItem.itemType) {
		case Item.ItemType.Potion:
			//UsePotion (usedItem.itemID, usedItem.itemHealth); 
		player.GainHealth(usedItem.itemHealth);
			inventory.removeItem (usedItem.itemID, slot);
			break;

		case Item.ItemType.QuestItem:
			GiveQuestItem (usedItem.itemID);

			break;

		case Item.ItemType.Fairy:
			FairyCase((Fairy)usedItem , slot);

			break;
		default:
			break;
		}

	}



	private void FairyCase(Fairy UseFairy ,int slot)
	{

		switch(UseFairy.fairyType)
		{
		case Fairy.FairyType.Heal:
			//Debug.Log(UseFairy.ActiveFairy);

				if (!UseFairy.ActiveFairy	&& !UseFairy.CooldownActive )
				{
					
					inventory.removeItem (UseFairy.itemID, slot);
					inventory.CountDownTimer(0,UseFairy);
					UseFairy.ActiveFairy = true;
					StartCoroutine(HealFairy(UseFairy));
				}
				else 
				slotmanger.ReturnItemToLastSlot(slotmanger.originSlot);

			break;
		case Fairy.FairyType.GearProtection:
			if (!UseFairy.ActiveFairy	&& !UseFairy.CooldownActive )
			{
					inventory.removeItem (UseFairy.itemID, slot);
					inventory.CountDownTimer(1,UseFairy);
					UseFairy.ActiveFairy = true;

			}
			else 
				slotmanger.ReturnItemToLastSlot(slotmanger.originSlot);
			break;
		case Fairy.FairyType.DefenseBoost:
			if (!UseFairy.ActiveFairy	&& !UseFairy.CooldownActive )
			{
					inventory.removeItem (UseFairy.itemID, slot);
					inventory.CountDownTimer(2,UseFairy);
					UseFairy.ActiveFairy = true;

			}
			else 
				slotmanger.ReturnItemToLastSlot(slotmanger.originSlot);
			break;
		case Fairy.FairyType.AttackBoost:
			if (!UseFairy.ActiveFairy	&& !UseFairy.CooldownActive )
			{
					inventory.removeItem (UseFairy.itemID, slot);
					inventory.CountDownTimer(3,UseFairy);
					UseFairy.ActiveFairy = true;
					StartCoroutine(AttackFairy(UseFairy));

			}
			else 
				slotmanger.ReturnItemToLastSlot(slotmanger.originSlot);
			break;
		
		}

	}

	IEnumerator HealFairy(Fairy fairy)
	{
		Debug.Log("enterCo");
		while(fairy.ActiveFairy)
		{
			if(StopFairy)
				break;


			player.GainHealth(fairy.itemHealth);
			yield return new WaitForSeconds(1);
		}
	}

	IEnumerator GuardFairy(Fairy fairy)
	{
		Debug.Log("enterCo");
		while(fairy.ActiveFairy)
		{
			if(StopFairy)
				break;
			
			
			player.GainHealth(fairy.itemHealth);
			yield return new WaitForSeconds(1);
		}
	}

	IEnumerator AttackFairy(Fairy fairy)
	{
		Debug.Log("enterCo");

		player.SwordAttack+= fairy.StatModifier;
		player.BowAttack+= fairy.StatModifier;
		yield return new WaitForSeconds(fairy.Duration);
		player.SwordAttack -= fairy.StatModifier;
		player.BowAttack -= fairy.StatModifier;
	


	}

	IEnumerator DefenseFairy(Fairy fairy)
	{

		player.MaxHealth += fairy.StatModifier;
		yield return new WaitForSeconds(fairy.Duration);
		player.MaxHealth -= fairy.StatModifier;
		
	}

	private void GiveQuestItem (int ID)
	{
		switch (ID) {
		case 2:
			break;
		
		default:
			break;
		}
	}

}

