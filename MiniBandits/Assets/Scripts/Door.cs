using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoomInfo;
using TMPro;

public class Door : MonoBehaviour
{
    public roomConfig room;

    public BaseRoomManager levelMan;
    FloorManager floorMan;

    public TextMeshProUGUI tmp;
     
    public void SetReward(roomConfig r)
    {
        room = r; 
        if(room.progressRoom)
        { 
            switch (room.reward)
            {
                case rewardTypes.randomWeapon:
                    tmp.text = "Random Weapon";
                    break;
                case rewardTypes.randomArmor:
                    tmp.text = "Random Armor";
                    break;
                case rewardTypes.gold:
                    tmp.text = "A Small Amount of Gold";
                    break;
                case rewardTypes.vitalityShrine:
                    tmp.text = "Shrine of Vitality";
                    break;
                case rewardTypes.powerShrine:
                    tmp.text = "Shrine of Power";
                    break;
                case rewardTypes.speedShrine:
                    tmp.text = "Shrine of Speed";
                    break;
                case rewardTypes.defenseShrine:
                    tmp.text = "Shrine of Defense";
                    break;
                case rewardTypes.market:
                    tmp.text = "Market";
                    break;
                case rewardTypes.blackSmith:
                    tmp.text = "Blacksmith";
                    break; 

            }
        }
        else
        { 
            tmp.text = room.reward.ToString(); 
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
