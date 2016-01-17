using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class testLeaf : MonoBehaviour {

	public bool Activate =false;
	public bool Draw =false;
	public List<Leaf> leafList = new List<Leaf>();

	public GameObject block;

	int sliceTimes = 3;
	int currentSlice =0;
	public void Start()
	{
		Leaf root = new Leaf(0,0,34,34);
		leafList.Add(root);

	}
	public void Update()
	{
		if(currentSlice < sliceTimes)
		{
			Activate = true;
		}

		if (Activate)
		{
			StartCoroutine(Test());
		}

		if(Draw)
		{
			int count = leafList.Count;
			//drawRoom(leafList[0].x,leafList[0].y, leafList[0].width, leafList[0].height);
			drawRoom(leafList[count-4].x,leafList[count-4].y, leafList[count-4].width, leafList[count-4].height,0);
			drawRoom(leafList[count-3].x,leafList[count-3].y, leafList[count-3].width, leafList[count-3].height,1);
			drawRoom(leafList[count-2].x,leafList[count-2].y, leafList[count-2].width, leafList[count-2].height,2);
			drawRoom(leafList[count-1].x,leafList[count-1].y, leafList[count-1].width, leafList[count-1].height,3);

			transform.position = new Vector2(33,0);
		}


	}
	// temp room creation to make sure its working
	void drawRoom(int x , int y, int width, int height ,int color)
	{
		// just to visually see rooms for now
		Color colorArea;
		if(color ==1)
			colorArea = Color.green;
		else if (color ==2)
			colorArea = Color.magenta;

		else if(color ==3)
			colorArea = Color.cyan;
		else
			colorArea = Color.yellow;
		// start postion 
		Vector2 CurrentPos = new Vector2(x , y );

		// build room
		for(int row = 0; row < width; row++ )
		{
			//Debug.Log(row);
			for(int col = 0; col <height; col++)
			{
				GameObject tile;
				tile = Instantiate(block);
				tile.transform.parent = this.transform;
				tile.GetComponent<SpriteRenderer>().color =colorArea ;
				tile.transform.position = CurrentPos;
				CurrentPos =  CurrentPos + new Vector2(0,1);
			}
			CurrentPos = CurrentPos + new Vector2(1,- height);
		}

		Draw =false;
	}
	// modify
	// this will loop unitll it cant creat anymore more child leafs
	IEnumerator Test()
	{
		Activate = false;
		


		bool didSplit = true;
		// temp list  to  add new child leafs after to main list 
		// after foreach loop is done 
		List<Leaf> tempLeaf = new List<Leaf>();

		while(didSplit)
		{
			didSplit =false;
			// loop throuhg main leaf list  and check
			// if any leaf has any empty child 
			// if it is add new child leafs if possible
			foreach ( Leaf l in leafList)
			{
				if(l.leftChild == null && l.rightChild == null)
				{
					if(l.width > Leaf.MAX_LEAF_SIZE || l.height > Leaf.MAX_LEAF_SIZE || Random.value > 0.25f )
					{
						if (l.Split())
						{
							tempLeaf.Add(l.leftChild);
							tempLeaf.Add(l.rightChild);
							didSplit =true;
						}
					}
				}
			}
			yield return null;
		}

		// add temp list to main list;
		foreach(Leaf l in tempLeaf)
		{
			leafList.Add(l);
		}
		tempLeaf.Clear();
		currentSlice++;
		// modify later to loop unitll it cant slice anymore;
		if(currentSlice < sliceTimes)
		{
			StartCoroutine(Test());
		}
		else 
			Draw =true;
	}
}
