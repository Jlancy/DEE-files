using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace KnightlyTales
{
	public class Inventory : MonoBehaviour
	{

		public GameObject slots, toolTip, draggedItemGameObject;
		public List<GameObject> Slots = new List<GameObject> ();
		public List<Item> Items = new List<Item> ();
		public bool draggingItem = false;
		public Item draggedItem;
		public int draggingIndex;
		private int slotX, slotY, x, y;
		private ItemDatabase database;
		private ItemUser user;

		public void showToolTip (Vector3 toolPosition, Item item)
		{
			toolTip.SetActive (true);
			toolTip.GetComponent<RectTransform> ().localPosition = 
			new Vector3 (toolPosition.x - 20, toolPosition.y - 255, toolPosition.z + 50);
			toolTip.transform.GetChild (0).GetComponent<Text> ().text = toolTipText (item);
		}
	
		//Creates the text that shows up in the tooltip.
		public string toolTipText (Item item)
		{
			string toolTipDescription = "";
			//name
			toolTipDescription =
			"<color=#4DA4BF>" 
				+ item.itemName 
				+ "</color>";
			//stats
			toolTipDescription += "<color=#ffffff>"; 
			if (item.itemHealth > 0) {
				toolTipDescription += "\n Health: " + item.itemHealth;
			}
			if (item.itemSpeed > 0) {
				toolTipDescription += "\n Speed: " + item.itemSpeed;
			}
			toolTipDescription += "\n Type: " + item.itemType;
			toolTipDescription += "</color>";
			//description
			toolTipDescription +=
				"<color=#ffffff>" 
				+ "\n \n" + item.itemDescription
				+ "</color>";
			return toolTipDescription;
		}
	
		public void closeToolTip ()
		{
			toolTip.SetActive (false);
		}

		public void showDraggedItem (Item item)
		{
			closeToolTip ();
			draggedItemGameObject.SetActive (true);
			draggingItem = true;
			draggedItem = item;
			draggedItemGameObject.GetComponent<Image> ().sprite = item.itemIcon;
		}

		public void closeDraggedItem ()
		{
			draggingItem = false;
			draggedItemGameObject.SetActive (false);
		}


		// Use this for initialization
		void Start ()
		{
			x = -50;
			y = 240;
			slotX = 3;
			slotY = 10;
			int slotIndex = 0;
			database = GameObject.FindGameObjectWithTag ("Item Database").GetComponent<ItemDatabase> ();
			user = GameObject.FindGameObjectWithTag ("Item User").GetComponent<ItemUser> ();
			for (int i = 0; i < slotY; i++) {
				for (int k = 0; k < slotX; k++) {
					GameObject slot = (GameObject)Instantiate (slots);
					slot.GetComponent<SlotScript> ().slotNumber = slotIndex;
					Slots.Add (slot);
					Items.Add (new Item ());
					slot.transform.parent = this.gameObject.transform;
					slot.name = "Slot " + (slotIndex);
					slot.GetComponent<RectTransform> ().localPosition = new Vector3 (x + k * 50, y - i * 50, 0);
					slotIndex++;
				}
			}
			if (GameManager.instance.level != 1) {
				GameManager.instance.loadInventory ();
			}
		}


		// Update is called once per frame
		void Update ()
		{
			if (draggingItem) {
				//needs fix. dragged icon too far right.
				Vector3 position = (Input.mousePosition);
				draggedItemGameObject.GetComponent<RectTransform> ().localPosition = new Vector3 (position.x - 585, position.y - 150, position.z); 
			}
		}

		//Checks if a stackable item is in the inventory already, if so, adds a count to it.
		public bool addIfItemIsInInventory (int ID)
		{
			for (int x= 0; x< Items.Count; x++) {
				if (Items [x].itemID == ID) {
					Items [x].itemValue ++;
					return true; 
				}
			}
			return false;
		}

		//Adds the passed item to the inventory.
		public void AddItem (int ID)
		{
			if (ID > 0) {
				for (int s = 0; s < Items.Count; s++) {
					if (Items [s].itemName == null) {
						for (int l = 0; l < database.items.Count; l++) {
							if (database.items [l].itemID.Equals (ID)) {
								if (database.items [l].itemStackable) {
									if (!addIfItemIsInInventory (ID)) {
										Items [s] = database.items [l]; 
									}
									break;
								} else {
									Items [s] = database.items [l]; 
								}
							}
						}
						break;
					}
				}
			}
		}

		public void removeItem (int ID, int slot)
		{
			if (ID > 0) {
				if (Items [slot].itemStackable && Items [slot].itemValue > 1) {
					Items [slot].itemValue--;
				} else {
					Items [slot] = new Item ();
				}
			}
		}


	}
}
