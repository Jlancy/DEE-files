using UnityEngine;
using System.Collections;


namespace KnightlyTales
{
	public class tempAddItem : MonoBehaviour {
		Inventory inv;
		public ItemDatabase itemD;
		// Use this for initialization
		void Start () {
			inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();

		}

		// Update is called once per frame
		void Update () {
			if(Input.GetMouseButtonDown(2))
			{
				inv.Items.Add(itemD.items[1]);
			}
		}
	}
}
