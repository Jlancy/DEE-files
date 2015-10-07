using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;


	public class Inventory : MonoBehaviour
	{

		public GameObject slots, toolTip, draggedItemGameObject ,Page;
		public List<GameObject> Slots = new List<GameObject> ();
		public List<Item> Items = new List<Item> ();
		public bool draggingItem = false;
		public Item draggedItem;
		public int draggingIndex;
		public int dragOn;
		public int ItemPerPage = 30;
		public int PageCount = 3;// max 4 or else ui would probally need to change
		private int slotX, slotY, x, y;
		private ItemDatabase database;
		private ItemUser user;
		private Button[] PageButton;
		private GameObject PageObject;
		private Event temp;
		private int SlotNumberStart;
		public int SlotNumber;
		private GameObject StorageSwap;
		public int StorageSlot = 3;
		SlotManger slotmanger;	

		public void showToolTip (Vector3 toolPosition, Item item)
		{
			toolTip.SetActive (true);
			//toolTip.GetComponent<RectTransform> ().localPosition = 
			//new Vector3 (toolPosition.x - 20, toolPosition.y - 255, toolPosition.z + 50);
			toolTip.transform.GetChild(0).GetComponent<Text> ().text = toolTipText (item);
		}
	
		//Creates the text that shows up in the tooltip.
		public string toolTipText (Item item)
		{
			Debug.Log("String"+item.itemName);
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
			//toolTip.SetActive (false);
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
			draggedItem = null;
			draggingItem = false;
			draggedItemGameObject.SetActive (false);
		}


		// Use this for initialization
		void someFunction()
		{

			Debug.Log("derp");

		}
		void Start ()
		{	slotmanger = FindObjectOfType<SlotManger>();
			SlotNumberStart = 0;
			PageButton = new Button[PageCount]; 
		
			PageObject = GameObject.FindGameObjectWithTag("PageContainer");
			StorageSwap = GameObject.FindGameObjectWithTag("StorageSwap");

			for( int p=0; p < PageCount; p++)
			{
				GameObject page = Instantiate(Page) as GameObject;
				page.transform.parent = PageObject.gameObject.transform;
				PageButton[p] = page.GetComponent<Button>(); 
				page.name = p.ToString();
				page.GetComponentInChildren<Text>().text = p.ToString();
				page.GetComponent<PageButton>().PageNumberStart = SlotNumberStart;
				page.transform.localScale = new Vector3(1,1,1);
				SlotNumberStart+=ItemPerPage;
				//PageButton[p].onClick.AddListener( delegate{someFunction();});
				// mod to have functions for page change
				//second mod make second script to attach to button

			}


			for( int c = 0 ; c < (PageCount * ItemPerPage) +StorageSlot; c++)
			{
				Items.Add (new Item ());
			}
			//x = -82;//-50;
			//y =	102;// 240;
			//slotX = 3;
			//slotY = 10;

			database = GameObject.FindGameObjectWithTag ("Item Database").GetComponent<ItemDatabase> ();
			//user = GameObject.FindGameObjectWithTag ("Item User").GetComponent<ItemUser> ();
			//for (int i = 0; i < slotY; i++) {
			for (int i = 0; i < ItemPerPage ; i++) {
				GameObject slot = (GameObject)Instantiate(slots);
				slot.GetComponent<SlotScript> ().slotNumber = i;
				Slots.Add (slot);
				
				//slot.transform.parent = this.gameObject.transform;
				slot.transform.SetParent(this.gameObject.transform);
				slot.transform.localScale = new Vector2(1,1);
				slot.name = "Slot " + (i);
				//slot.GetComponent<RectTransform> ().localPosition = new Vector3 (x + k * 80, y - i * 80, 0);
				
			}
			for (int s = 0; s< StorageSlot; s++)
			{
				GameObject slot = (GameObject)Instantiate(slots);
				slot.GetComponent<SlotScript> ().slotNumber = s+((PageCount * ItemPerPage) -StorageSlot);
				slot.GetComponent<SlotScript> ().StorageSlot =true;
				Slots.Add (slot);
				
				//slot.transform.parent = this.gameObject.transform;
				slot.transform.SetParent(StorageSwap.gameObject.transform);
				slot.transform.localScale = new Vector2(1,1);
				slot.name = "Slot " + (s+((PageCount * ItemPerPage) -StorageSlot));
			}
			//}
			//if (GameManager.instance.level != 1) {
			//	GameManager.instance.loadInventory ();
			//}
			Items [0] = database.items [1];
			Items [30] = database.items [0];
			//for (int t =0; t < 90; t++)
			//{
				 // database.items [1];
			//}
			//AddItem(1);
			Items [61] = database.items [0];
			//AddItem(1);
			//for(int i = 11 ; i <19 ; i++)
			//{
			//	AddItem(i);
			//}
		}


		// Update is called once per frame
		void Update ()
		{
			if (draggingItem) {
				//needs fix. dragged icon too far right.
				Vector3 position = (Input.mousePosition);
				draggedItemGameObject.GetComponent<RectTransform>().localPosition = new Vector3 (position.x , position.y  ,0); 
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
			if (ID > 0) { //Makes sure ID passed into the method is correct; ID=0 is for Ammo, has its own method in playerAttackOrDefend
				for (int s = 0; s < Items.Count; s++) {			//loops through inventory array
					if (Items [s].itemName == null) {			//finds the first empty slot
						for (int l = 0; l < database.items.Count; l++) {		//loops through inventory database
							if (database.items [l].itemID.Equals (ID)) {		//finds correct item based on ID
								if (database.items [l].itemStackable) {			//checks if item is stackable
									if (!addIfItemIsInInventory (ID)) {			//if stackable, checks if item is in inventory
										Items [s] = database.items [l]; 		//adds new item if stackable item is not in inventory yet
									}
									break;						//breaks loop for inventory database
								} else {
									Items [s] = database.items [l]; 			//adds new item if item is not in the inventory
								}
							}
						}
						break;									//breaks loop for inventory (Items[])
					}
				}
			}
			slotmanger.updateCheck =true;
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
	public bool CheckItem( Item item)
	{
		return Items.Contains(item);
	}

	}

