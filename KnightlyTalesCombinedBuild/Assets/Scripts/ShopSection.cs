using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ShopSection : MonoBehaviour {

	private EquipmentList equipList;
	public GearType GearUpgrade;
	public GameObject ShopPanel;
	public int Gold;
	//int i = 1;

	// Use this for initialization
	void Start () {
		equipList = FindObjectOfType<EquipmentList>();
		if(ShopPanel ==null)
			Debug.LogError("Missing ShopPanel Prefab");

		else
		CreateBuyPanel();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpgradeGear( GearType gear, int UpgradeIndex, int GoldCost)
	{
		switch(gear.Gear)
		{
		case GearType.Type.Sword:
			if( Gold >= GoldCost)
			{
				equipList.UpgradeSword(UpgradeIndex);
				Gold -= GoldCost;
			}
			else
				NotEnoughGold();

			break;

		case GearType.Type.Armor:
			if( Gold >= GoldCost)
			{
				equipList.UpgradeArmor(UpgradeIndex);
				Gold -= GoldCost;
			}
			else 
				NotEnoughGold();
			break;
		case GearType.Type.Quiver:
			if( Gold >= GoldCost)
			{
				equipList.UpgradeQuiver(UpgradeIndex);
				Gold -= GoldCost;
			}
			else
				NotEnoughGold();
			break;
		case GearType.Type.Bow:
			if( Gold >= GoldCost)
			{
				equipList.UpgradeBow(UpgradeIndex);
				Gold -= GoldCost;
			}
			else 
				NotEnoughGold();
			break;


		}
	}

	void CreateBuyPanel()
	{
		List<int> tempList= new List<int>(); 
		switch(GearUpgrade.Gear)
		{

		case GearType.Type.Sword:
			for(int s = 1; s < equipList.SwordUpgrades.Count;s ++)
			{
				tempList.Add(equipList.SwordUpgrades[s].CalculateSale());
				//Debug.Log(equipList.SwordUpgrades[s].CalculateSale());
			}
			AttachShopPanel(equipList.SwordUpgrades.Count ,tempList);
			break;

		case GearType.Type.Armor:
			for(int s = 1; s < equipList.SwordUpgrades.Count;s ++)
			{
				tempList.Add(equipList.ArmorUpgrades[s].Cost);
			}
			AttachShopPanel(equipList.ArmorUpgrades.Count,tempList );
			break;
		case GearType.Type.Bow:
			for(int s = 1; s < equipList.SwordUpgrades.Count;s ++)
			{
				tempList.Add(equipList.BowUpgrades[s].Cost);
			}
			AttachShopPanel(equipList.BowUpgrades.Count,tempList);
			break;
		case GearType.Type.Quiver:
			for(int s = 1; s < equipList.SwordUpgrades.Count;s ++)
			{
				tempList.Add(equipList.QuiverUpgrades[s].Cost);
			}
			AttachShopPanel(equipList.QuiverUpgrades.Count,tempList);
			break;
		}
		tempList.Clear();


	}

	void AttachShopPanel( int PanelCount ,  List<int> Cost)
	{


		for(int i = 1 ; i< PanelCount; i++)
		{
			GameObject panel;
			panel = Instantiate(ShopPanel);
			panel.transform.SetParent (this.transform);
			panel.GetComponent<ShopPanel>().Gear = GearUpgrade;
			panel.GetComponent<ShopPanel>().UpgradeIndex = i;
			panel.GetComponent<ShopPanel>().shopSection = this;
			panel.GetComponent<ShopPanel>().Cost = Cost[i-1];
			panel.GetComponentInChildren<Text>().text = Cost[i-1].ToString();
		}
			

	}
	void NotEnoughGold()
	{
		Debug.Log("derp");
	}
}

