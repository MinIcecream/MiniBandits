using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoomInfo;

public class BaseRoomManager : MonoBehaviour
{
    public FloorManager floorMan;
    public GameObject[] doors;
    //Where to spawn player in the room
    public Transform playerSpawnPt,itemSpawnPt;
    public rooms room;

    public virtual void Awake()
    {
        floorMan = GameObject.FindWithTag("FloorManager").GetComponent<FloorManager>();
        floorMan.playerSpawnPt = playerSpawnPt;
    }

    public virtual void TransitionToNextRoom(rooms room)
    {

    }
    //AFTER YOU BEAT ALL THE ENEMIES:
    public virtual void EndRoom()
    { 
        rooms[] generatedRooms = RoomOptionGenerator.GenerateRoomOptions(doors.Length);

        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].GetComponent<Door>().SetReward(generatedRooms[i]); 
        }
    }
}
