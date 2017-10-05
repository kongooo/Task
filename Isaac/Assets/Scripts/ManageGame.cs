using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageGame : MonoBehaviour {

	List<RoomManage.Room> rooms=new List<RoomManage.Room>();
    public Vector3 start;

	void Start () {
		LoadRoom();
	}

	
	void Update () {
		
	}

    public void LoadRoom()
    {
        RoomManage.Instance.initStartRoom(start);
        RoomManage.Instance.initDoorForRooms();
        RoomManage.Instance.initRoomForDoors();
        RoomManage.Instance.islast = true;
        RoomManage.Instance.initDoorForRooms();
    }
}
