using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFinalDoor : MonoBehaviour
{
    public GameObject door;
    public GameObject finalDoor;
    public ProgressRoomManager man;

    void Start()
    {
        if(GameManager.floor == 10)
        {  
            Destroy(door); 
        }
        else
        {
            Destroy(finalDoor);
        }
    }
}
