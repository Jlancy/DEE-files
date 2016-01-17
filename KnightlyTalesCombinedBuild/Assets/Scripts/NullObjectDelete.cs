using UnityEngine;
using System.Collections;

public class NullObjectDelete : MonoBehaviour {


	public GameObject[] NullTileList;
	bool find;
	bool finished;


	// Update is called once per frame
	void Update () {

		if(!find)	
		{
			NullTileList = GameObject.FindGameObjectsWithTag("Null");

			find = true;
		}
		else
		{  
			if(!finished)
			{
				for (int i = 0; i< NullTileList.Length; i ++)
				{
					Destroy(NullTileList[i]);
				}
				finished =true;
			}



		}

	}
}
