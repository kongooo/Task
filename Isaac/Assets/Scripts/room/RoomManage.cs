using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManage : MonoBehaviour {

    public class Room
    {
        public Vector3 center;
        public Vector3 door_up, door_down, door_right, door_left;

        public Room(float x,float y)
        {
            this.center=new Vector3(x,y,0);
            this.door_up=new Vector3(x,y+3.7f,0);
            this.door_down=new Vector3(x,y-3.75f,0);
            this.door_left=new Vector3(x+6.7f,y,0);
            this.door_left=new Vector3(x-6.65f,y,0);
        }
    }

    public GameObject[] room_prefabs;
    public GameObject[] door_prefabs;

	
}
