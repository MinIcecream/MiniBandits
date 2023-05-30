using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoomInfo;

public class RoomGeneratorAlgorithm : MonoBehaviour
{ 
  //  RoomGeneratorAlgorithm instance = new RoomGeneratorAlgorithm();
   
    public static void UpdateOdds()
    {
        Health playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
        GoldManager gold = GameObject.FindWithTag("Player").GetComponent<GoldManager>();
        KeyManager key = GameObject.FindWithTag("Player").GetComponent<KeyManager>();

        RoomOptionGenerator.SetDefaultRoomChances();
        
        /*
        for (int i = 0; i < RoomOptionGenerator.rooms.Count;i++)
        {
            if (RoomOptionGenerator.rooms[i].locked)
            {
                RoomOptionGenerator.ChangeRoomChance(RoomOptionGenerator.rooms[i].reward, 2220);
            }
        }/*
        if (key.GetKeys() < 1 && RoomOptionGenerator.previouslyGeneratedRooms.Count > 1)
        {
            if(RoomOptionGenerator.previouslyGeneratedRooms[RoomOptionGenerator.previouslyGeneratedRooms.Count - 1].locked)
            {
                for (int i = 0; i < RoomOptionGenerator.rooms.Count; i++)
                {
                    if (RoomOptionGenerator.rooms[i].locked)
                    {
                        RoomOptionGenerator.ChangeRoomChance(RoomOptionGenerator.rooms[i].reward, 0);
                    }
                }
            }
        }*/
        if (gold.GetGold() <= 20)
        {
            RoomOptionGenerator.ChangeRoomChance(rewardTypes.market, 0);
            RoomOptionGenerator.ChangeRoomChance(rewardTypes.blackSmith, 0);
        }
        else if (gold.GetGold() < 40)
        {
            RoomOptionGenerator.ChangeRoomChance(rewardTypes.market, 20);
            RoomOptionGenerator.ChangeRoomChance(rewardTypes.blackSmith, 20);
        }
        else
        {
            RoomOptionGenerator.ChangeRoomChance(rewardTypes.market, 40);
            RoomOptionGenerator.ChangeRoomChance(rewardTypes.blackSmith, 40);
        }

        if (playerHealth.GetHealth() < playerHealth.GetMaxHealth() / 4)
        {
            RoomOptionGenerator.ChangeRoomChance(rewardTypes.vitalityShrine, 15); 
        } 
        else
        {
            RoomOptionGenerator.ChangeRoomChance(rewardTypes.vitalityShrine, 0);
        }
        if (GameManager.floor < 3)
        {
            RoomOptionGenerator.ChangeRoomChance(rewardTypes.rareWeapon, 0);
            RoomOptionGenerator.ChangeRoomChance(rewardTypes.rareArmor, 0); 
        }
        else
        {
            RoomOptionGenerator.ChangeRoomChance(rewardTypes.rareWeapon, 5);
            RoomOptionGenerator.ChangeRoomChance(rewardTypes.rareArmor, 5);
        }
        if (GameManager.floor == 1 && GameManager.room == 0)
        {
            RoomOptionGenerator.ChangeRoomChance(rewardTypes.largeGold, 0);  
        }
        else
        {
            RoomOptionGenerator.ChangeRoomChance(rewardTypes.largeGold, 5); 
        } 
        foreach (rewardTypes value in rewardTypes.GetValues(typeof(rewardTypes)))
        {
            for (int i = RoomOptionGenerator.previouslyGeneratedRooms.Count - 1; i > RoomOptionGenerator.previouslyGeneratedRooms.Count - 5; i--)
            {
                if (i < 0)
                {
                    break ;
                } 
                if (RoomOptionGenerator.previouslyGeneratedRooms[i].reward == value && RoomOptionGenerator.previouslyGeneratedRooms[i].reward != rewardTypes.randomWeapon && RoomOptionGenerator.previouslyGeneratedRooms[i].reward != rewardTypes.randomArmor)
                {
                    RoomOptionGenerator.ChangeRoomChance(value,0); 
                } 
            }
        } 
    }

}
