using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoomInfo;

public class RoomGeneratorAlgorithm : MonoBehaviour
{
    Health playerHealth;
    GoldManager gold;

    void Awake()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
        gold = GameObject.FindWithTag("Player").GetComponent<GoldManager>();
        UpdateOdds();
    }
    void Update()
    {
        UpdateOdds();
    }

    void UpdateOdds()
    { 
        if (gold.GetGold() <= 20)
        {
            RoomOptionGenerator.rooms[1] = new roomConfig(0, rewardTypes.market, false);
            RoomOptionGenerator.rooms[2] = new roomConfig(0, rewardTypes.blackSmith, false);
        }
        else if (gold.GetGold() < 40)
        {
            RoomOptionGenerator.rooms[1] = new roomConfig(20, rewardTypes.market, false);
            RoomOptionGenerator.rooms[2] = new roomConfig(20, rewardTypes.blackSmith, false);
        }
        else
        {
            RoomOptionGenerator.rooms[1] = new roomConfig(40, rewardTypes.market, false);
            RoomOptionGenerator.rooms[2] = new roomConfig(40, rewardTypes.blackSmith, false);
        }

        if (playerHealth.GetHealth() < playerHealth.GetMaxHealth()/4)
        { 
            RoomOptionGenerator.rooms[5] = new roomConfig(15, rewardTypes.vitalityShrine, true);
        }
        else if (playerHealth.GetHealth() < playerHealth.GetMaxHealth())
        { 
            RoomOptionGenerator.rooms[5] = new roomConfig(10, rewardTypes.vitalityShrine, true);
        }
        else
        { 
            RoomOptionGenerator.rooms[5] = new roomConfig(0, rewardTypes.vitalityShrine, true);
        }  

        bool allowMarket = true;
        bool allowBlacksmith = true;
        for (int i = RoomOptionGenerator.previouslyGeneratedRooms.Count-1; i > RoomOptionGenerator.previouslyGeneratedRooms.Count - 7; i--)
        {
            if (i < 0)
            {
                return;
            }
            if (RoomOptionGenerator.previouslyGeneratedRooms[i].reward == rewardTypes.blackSmith)
            {
                allowBlacksmith = false;
            }
            if (RoomOptionGenerator.previouslyGeneratedRooms[i].reward == rewardTypes.market)
            {
                allowMarket = false;
            }
        }
        if (!allowBlacksmith)
        {
            RoomOptionGenerator.rooms[2] = new roomConfig(0, rewardTypes.blackSmith, false);
        }
        if (!allowMarket)
        {
            RoomOptionGenerator.rooms[1] = new roomConfig(0, rewardTypes.market, false);
        }
     

    }
}
