using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemShopPanel : MonoBehaviour {

	public int ItemIndex;
	Item ItemForSale;
	public int ItemCost;
	[Range(0,1)]
	public float ItemSale;
	Inventory inventory; 
	ItemDatabase itemDataBase;

	// Use this for initialization

	void Start () {
	
	}

	public int CalculateSale()
	{
		float Total = 0;
		if(ItemSale !=0)
		{
			Total = ItemSale *(float) ItemCost ;
			Total -= ItemCost; 
			Total *= -1;
		}	
		else
			Total = ItemCost;
		
		return (int)Total  ;
	}

}
