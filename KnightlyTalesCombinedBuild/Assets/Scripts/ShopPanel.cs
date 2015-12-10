using UnityEngine;
using System.Collections;
using UnityEngine.UI;

	public class ShopPanel : MonoBehaviour {
		public Button BuyButton ;
		public ShopSection shopSection;
		public int UpgradeIndex;
		public int Cost = 1000;
		public GearType Gear;

		// Use this for initialization
		void Start () {

			BuyButton = transform.GetComponentInChildren<Button>();
			BuyButton.onClick.AddListener(() => { shopSection.UpgradeGear(Gear,UpgradeIndex,Cost );});


		}
		
		// Update is called once per frame
		void Update () {
		
		}

	}
