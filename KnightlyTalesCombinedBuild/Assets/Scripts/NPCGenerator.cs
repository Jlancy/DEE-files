using UnityEngine;
using System.Collections;

public class NPCGenerator : MonoBehaviour {
	QuestManager questManger;
	int AmmountOfNPC;
	bool CreatedNPC = false;
	public GameObject[] NPC_List;
	public GameObject npcPrefab;
	private Sprite[] villagerSprite;
	// Use this for initialization
	void Start () {
		villagerSprite = Resources.LoadAll<Sprite>("Villager");
		questManger = FindObjectOfType<QuestManager>();
		AmmountOfNPC = questManger.AmmountOfQuests;
		NPC_List = new GameObject[AmmountOfNPC];
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!questManger.GenerateQuest)
		{
			if(!CreatedNPC)
			{
				for(int i= 0 ; i < AmmountOfNPC; i++)
				{	
					//Random.seed = Random.Range (-100, 100);
					int VillagerId = Random.Range (0, villagerSprite.Length);
					GameObject temp = Instantiate(npcPrefab);
					temp.GetComponent<SpriteRenderer> ().sprite = villagerSprite [VillagerId];
					temp.GetComponent<NPC>().questVillager = questManger.SubQuest[i];
					temp.transform.position =new Vector2(10,10 + (i * 4));
					NPC_List[i] = temp; 
					temp = null;
				}
				CreatedNPC =true;
			}
		}
		//Debug.Log(NPC_List[0].GetComponent<NPC>().questVillager._RequiredItem.itemName+ "1");
		//Debug.Log(NPC_List[1].GetComponent<NPC>().questVillager._RequiredItem.itemName+ "2");
	}
}
