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
        graveyard
    }
    public enum roomTypes
    { 
        starter,
       // campfire, 
       // market,
        normal,
        blacksmith
    }
    public struct room
    {
        public rewardTypes reward;
        public roomTypes roomType;
    }
    public enum rewardTypes
    {
        gold,
        randomWeapon,
        randomArmor
    }
}
public class RoomOptionGenerator
{
    public float normalRoomWeight=0;

    public static roomThemes[] GenerateRoomThemes(int numThemes)
    {
        roomThemes[] themes = new roomThemes[numThemes];

        for(int i=0; i < numThemes; i++)
        { 
            themes[i] = roomThemes.graveyard;

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

    public static room[] GenerateRoomOptions(int numDoors)
    {
        RoomOptionGenerator instance = new RoomOptionGenerator(); 

        room[] rooms= new room[numDoors];
         
        System.Random random = new System.Random();

        List<rewardTypes> alreadyAssignedRewards = new List<rewardTypes>();

        for (int i = 0; i < numDoors; i++)
        { 
            if (UnityEngine.Random.Range(0, 100) <= instance.normalRoomWeight)
            { 
                rooms[i].roomType = roomTypes.normal; 
                 
                Array values = typeof(rewardTypes).GetEnumValues();

                //Generatinga  random reward
                int index = random.Next(values.Length);
                rewardTypes randomReward = (rewardTypes)values.GetValue(index);
                rooms[i].reward = randomReward;

                if (alreadyAssignedRewards.Contains(rooms[i].reward))
                {
                    while (alreadyAssignedRewards.Contains(rooms[i].reward))
                    {
                        index = random.Next(values.Length);
                        randomReward = (rewardTypes)values.GetValue(index);
                        rooms[i].reward = randomReward;
                    }
                    alreadyAssignedRewards.Add(rooms[i].reward);
                }
                else
                {
                    alreadyAssignedRewards.Add(rooms[i].reward);
                }  
            }
            else
            { 
                Array values = Enum.GetValues(typeof(roomTypes)); 
                roomTypes randomRoom=roomTypes.normal; 
                while (randomRoom == roomTypes.normal || randomRoom==roomTypes.starter)
                {  
                    randomRoom = (roomTypes)values.GetValue(random.Next(values.Length)); 
                }
                rooms[i].roomType = randomRoom;
            }
        }
        return rooms;
    } 
    public static Weapon GenerateRandomWeapon()
    {
        UnityEngine.Object[] tempList = Resources.LoadAll("Items/Weapons", typeof(Weapon));
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

