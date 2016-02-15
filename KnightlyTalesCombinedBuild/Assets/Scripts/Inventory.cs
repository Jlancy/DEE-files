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

	public int[] FairyUseTimer = new int[4];
	public int[] FairyCoolDownTimer = new int[4];
	private bool DisplayTime= false;
	private int DisplayIndex;
	private bool DisplayCoolDown;
	private bool DisplayUseTimer;
	private Fairy CurrentToolTipFairy;

	public void showToolTip (Vector3 toolPosition, Item item)
	{
		toolTip.SetActive (true);
		//toolTip.GetComponent<RectTransform> ().localPosition = 
		//new Vector3 (toolPosition.x - 20, toolPosition.y - 255, toolPosition.z + 50);
		toolTip.transform.GetChild(0).GetComponent<Text> ().text = toolTipText (item);
		if(item.itemType == Item.ItemType.Fairy)
		{
			DisplayTime = true;
			CurrentToolTipFairy = (Fairy)item;
			DisplayCoolDown = CurrentToolTipFairy.CooldownActive;
			DisplayUseTimer = CurrentToolTipFairy.ActiveFairy;
			FairyCase(CurrentToolTipFairy);
			if(!DisplayUseTimer)
				FairyUseTimer[DisplayIndex] = CurrentToolTipFairy.Duration;
				
			if(!DisplayCoolDown)
				FairyCoolDownTimer[CurrentToolTipFairy.CoolDownIndex] = CurrentToolTipFairy.Cooldown;
		}
		else
		{
			DisplayCoolDown = false;
			DisplayUseTimer = false;
			DisplayTime =false;
		}
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
		//Items [1] = new Fairy("test",12,"nope",5,10,true,Item.ItemType.Fairy,11,0,10,10,Fairy.FairyType.Heal,0);
		//Items [2] = new Fairy("test",12,"nope",0,10,true,Item.ItemType.Fairy,11,4,10,10,Fairy.FairyType.AttackBoost,3);
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
		//StartCoroutine(CountDown(20));
	}


	// Update is called once per frame
	void Update ()
	{
		if (draggingItem) {
			//needs fix. dragged icon too far right.
			Vector3 position = Camera.main.ScreenPointToRay(Input.mousePosition).origin;

			Debug.Log("DraggedPos:"+position+ " transform:"+draggedItemGameObject.transform.position );
			//draggedItemGameObject.GetComponent<RectTransform>().localPosition = new Vector3 (position.x , position.y  ,0); 
			draggedItemGameObject.transform.position = new Vector3 (position.x , position.y  ,0); 
		}
		if(DisplayTime)
		{	

			string TimerSting = "Duration:" +FairyUseTimer[DisplayIndex].ToString()+"\n"+
								"CoolDown:"+ FairyCoolDownTimer[CurrentToolTipFairy.CoolDownIndex].ToString();
			toolTip.transform.GetChild(1).GetComponent<Text>().text =TimerSting;

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
			Debug.Log(draggedItem.itemName);
			Debug.Log(draggedItem.itemValue);
			if (draggedItem.itemStackable && draggedItem.itemValue > 1) {
				draggedItem.itemValue--;
				slotmanger.ReturnItemToLastSlot(slotmanger.originSlot);
			} else {
				Items [slot] = new Item ();
			}
		}
	}

	public  bool CheckItem( Item item)
	{
		return Items.Contains(item);
	}




	public void CountDownTimer(int index, Fairy fairy)
	{
		StartCoroutine(CountDownUseTimer(index,fairy));
	}

	IEnumerator CountDownUseTimer(int index, Fairy fairy)
	{
		fairy.ActiveFairy = true;
		FairyUseTimer[index] = fairy.Duration;
		while(FairyUseTimer[index]  >0)
		{
			FairyUseTimer[index] -= 1;

			yield return new WaitForSeconds(1);
		}
		FairyUseTimer[index] = fairy.Duration;
		fairy.ActiveFairy = false;
		CooldownTimer( fairy);
	}

	public void CooldownTimer( Fairy fairy)
	{
		StartCoroutine(CountDownCooldownTimer( fairy));
	}
		
	IEnumerator CountDownCooldownTimer( Fairy fairy)
	{
		fairy.CooldownActive = true;
		FairyCoolDownTimer[fairy.CoolDownIndex] = fairy.Cooldown;
		while(FairyCoolDownTimer[fairy.CoolDownIndex]  >0)
		{
			FairyCoolDownTimer[fairy.CoolDownIndex] -= 1;

			yield return new WaitForSeconds(1);
		}
		FairyCoolDownTimer[fairy.CoolDownIndex] = fairy.Cooldown;
		fairy.CooldownActive = false;
	}

	private void FairyCase(Fairy DisplayFairy)
	{
		
		switch(DisplayFairy.fairyType)
		{
		case Fairy.FairyType.Heal:
			DisplayIndex = 0;
			break;
		case Fairy.FairyType.GearProtection:
			DisplayIndex = 1;
			break;
		case Fairy.FairyType.DefenseBoost:
			DisplayIndex = 2;
			break;
		case Fairy.FairyType.AttackBoost:
			DisplayIndex = 3;
			break;
			
		}
		
	}

}

