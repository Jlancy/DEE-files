using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace KnightlyTales
{
	public class PageButton : MonoBehaviour {

		public int PageNumberStart;
		[HideInInspector]
		public SlotManger slotManger;


		// Use this for initialization
		void Start () {
			slotManger = GameObject.FindGameObjectWithTag("SlotManger").GetComponent<SlotManger>();
		}
		
		// Update is called once per frame
		void Update () {
		
		}
		public void LoaddPages()
		{
			//Debug.Log(PageNumberStart);
			slotManger.SlotNumberMod = PageNumberStart;
			slotManger.updateCheck = true;
		}
	}
}
