using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageGame : MonoBehaviour {

	List<RoomManage.Room> rooms=new List<RoomManage.Room>();
    public Vector3 start;

	void Awake () {
		LoadRoom();
	}

	
	void Update () {
		
	}

    public void LoadRoom()
    {
        RoomManage.Instance.initStartRoom(start);
    }
}
