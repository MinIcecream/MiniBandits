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

    public TextMeshPro tmp;

    public bool locked;

    public GameObject lockedDoor;
    public Sprite lockedSprite, unlockedSprite;
    public void SetReward(roomConfig r)
    {
        room = r; 
    
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
            case rewardTypes.heart:
                tmp.text = "Heart";
                break;
            case rewardTypes.rareWeapon:
                tmp.text = "Rare Weapon";
                break; 
            case rewardTypes.rareArmor:
                tmp.text = "Rare Armor";
                break;
            case rewardTypes.key:
                tmp.text = "Key";
                break;
            case rewardTypes.largeGold:
                tmp.text = "A Lot of Gold";
                break;
        }

        if (r.locked)
        {
            locked = true;
            lockedDoor.SetActive(true);
            GetComponent<SpriteRenderer>().sprite = lockedSprite;
        }
        else
        {
            Destroy(lockedDoor);
        }
    }
    public void Unlock()
    { 
        locked = false;
        Destroy(lockedDoor);
        GetComponent<SpriteRenderer>().sprite = unlockedSprite;
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (!locked)
        { 
            floorMan = GameObject.FindWithTag("FloorManager").GetComponent<FloorManager>();

            if (coll.gameObject.tag == "Player")
            {
                levelMan.TransitionToNextRoom(room);
                Destroy(this);
            }
        } 
    } 
}
