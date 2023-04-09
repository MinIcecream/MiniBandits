using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public enum rooms
    {
        gold,
        randomWeapon,
        randomArmor,
        vitalityShrine,
        powerShrine,
        defenseShrine,
        speedShrine,
        blackSmith,
        market,
        starter
    }   
}
public class RoomOptionGenerator
{  
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

    public static rooms[] GenerateRoomOptions(int numDoors)
    {
        RoomOptionGenerator instance = new RoomOptionGenerator();

        rooms[] roomOptions= new rooms[numDoors];
         
        System.Random random = new System.Random();

        List<rooms> alreadyAssignedRooms = new List<rooms>();

        for (int i = 0; i < numDoors; i++)
        {  
            Array values = typeof(rooms).GetEnumValues();

            //Generatinga  random reward
            int index = random.Next(values.Length);
            rooms randomRoom = (rooms)values.GetValue(index);
            roomOptions[i] = randomRoom;

            if (alreadyAssignedRooms.Contains(roomOptions[i]))
            {
                while (alreadyAssignedRooms.Contains(roomOptions[i])|| randomRoom == rooms.starter)
                {
                    index = random.Next(values.Length);
                    randomRoom = (rooms)values.GetValue(index);
                roomOptions[i] = randomRoom;
                }
            alreadyAssignedRooms.Add(roomOptions[i]);
            }
            else
            {
            alreadyAssignedRooms.Add(roomOptions[i]);
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

