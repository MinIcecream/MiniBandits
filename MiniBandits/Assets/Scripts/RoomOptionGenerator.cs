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
        blackSmith,
        starter,
        market,
        normal
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
    public float normalRoomWeight=100;

     
    public static room[] GenerateRoomOptions(int numDoors)
    {
        RoomOptionGenerator instance = new RoomOptionGenerator(); 

        room[] rooms= new room[numDoors];

        for(int i = 0; i < numDoors; i++)
        { 
            if (UnityEngine.Random.Range(0, 100) <= instance.normalRoomWeight)
            { 
                rooms[i].roomType = roomTypes.normal;

                System.Random random = new System.Random();

                Type type = typeof(rewardTypes);
                Array values = type.GetEnumValues();
                int index = random.Next(values.Length); 
                rewardTypes value = (rewardTypes)values.GetValue(index);
                Debug.Log(value);
                rewardTypes randomReward = value;
                rooms[i].reward = randomReward;
            }
            else
            { 
                Array values = Enum.GetValues(typeof(roomTypes)); 
                roomTypes randomRoom=roomTypes.normal;
                Debug.Log(randomRoom);
                while (randomRoom == roomTypes.normal)
                { 
                    System.Random random = new System.Random();
                    randomRoom = (roomTypes)values.GetValue(random.Next(values.Length)); 
                }
                rooms[i].roomType = randomRoom;
            }
        }
        return rooms;
    } 
}

