using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System;
using RoomInfo;
using System.Linq;

namespace RoomInfo
{
    public enum roomThemes
    {
        Graveyard,
        EnchantedForest,
        GoblinVillage,
        MechCity,
        Mothership,
        RabidZoo,
        SpiderDen
    }
    public enum rewardTypes
    {
        gold,
        randomWeapon,
        randomArmor,
        shrine,
        blackSmith,
        market,
        starter
    }   
    public struct roomConfig{
        public int chance;
        public rewardTypes reward;
        public bool progressRoom; 

        public roomConfig(int c, rewardTypes r, bool p)
        {
            chance = c;
            reward = r;
            progressRoom = p;
        }
    }
}
public class RoomOptionGenerator
{  
    public static List<roomConfig> rooms = new List<roomConfig>() 
    { 
        new roomConfig(20, rewardTypes.starter, false),
        new roomConfig(20, rewardTypes.market, false),
        new roomConfig(20, rewardTypes.blackSmith, false),
        new roomConfig(20, rewardTypes.randomWeapon, true),
        new roomConfig(20, rewardTypes.randomArmor, true),
        new roomConfig(20, rewardTypes.gold, true),

    };
     
    public static roomThemes[] GenerateRoomThemes(int numThemes)
    {
        roomThemes[] themes = new roomThemes[numThemes];

        for(int i=0; i < numThemes; i++)
        { 
            System.Random random = new System.Random();

            Type type = typeof(roomThemes);
            Array values = type.GetEnumValues();
            int index = random.Next(values.Length);
            roomThemes value = (roomThemes)values.GetValue(index);
            //Debug.Log(value);
            roomThemes randomTheme = value;
            themes[i] = randomTheme;
        }
        return themes;
    }

    public static List<roomConfig> GenerateRoomOptions(int numDoors)
    { 
        RoomOptionGenerator instance = new RoomOptionGenerator();

        List<roomConfig> roomOptions = new List<roomConfig>(numDoors); 
        List<roomConfig> alreadyAssignedRooms = new List<roomConfig>(); 
        int totalWeight = 0;

        foreach(roomConfig s in rooms)
        {
            totalWeight += s.chance;
        } 

        for(int i = 0; i < numDoors; i++)
        { 
            roomOptions.Add(rooms[0]);
            while (alreadyAssignedRooms.Contains(roomOptions[i]) || roomOptions[i].reward == rewardTypes.starter)
            {
                int rand = Random.Range(0, totalWeight);
                int cumulativeWeight = 0;

                foreach (roomConfig s in rooms)
                {
                    cumulativeWeight += s.chance;
                    if (rand < cumulativeWeight)
                    { 
                        roomOptions[i] = s;
                        break;
                    }
                }
            }
        }
        return roomOptions;
    } 
    public static Weapon GenerateRandomWeapon()
    {
        UnityEngine.Object[] tempList = Resources.LoadAll("Items/Weapons/BaseWeapons", typeof(Weapon));
        List<Weapon> allWeapons = new List<Weapon>();

        foreach(Weapon w in tempList)
        { 
            allWeapons.Add(w);
        } 
        int ran = UnityEngine.Random.Range(0, allWeapons.Count); 
        return allWeapons[ran];
    }
    public static Armor GenerateRandomArmor()
    { 
        UnityEngine.Object[] tempList = Resources.LoadAll("Items/Armor", typeof(Armor));
        List<Armor> allArmor = new List<Armor>();

        foreach (Armor w in tempList)
        { 
            allArmor.Add(w);
        } 
        int ran = UnityEngine.Random.Range(0, allArmor.Count); 
        return allArmor[ran];
    } 
}

