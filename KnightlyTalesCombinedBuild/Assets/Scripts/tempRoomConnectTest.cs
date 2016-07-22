using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class tempRoomConnectTest : MonoBehaviour {


	Room center = new Room (0, 0, 1, 1);

	public List<Room> testRooms ;

	[Range(0,3)]
	public int testRoomInt=0;
	public bool hallWayReturn;

	public bool setRect =false;
	// Use this for initialization
    void Start()
    {
        //center.layoutVector = new Vector2(0, 0);
        testRooms.Add(center);
    }

	// Update is called once per frame
	void Update () {

		if (hallWayReturn) 
		{

			hallWayReturn = false;
		}
	
	}

    /*intial room connect( x = ammount of room to connect) 
     * go to inital room check if its possoble to connect 
     * if none go to next room and connect( for x)
     * ^only connect if connect is false
     * if there 
     */
}
