using UnityEngine;
using System.Collections;

public class SpriteLoader : MonoBehaviour {

	public  Sprite[] ItemSheet ;
	// Use this for initialization
	void Awake () {
		ItemSheet = Resources.LoadAll<Sprite>("Items");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public Sprite ItemImage(int Index)
	{
		return ItemSheet[Index];
	}
}
