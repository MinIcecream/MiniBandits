using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoomInfo;
using TMPro;

public class Door : MonoBehaviour
{
    public room room;

    public BaseRoomManager levelMan;
    FloorManager floorMan;

    public TextMeshProUGUI tmp;

    public void SetReward(room r)
    {
        room = r;
        tmp.text = room.reward.ToString();
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        floorMan = GameObject.FindWithTag("FloorManager").GetComponent<FloorManager>();

        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("LEAVING...");
            levelMan.TransitionToNextRoom(room);
            Destroy(this);
        }
    } 
}
