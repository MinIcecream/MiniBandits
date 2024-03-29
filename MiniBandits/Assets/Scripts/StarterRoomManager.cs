using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoomInfo;

public class StarterRoomManager : BaseRoomManager
{
    public GameObject level;   

    public void Start()
    { 
        EndRoom();
    }
      

    //WHEN THE PLAYER WALKS THROUGH THE DOOR:
    public override void TransitionToNextRoom(roomConfig r)
    {
        floorMan.SpawnRoom(r);

        floorMan.UpdatePlayerAndCameraPos();
    } 
}
