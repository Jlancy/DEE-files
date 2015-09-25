using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

	public class ShopSection : MonoBehaviour {

		public EquipmentList equipList;
		public GearType GearUpgrade;
		public GameObject ShopPanel;
		public int Gold;
		//int i = 1;

		// Use this for initialization
		void Start () {
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

			case GearType.Type.Shield:
				if( Gold >= GoldCost)
				{
					equipList.UpgradeShield(UpgradeIndex);
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
			switch(GearUpgrade.Gear)
			{
			case GearType.Type.Sword:
				AttachShopPanel(equipList.SwordUpgrades.Count , equipList.SwordCost);
				break;

			case GearType.Type.Shield:
				AttachShopPanel(equipList.ShieldUpgrades.Count, equipList.ShieldCost);
				break;
			case GearType.Type.Bow:
				AttachShopPanel(equipList.BowUpgraes.Count,equipList.BowCost);
				break;
			case GearType.Type.Quiver:
				AttachShopPanel(equipList.QuiverUpgrades.Count,equipList.QuiverCost);
				break;
			}


		}

		void AttachShopPanel( int PanelCount ,  List<int> Cost)
		{

			for(int i = 1 ; i< PanelCount; i++)
			{
				GameObject panel;
				panel = Instantiate(ShopPanel);
				panel.transform.parent = this.transform;
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

