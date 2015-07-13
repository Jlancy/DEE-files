using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace KnightlyTales
{
	public class SlotScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler
	{

		public Item item;
		[HideInInspector]
		public Image itemImage;
		[HideInInspector]
		public Text itemAmount;
		public int slotNumber;     // total number of slots
		Inventory inventory;	   // contains the picked up items 
		ItemUser user;			   // 
		SlotManger _slotManger;    // manages  the slots 
		//static bool outsideInventory = false; // change later add to a main script

		// Use this for initialization
		void Start ()
		{
			itemAmount = gameObject.transform.GetChild (1).GetComponent<Text> ();
			inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
			user = GameObject.FindGameObjectWithTag ("Item User").GetComponent<ItemUser> ();
			itemImage = gameObject.transform.GetChild (0).GetComponent<Image> ();
			_slotManger = FindObjectOfType<SlotManger>();
			itemImage.enabled = false;
			itemAmount.text = "";
		}
	
		// Update is called once per frame
		void Update ()
		{
			/*
			if (inventory.Items[slotNumber].itemName != null) {
				item = inventory.Items[slotNumber];
				itemImage.enabled = true;
				itemImage.sprite = inventory.Items [slotNumber].itemIcon;
				if (inventory.Items[slotNumber].itemStackable) {
					itemAmount.enabled = true;
					itemAmount.text = "" + inventory.Items [slotNumber].itemValue;
				} else {
					itemAmount.enabled = false;
				}
			} else {
				itemImage.enabled = false;
				itemAmount.text = "";
			}*/
			//Debug.Log(inventory.draggedItem);
		}

		public void OnPointerDown (PointerEventData data)
		{
			if (inventory.draggingItem) {
				inventory.closeDraggedItem ();
				inventory.Items [inventory.draggingIndex] = inventory.Items [slotNumber];
				inventory.Items [slotNumber] = inventory.draggedItem;
				_slotManger.updateCheck= true;
				//inventory.Items[slotNumber+1].itemName = "wellTHen";	

			}



		}

		public void OnPointerUp (PointerEventData data)
		{

		}

		public void OnPointerEnter (PointerEventData data)
		{
			_slotManger.lastSlotNumber = slotNumber;
			_slotManger.overSlot = true;
			//_slotManger.outSideInvenotry = false;

			if (inventory.Items [slotNumber].itemName != null && !inventory.draggingItem) {
				inventory.showToolTip (inventory.Slots [slotNumber].GetComponent<RectTransform> ().localPosition, inventory.Items [slotNumber]);

			}

			/*
			else{
			inventory.draggingIndex = slotNumber;
			outsideInventory = false;
			}
			*/

		}

		public void OnPointerExit (PointerEventData data)
		{
			inventory.closeToolTip ();
			_slotManger.overSlot = false;
			//_slotManger.outSideInvenotry = true;
			//outsideInventory = true;
		}

		public void OnDrag (PointerEventData data)
		{		

			if (inventory.Items[slotNumber].itemName != null) {
				_slotManger.originSlot = slotNumber;
				inventory.draggingIndex = slotNumber;
				inventory.showDraggedItem (inventory.Items [slotNumber]);
				inventory.Items[slotNumber] = new Item ();
				_slotManger.updateCheck = true;
				itemAmount.enabled = false;


			}




		}

		public void OnEndDrag (PointerEventData data)
		{
			Debug.Log("what2");
			if(_slotManger.overSlot)
			{
			_slotManger.ReturnItemToLastSlot(_slotManger.lastSlotNumber);
			}
			else if(!_slotManger.outSideInvenotry)
			{
				_slotManger.ReturnItemToLastSlot(_slotManger.originSlot);
			}
			else
			{//use item
				user.UseItem(inventory.Items[1], 1);
				Debug.Log("inv"+inventory.Items[1]+"index"+inventory.draggingIndex);

			}
			//else 
			//	Debug.Log("outside");

			//Debug.Log("maybe");
			//if (inventory.draggingItem && inventory.Items[slotNumber].itemName == null ) {
			//	inventory.closeDraggedItem ();
			//	inventory.Items [inventory.draggingIndex] = inventory.Items [slotNumber];
			//	inventory.Items [slotNumber] = inventory.draggedItem;
				//Debug.Log("returnItem");
				//_slotManger.updateCheck = true;
				//user.UseItem (inventory.Items [slotNumber], slotNumber);
			//}
			//else 
			//{

			//	Debug.Log("useItem");
			//	user.UseItem(inventory.draggedItem, inventory.draggingIndex);
			//}
		}

	}
}
