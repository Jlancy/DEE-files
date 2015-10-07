using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class SlotScript : MonoBehaviour, IPointerDownHandler,IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler
	{

		public Item item;
		[HideInInspector]
		public Image itemImage;
		[HideInInspector]
		public Text itemAmount;
		public int slotNumber;     // total number of slots
		private int InventoryIndex;
		Inventory inventory;	   // contains the picked up items 
		ItemUser user;			   // 
		SlotManger _slotManger;    // manages  the slots 
		public bool StorageSlot =false ;
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
			if(StorageSlot)
			{
			//	Debug.Log(slotNumber);
			}
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
		void SetIndex()
		{

			if(StorageSlot)
			{
				InventoryIndex =slotNumber;

			}
			else
			{
				InventoryIndex = slotNumber+_slotManger.SlotNumberMod;
			}

		Debug.Log(InventoryIndex);
		}

		public void OnPointerDown (PointerEventData data)
		{
			//Debug.Log("derp");
			if (inventory.draggingItem) {
				SetIndex();

				inventory.Items [inventory.draggingIndex] = inventory.Items [InventoryIndex];
				inventory.Items [InventoryIndex] = inventory.draggedItem;
				inventory.closeDraggedItem ();
				_slotManger.updateCheck= true;
				//inventory.Items[slotNumber+1].itemName = "wellTHen";	

			}



		}

		public void OnPointerUp(PointerEventData data)
		{
		int SlotToolTipIndex;
		if(StorageSlot)
		{
			SlotToolTipIndex = slotNumber-61;
			
		}
		else
		{
			SlotToolTipIndex = slotNumber;
		}
		//Debug.LogError("click"+slotNumber);
		//Debug.LogError(inventory.Items[InventoryIndex]);

			if (inventory.Items [InventoryIndex].itemName != null && !inventory.draggingItem) {
				
			inventory.showToolTip (inventory.Slots[SlotToolTipIndex].GetComponent<RectTransform>().localPosition, inventory.Items[InventoryIndex]);
				
			}
		}


		

		public void OnPointerEnter (PointerEventData data)
		{
			SetIndex();

			_slotManger.lastSlotNumber = InventoryIndex;
			_slotManger.overSlot = true;
			//_slotManger.outSideInvenotry = false;
	
			inventory.dragOn = slotNumber;

			

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
			
			if (inventory.Items[InventoryIndex].itemName != null) {
			SetIndex();
				_slotManger.originSlot = InventoryIndex;
				inventory.draggingIndex = InventoryIndex;
				Debug.Log(InventoryIndex);
				inventory.showDraggedItem (inventory.Items [InventoryIndex]);
				inventory.Items[InventoryIndex] = new Item ();
				_slotManger.updateCheck = true;
				itemAmount.enabled = false;


			}




		}

		public void OnEndDrag (PointerEventData data)
		{

			
			if(_slotManger.overSlot)
			{
				Debug.Log("in");
				_slotManger.ReturnItemToLastSlot(_slotManger.lastSlotNumber);
			}
			else if(!_slotManger.outSideInvenotry)
			{
				Debug.Log("out");
				_slotManger.ReturnItemToLastSlot(_slotManger.originSlot);
			}
			else
			{//use item
				Vector2 ray =  Camera.main.ScreenPointToRay(Input.mousePosition).origin ;
				RaycastHit2D hit =Physics2D.Raycast(ray,Vector3.forward,20,_slotManger.layer);
				

				if(hit.transform != null) 
				{
					if(hit.transform.tag == "Player"  )
					{
					Debug.Log(hit);
						user.UseItem(inventory.draggedItem, inventory.draggingIndex);

					}
					else

					{
						_slotManger.ReturnItemToLastSlot(_slotManger.originSlot);
					}
				}	//inventory.closeDraggedItem ();
				else
				{
					_slotManger.ReturnItemToLastSlot(_slotManger.originSlot);
				}
			}
			inventory.closeDraggedItem ();
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
			//if (inventory.draggingItem) {
			//	inventory.closeDraggedItem ();
			//	inventory.Items [inventory.draggingIndex] = inventory.Items [inventory.dragOn];
			//	inventory.Items [inventory.dragOn] = inventory.draggedItem;
			//}


		}

	}

