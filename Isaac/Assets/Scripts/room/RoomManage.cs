using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManage : MonoBehaviour
{
    private static RoomManage _instance;
    public static RoomManage Instance{get { return _instance; }}

    public Vector3 start;

    public class Room
    {
        public Vector3 center;
        public Vector3 door_up, door_down, door_right, door_left;
        public int doorDir;

        public Room(float x,float y)
        {
            this.center=new Vector3(x,y,0);
            this.door_up=new Vector3(0,1.185f,0);
            this.door_down=new Vector3(0,-1.185f,0);
            this.door_left=new Vector3(-1.97f,0,0);
            this.door_right=new Vector3(1.97f,0,0);
        }
        public Room parentRoom;
    }

    public class Door
    {
        public int number;
        public Room Parent;
        public GameObject ParentPrefab,myPrefab,nextRoom;

        public Door(int n, Room room,GameObject p,GameObject m)
        {
            this.number = n;
            this.Parent = room;
            this.ParentPrefab = p;
            this.myPrefab = m;
        }
    }

    public GameObject[] room_prefabs;
    public GameObject[] door_prefabs;
    public GameObject[] enemy_prefabs;
    public GameObject start_room;
    public GameObject boss_room;
    public GameObject []boss_doors;
    public LayerMask OBLayer;
    private int Roomcount=0;
    public int RandomCount;
    private int Doorcount=0;
    public LayerMask roomLayer;
    //防止出现重复房间
    private List<int> DoorList = new List<int>();
   
    private List<GameObject> RoomPrefabs=new List<GameObject>();
    private List<Room> roomList=new List<Room>();
    private List<Door> RecordDoor=new List<Door>();
    private int roomStart=1, roomEnd=0, doorStart=0, doorEnd=-1;
    private bool iswall;
    [HideInInspector]public bool islast;
    public int enemymin, enemymax;

    void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        islast = false;
        initStartRoom(start);
        for (int i = 0; i < RandomCount; i++)
        {
            initDoorForRooms();
            initRoomForDoors();
        }       
        islast = true;
        initDoorForRooms();
        initBossRoom();
    }

    public void initStartRoom(Vector3 pos)
    {
        Room roomMes=new Room(pos.x,pos.y);
        roomMes.doorDir = -1;
        roomList.Add(roomMes);
        GameObject room = GameObject.Instantiate(room_prefabs[0], pos, Quaternion.identity);
        RoomPrefabs.Add(room);
        //随机生成门
        initDoor(roomMes,room);       
        initRoomForDoors();
    }

    public void initRoom(Room ParentRoom,GameObject _room,int doorNumber,GameObject door,Door doorMes)
    {
        Roomcount++;
        int roomNumber = Random.Range(0, 9);
        Vector3 roomPos=new Vector3();
        GameObject room;
        switch (doorNumber)
        {
            case 0:
                 roomPos = ParentRoom.center + new Vector3(0, 10, 0);
                 break;
            case 1:
                 roomPos = ParentRoom.center + new Vector3(18, 0, 0);
                 break;
            case 2:
                 roomPos = ParentRoom.center + new Vector3(0, -10, 0);
                 break;
            case 3:
                 roomPos = ParentRoom.center + new Vector3(-18, 0, 0);
                 break;
        }
        for(int i=0;i< _room.GetComponentsInChildren<BoxCollider>().Length;i++)
        _room.GetComponentsInChildren<BoxCollider>()[i].enabled=false;
        iswall = Physics.Linecast(ParentRoom.center, roomPos,roomLayer);
        for (int i = 0; i < _room.GetComponentsInChildren<BoxCollider>().Length; i++)
         _room.GetComponentsInChildren<BoxCollider>()[i].enabled = true;
        if (!iswall)
        {
            roomEnd++;
            Room roomMes = new Room(roomPos.x, roomPos.y);
            roomMes.doorDir = GetDoorDir(doorNumber);
            roomMes.parentRoom = ParentRoom;
            roomList.Add(roomMes);
            room = GameObject.Instantiate(room_prefabs[roomNumber], roomPos, Quaternion.identity);
            doorMes.nextRoom = room;
            RoomPrefabs.Add(room);
            //随机生成敌人
            initEnemy(roomPos, room);                
        }
        else
        {
            Destroy(door);
        }
    }

    public void initDoorForRooms()
    {
        doorStart = doorEnd + 1;
        if(roomEnd>=roomStart)
        for (int i = roomStart; i <= roomEnd; i++)
        {
            initDoor(roomList[i], RoomPrefabs[i]);
        }
    }

    public void initRoomForDoors()
    {
        roomStart = roomEnd + 1;
        if(doorEnd>=doorStart)
        for (int i = doorStart; i <= doorEnd; i++)
        {
            initRoom(RecordDoor[i].Parent,RecordDoor[i].ParentPrefab,RecordDoor[i].number,RecordDoor[i].myPrefab,RecordDoor[i]);
        }
    }

    //得到下一个房间所必须有的门的编号
    public int GetDoorDir(int number)
    {
        switch (number)
        {
            case 0:
                return 2;
            case 1:
                return 3;
            case 2:
                return 0;
            case 3:
                return 1;
        }
        return -1;
    }

    public void initDoor(Room MesRoom,GameObject room)
    {        
        Doorcount++;
        if (DoorList.Count != 0)
            DoorList.Clear();
        int doorCount = Random.Range(2, 5);
        //创建下一个房间必须有的门
        if (MesRoom.doorDir != -1)
        {
            DoorList.Add(MesRoom.doorDir);
            Vector3 doorpos = GetDoorPos(MesRoom);
            GameObject door = GameObject.Instantiate(door_prefabs[MesRoom.doorDir], doorpos, Quaternion.identity);
            door.transform.SetParent(room.transform, false);
        }

        //随机创建剩下的门
        if (!islast)
        {
            for (int k = 0; k < doorCount; k++)
            {
                int doorType = Random.Range(0, 4);
                if (DoorList.Contains(doorType))
                    continue;
                switch (doorType)
                {
                    case 0:
                        GameObject doorUP = GameObject.Instantiate(door_prefabs[doorType], MesRoom.door_up, Quaternion.identity);
                        doorUP.transform.SetParent(room.transform, false);
                        RecordDoor.Add(new Door(0, MesRoom,room,doorUP));
                        break;
                    case 1:
                        GameObject doorRight = GameObject.Instantiate(door_prefabs[doorType], MesRoom.door_right, Quaternion.identity);
                        doorRight.transform.SetParent(room.transform, false);
                        RecordDoor.Add(new Door(1, MesRoom, room, doorRight));
                        break;
                    case 2:
                        GameObject doorDown = GameObject.Instantiate(door_prefabs[doorType], MesRoom.door_down, Quaternion.identity);
                        doorDown.transform.SetParent(room.transform, false);
                        RecordDoor.Add(new Door(2, MesRoom, room, doorDown));
                        break;
                    case 3:
                        GameObject doorLeft = GameObject.Instantiate(door_prefabs[doorType], MesRoom.door_left, Quaternion.identity);
                        doorLeft.transform.SetParent(room.transform, false);
                        RecordDoor.Add(new Door(3, MesRoom, room, doorLeft));
                        break;
                }
                DoorList.Add(doorType);                
                doorEnd++;
            }
        }
    }

    public void initEnemy(Vector3 pos,GameObject room)
    {
        int enemy_count = Random.Range(enemymin, enemymax);
        int i = 0;
        Vector2[]recordPos=new Vector2[8];
        Vector2[] EnemyPos = new Vector2[8];
        GameObject[] ForChange=new GameObject[8];
        for (int x = (int)pos.x - 5; x < pos.x + 5; x++)
        for (int y = (int)pos.y - 2; y < (int)pos.y + 2; y++)
        {
            Collider[] colliders = Physics.OverlapSphere(pos, 0.5f, OBLayer);
            if (colliders.Length == 0)
            if (i < 8)
                    recordPos[i] = new Vector2(x, y);
            i++;
        }
        for (int k = 0; k < 8; k++)
        {
            ForChange[k] = new GameObject("change1");
            ForChange[k].transform.position = recordPos[k];
            ForChange[k].transform.SetParent(room.transform);
        }
        
        for (int j = 0; j <= enemy_count; j++)
        {
            int enemy_type = Random.Range(0, 120) % 6;                       
            GameObject enemy = GameObject.Instantiate(enemy_prefabs[enemy_type],ForChange[j].transform.localPosition, Quaternion.identity);
            if (enemy.GetComponent<GRID>())
            {
                enemy.GetComponent<GRID>().centerX = (int)pos.x;
                enemy.GetComponent<GRID>().centerY = (int)pos.y;
            }
            enemy.transform.SetParent(room.transform,false);
        }
        foreach (GameObject temp in ForChange)
        {
            Destroy(temp);
        }
    }
    //通过编号得到门的位置
    public Vector3 GetDoorPos(Room roomMes)
    {
        switch (roomMes.doorDir)
        {
            case 0:
                return roomMes.door_up;
            case 1:
                return roomMes.door_right;
            case 2:
                return roomMes.door_down;
            case 3:
                return roomMes.door_left;
        }
        return Vector3.zero;
    }

    private Vector3 GetBossDoorDir(Room bossroom)
    {
        switch (bossroom.doorDir)
        {
            case 2:
                return bossroom.door_up;
            case 3:
                return bossroom.door_right;
            case 0:
                return bossroom.door_down;
            case 1:
                return bossroom.door_left;
        }
        return Vector3.zero;
    }
    private int GetBossDoorNumber(Room bossroom)
    {
        switch (bossroom.doorDir)
        {
            case 2:
                return 0;
            case 3:
                return 1;
            case 0:
                return 2;
            case 1:
                return 3;
        }
        return 0;
    }

    public void initBossRoom()
    {
        Debug.Log("boss");
        int BossRoom = Random.Range(roomStart, roomEnd+1);
        GameObject BossDoor =GameObject.Instantiate(boss_doors[GetBossDoorNumber(roomList[BossRoom])], GetBossDoorDir(roomList[BossRoom]), Quaternion.identity);
        BossDoor.transform.SetParent(RoomPrefabs[BossRoom].transform,false);
        Vector3 BossroomPos = new Vector3();
        GameObject room;
        switch (GetBossDoorNumber(roomList[BossRoom]))
        {
            case 0:
                BossroomPos = roomList[BossRoom].center + new Vector3(0, 10, 0);
                break;
            case 1:
                BossroomPos = roomList[BossRoom].center + new Vector3(18, 0, 0);
                break;
            case 2:
                BossroomPos = roomList[BossRoom].center + new Vector3(0, -10, 0);
                break;
            case 3:
                BossroomPos = roomList[BossRoom].center + new Vector3(-18, 0, 0);
                break;
        }
        GameObject.Instantiate(boss_room, BossroomPos, Quaternion.identity);
    }    
}
