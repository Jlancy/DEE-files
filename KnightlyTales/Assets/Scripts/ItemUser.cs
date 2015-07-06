using UnityEngine;
using System.Collections;

namespace KnightlyTales
{
	public class ItemUser : MonoBehaviour
	{
		private Player player;
		private Inventory inventory;

		void Start () {
			player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
			inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();

		}
		public void UseItem (Item usedItem, int slot)
		{
			switch (usedItem.itemType) {
			case Item.ItemType.Potion:
				UsePotion (usedItem.itemID, usedItem.itemHealth); 
				break;

			case Item.ItemType.QuestItem:
				GiveQuestItem (usedItem.itemID);
				break;

			default:
				break;
			}
			inventory.removeItem (usedItem.itemID, slot);
		}
	
		private void UsePotion (int ID, int gain)
		{
			switch (ID) {
			case 1:
				player.GainHealth(gain);
				break;
			
			default:
				print ("not nice");
				break;
			}
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
}
