using UnityEngine;
using System.Collections;

public class NPCGenerator : MonoBehaviour {
	QuestManager questManger;
	int AmmountOfNPC;
	bool CreatedNPC = false;
	public GameObject[] NPC_List;
	public GameObject npcPrefab;

	// Use this for initialization
	void Start () {
		questManger = FindObjectOfType<QuestManager>();
		AmmountOfNPC = questManger.QuestItemAmount - 1 ;
		NPC_List = new GameObject[AmmountOfNPC];
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!questManger.GenerateQuest)
		{
			if(!CreatedNPC)
			{
				for(int i= 0 ; i < AmmountOfNPC; i++)
				{	GameObject temp = Instantiate(npcPrefab);
					temp.GetComponent<NPC>().questVillager = questManger.SubQuest[i];
					NPC_List[i] = temp;

				}
				CreatedNPC =true;
			}
		}
		//Debug.Log(NPC_List[0].GetComponent<NPC>().questVillager._RequiredItem.itemName+ "1");
		//Debug.Log(NPC_List[1].GetComponent<NPC>().questVillager._RequiredItem.itemName+ "2");
	}
}
