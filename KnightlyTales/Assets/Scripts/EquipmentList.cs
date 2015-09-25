using UnityEngine;
using System.Collections;
using System.Collections.Generic;

	public class EquipmentList : MonoBehaviour {


		public Sword sword ;
		private Shield shield;
		private Bow bow;
		private Quiver quiver;
		[HideInInspector]
		public int SwordUpgradeIndex = 0;
		[HideInInspector]
		public int ShieldUpgradeIndex = 0;
		[HideInInspector]
		public int BowUpgradeIndex = 0;
		[HideInInspector]
		public int QuiverUpgradeIndex = 0;

		public List<Sword> SwordUpgrades = new List<Sword>();
		public List<Shield> ShieldUpgrades = new List<Shield>();
		public List<Bow> BowUpgraes = new List<Bow>();
		public List<Quiver> QuiverUpgrades = new List<Quiver>(); 

		public List<int> SwordCost= new List<int>();
		public List<int> ShieldCost= new List<int>();
		public List<int> BowCost= new List<int>();
		public List<int> QuiverCost= new List<int>();
		// Use this for initialization
		void Start () {
			
		



		}
		
		// Update is called once per frame
		void Update () {
		
		}

		public void UpgradeSword(int UpgradeIndex)
		{
			SwordUpgradeIndex = UpgradeIndex;
			sword = SwordUpgrades[SwordUpgradeIndex];
		}

		public void UpgradeShield(int UpgradeIndex)
		{
			ShieldUpgradeIndex = UpgradeIndex;
			shield = ShieldUpgrades[ShieldUpgradeIndex];

		}

		public void UpgradeBow(int UpgradeIndex)
		{
			BowUpgradeIndex = UpgradeIndex;
			bow = BowUpgraes[BowUpgradeIndex];
		}
		public void UpgradeQuiver( int UpgradeIndex)
		{
			QuiverUpgradeIndex = UpgradeIndex;
			quiver = QuiverUpgrades[QuiverUpgradeIndex];
		}
	}

