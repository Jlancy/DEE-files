using UnityEngine;
using System.Collections;

public class TriggerAddItem : MonoBehaviour {

	// Use this for initialization
	Inventory  inventory;
	QuestManager questManger;
	int itemID;
	SpriteRenderer spriteRenderer ;

	void Start () {
		spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
		inventory = FindObjectOfType<Inventory>();
		questManger = FindObjectOfType<QuestManager>();
	    //temp = new Fairy("test",12,"nope",0,10,true,Item.ItemType.Fairy,11,4,10,60,Fairy.FairyType.AttackBoost);
		//Debug.Log(temp.itemName);
		//temp.ActiveFairy = true;
		// StartCoroutine( CountDown(temp.Cooldown));
	}
	void Update()
	{

		if(!questManger.GenerateQuest)
		{
			itemID = questManger.SubQuest[0]._RequiredItem.itemID; 
			spriteRenderer.sprite = questManger.SubQuest[0]._RequiredItem.itemIcon;
		}
	}
	
	void OnTriggerEnter2D(Collider2D player)
	{Debug.Log(itemID);
		if(player.transform.tag == "Player")
		{
			Debug.Log(itemID);
		inventory.AddItem(itemID);
		Destroy(this.gameObject);
		}
	}


}
