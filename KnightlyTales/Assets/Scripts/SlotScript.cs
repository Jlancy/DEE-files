using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace KnightlyTales
{
	public class SlotScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler
	{

		public Item item;
		Image itemImage;
		Text itemAmount;
		public int slotNumber;
		Inventory inventory;
		ItemUser user;

		// Use this for initialization
		void Start ()
		{
			itemAmount = gameObject.transform.GetChild (1).GetComponent<Text> ();
			inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
			user = GameObject.FindGameObjectWithTag ("Item User").GetComponent<ItemUser> ();
			itemImage = gameObject.transform.GetChild (0).GetComponent<Image> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
			if (inventory.Items [slotNumber].itemName != null) {
				item = inventory.Items [slotNumber];
				itemImage.enabled = true;
				itemImage.sprite = inventory.Items [slotNumber].itemIcon;
				if (inventory.Items [slotNumber].itemStackable) {
					itemAmount.enabled = true;
					itemAmount.text = "" + inventory.Items [slotNumber].itemValue;
				} else {
					itemAmount.enabled = false;
				}
			} else {
				itemImage.enabled = false;
				itemAmount.text = "";
			}
			//Debug.Log(inventory.draggedItem);
		}

		public void OnPointerDown (PointerEventData data){}

		public void OnPointerUp (PointerEventData data)
		{
			if (!inventory.draggingItem && inventory.Items [slotNumber].itemName != null) {
				user.UseItem (inventory.Items [slotNumber], slotNumber);
			}
		}

		public void OnPointerEnter (PointerEventData data)
		{
			inventory.dragOn = slotNumber;
			if (inventory.Items [slotNumber].itemName != null && !inventory.draggingItem) {
				inventory.showToolTip (inventory.Slots [slotNumber].GetComponent<RectTransform> ().localPosition, inventory.Items [slotNumber]);
			}
		}

		public void OnPointerExit (PointerEventData data)
		{
			inventory.closeToolTip ();
		}

		public void OnDrag (PointerEventData data)
		{
			if (inventory.Items [slotNumber].itemName != null) {
				inventory.draggingIndex = slotNumber;
				inventory.showDraggedItem (inventory.Items [slotNumber]);
				inventory.Items [slotNumber] = new Item ();
				itemAmount.enabled = false;
			}
		}

		public void OnEndDrag (PointerEventData data)
		{
			if (inventory.draggingItem) {
				inventory.closeDraggedItem ();
				inventory.Items [inventory.draggingIndex] = inventory.Items [inventory.dragOn];
				inventory.Items [inventory.dragOn] = inventory.draggedItem;
			}

		}
	}
}
