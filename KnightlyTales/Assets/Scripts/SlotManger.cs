using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

// attached to SlideOutINV
namespace KnightlyTales
{
	public class SlotManger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
		[HideInInspector]
		public int lastSlotNumber; 				// last slot number hovered over
		public int originSlot;					// Slot where the item was picke up
		public bool overSlot;
		[HideInInspector]
		public bool outSideInvenotry;
		Inventory inventory;  					// contains the list of items picked up
		ItemUser user;		  					// allow items to be used to affect the player
		SlotScript[] slotList;  				// list of the slots
		public bool updateCheck = false;		// check if the inventory needs updating 


		// Use this for initialization
		void Start () {
			inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
			user = GameObject.FindGameObjectWithTag ("Item User").GetComponent<ItemUser> ();

		}
		
		// Update is called once per frame
		void Update () {
			// get list of the slots
			if (slotList == null)
			{
				slotList = FindObjectsOfType<SlotScript>();
				System.Array.Reverse(slotList); // to fix the slotList from being inverted
			}
			//run when an item has been added
			if(updateCheck)
			{
				UpdateInventory();
			}

		
		}


		public void UpdateInventory()
		{
			
			//cycle through the inventory to update it
			for(int i =0 ; i < inventory.Items.Count; i++)
			{
				slotList[i].item = inventory.Items[i]; 							// place the inventory item in the slot
				slotList[i].itemImage.enabled = true;							// enabel the image component
				slotList[i].itemImage.sprite = inventory.Items[i].itemIcon;		// render image in the item slot 
				// check if the item is stackable 
				if(inventory.Items[i].itemStackable)
				{
					slotList[i].itemAmount.enabled = true;  							// enabel the text componet for the amount
					slotList[i].itemAmount.text = "" + inventory.Items [i].itemValue;	// put the amount collected in text to display
				}
				// to make sure when swapping a draged item with an item in a slot 
				// that item amount stays
				else
				{
					slotList[i].itemAmount.enabled = false;  							// enabel the text componet for the amount
					slotList[i].itemAmount.text = "";
				}

			}
			updateCheck = false;



		}
		public void ReturnItemToLastSlot(int slotNumber)
		{
			inventory.closeDraggedItem ();
			inventory.Items [inventory.draggingIndex] = inventory.Items [slotNumber];
			inventory.Items [slotNumber] = inventory.draggedItem;
			updateCheck = true;
		}

		//*****
		//to check if the player has let go of the item with in the inventory and return it 
		// to its originSlot or to use it if outside of inventory
		public void OnPointerEnter (PointerEventData data)
		{
			//Debug.Log("outsied");
			outSideInvenotry = false;
		}

		public void OnPointerExit (PointerEventData data)
		{
			//Debug.Log("inside");
			outSideInvenotry = true;

		}
		

		//
		//****
	}
}
