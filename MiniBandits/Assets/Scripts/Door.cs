using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoomInfo;
using TMPro;

public class Door : MonoBehaviour
{
    public rooms room;

    public BaseRoomManager levelMan;
    FloorManager floorMan;

    public TextMeshProUGUI tmp;
     
    public void SetReward(rooms r)
    {
        room = r; 
        if(room== rooms.starter)
        { 
            switch (room)
            {
                case rooms.randomWeapon:
                    tmp.text = "Random Weapon";
                    break;
                case rooms.randomArmor:
                    tmp.text = "Random Armor";
                    break;
                case rooms.gold:
                    tmp.text = "A Small Amount of Gold";
                    break;
                case rooms.vitalityShrine:
                    tmp.text = "Shrine of Vitality";
                    break;
                case rooms.defenseShrine:
                    tmp.text = "Shrine of Defense";
                    break;
                case rooms.powerShrine:
                    tmp.text = "Shrine of Power";
                    break;
                case rooms.speedShrine:
                    tmp.text = "Shrine of Speed";
                    break;
            }
        }
        else
        { 
            tmp.text = room.ToString(); 
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        floorMan = GameObject.FindWithTag("FloorManager").GetComponent<FloorManager>();

        if (coll.gameObject.tag == "Player")
        { 
            levelMan.TransitionToNextRoom(room);
            Destroy(this);
        }
    } 
}
