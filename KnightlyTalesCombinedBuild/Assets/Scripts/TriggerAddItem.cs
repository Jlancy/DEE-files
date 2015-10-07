using UnityEngine;
using System.Collections;

public class TriggerAddItem : MonoBehaviour {

	// Use this for initialization
	Inventory  inventory;
	QuestManager questManger;
	int itemID;

	void Start () {
		inventory = FindObjectOfType<Inventory>();
		questManger = FindObjectOfType<QuestManager>();

	}
	void Update()
	{
		if(!questManger.GenerateQuest)
		{
			itemID = questManger.SubQuest[0]._RequiredItem.itemID; 
		}
	}
	
	void OnTriggerEnter2D(Collider2D player)
	{
		if(player.transform.tag == "Player")
		{
		inventory.AddItem(itemID);
		Destroy(this.gameObject);
		}
	}
}
