using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRoom : MonoBehaviour
{
    public ProgressRoomManager levelMan;
    FloorManager floorMan;

    void OnTriggerStay2D(Collider2D coll)
    {
        floorMan = GameObject.FindWithTag("FloorManager").GetComponent<FloorManager>();
         
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("PLAYER ENETERD ROOM!");

            levelMan.StartRoom();

            Destroy(this);
        }
    }
}
