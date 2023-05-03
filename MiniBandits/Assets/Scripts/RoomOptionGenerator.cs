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
        Scrapyard,
        Mothership,
        RabidZoo,
        SpiderDen
    }
    public enum rewardTypes
    {
        gold,
        largeGold,
        randomWeapon,
        randomArmor,
        vitalityShrine,
        powerShrine,
        speedShrine,
        defenseShrine,
        blackSmith,
        market,
        starter,
        rareArmor,
        rareWeapon,
        heart,
        key
    }   
    struct itemChance
    {
        public Item item;
        public int chance;
        public itemChance(Item w, int c)
        {
            chance = c;
            item = w; 
        }
    }
    public struct roomConfig{
        public int chance;
        public rewardTypes reward;
        public bool progressRoom;
        public bool locked;

        public roomConfig(int c, rewardTypes r, bool p, bool l)
        {
            chance = c;
            reward = r;
            progressRoom = p;
            locked = l;
        }
    }
}
public class RoomOptionGenerator
{
    public static List<roomConfig> previouslyGeneratedRooms = new List<roomConfig>();
    public static List<roomConfig> rooms = new List<roomConfig>() 
    { 
        new roomConfig(0, rewardTypes.starter, false, false),
        new roomConfig(15, rewardTypes.market, false, false),
        new roomConfig(15, rewardTypes.blackSmith, false, false),
        new roomConfig(20, rewardTypes.randomWeapon, true, false),
        new roomConfig(20, rewardTypes.randomArmor, true, false),
        new roomConfig(5, rewardTypes.vitalityShrine, true, false),
        new roomConfig(5, rewardTypes.defenseShrine, true, false),
        new roomConfig(5, rewardTypes.powerShrine, true, false),
        new roomConfig(5, rewardTypes.speedShrine, true, false),
        new roomConfig(50, rewardTypes.gold, true, false),
        new roomConfig(5, rewardTypes.rareWeapon, true, true),
        new roomConfig(5, rewardTypes.rareArmor, true, true),
        new roomConfig(5, rewardTypes.heart, true, false),
        new roomConfig(5, rewardTypes.largeGold, true, true),
        new roomConfig(20, rewardTypes.key, true, false)
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
                        previouslyGeneratedRooms.Add(s);
                        break;
                    }
                }
            }
            alreadyAssignedRooms.Add(roomOptions[i]);
        }
        return roomOptions;
    } 
    public static Weapon GenerateRandomWeapon(int c, int u, int r, int e)
    {
        UnityEngine.Object[] tempList = Resources.LoadAll("Items/Weapons/BaseWeapons", typeof(Weapon));
        List<itemChance> allWeapons = new List<itemChance>();
         

        foreach (Weapon w in tempList)
        {
            if (w.rarity == Item.itemRarity.common)
            {
                itemChance newItem = new itemChance(w, c);
                allWeapons.Add(newItem);
            }
            if (w.rarity == Item.itemRarity.uncommon)
            {
                itemChance newItem = new itemChance(w, u); 
                allWeapons.Add(newItem);
            }
            if (w.rarity == Item.itemRarity.rare)
            {
                itemChance newItem = new itemChance(w, r);
                allWeapons.Add(newItem);
            }
            if (w.rarity == Item.itemRarity.epic)
            {
                itemChance newItem = new itemChance(w, e);
                allWeapons.Add(newItem);
            } 
        } 
        int totalWeight = 0;
        foreach (itemChance s in allWeapons)
        {
            totalWeight += s.chance; 
        }

        GameObject inventory = GameObject.FindWithTag("Inventory");
        Weapon weaponToReturn = null;
        if (inventory != null)
        {
            int sup = 0;
            while(inventory.GetComponent<PlayerInventory>().InventoryContains(weaponToReturn))
            {
                sup++;
                if (sup > 20)
                {
                    Debug.Log("NOOOO");
                    break;
                }
                int rand = Random.Range(0, totalWeight);
                int cumulativeWeight = 0;
                foreach (itemChance w in allWeapons)
                {
                    cumulativeWeight += w.chance;
                    if (rand < cumulativeWeight)
                    {
                        weaponToReturn = (Weapon)w.item;
                        break;
                    }
                }
                Debug.Log(weaponToReturn);
            } 
            return weaponToReturn;
        } 
        return (Weapon)tempList[0];
    }
    public static Armor GenerateRandomArmor(int c, int u, int r, int e)
    { 
        UnityEngine.Object[] tempList = Resources.LoadAll("Items/Armor", typeof(Armor));
        List<itemChance> allArmor = new List<itemChance>();
         
        foreach (Armor a in tempList)
        {
            if (a.rarity == Item.itemRarity.common)
            {
                itemChance newItem = new itemChance(a, c);
                allArmor.Add(newItem);
            }
            if (a.rarity == Item.itemRarity.uncommon)
            {
                itemChance newItem = new itemChance(a, u);
                allArmor.Add(newItem);
            }
            if (a.rarity == Item.itemRarity.rare)
            {
                itemChance newItem = new itemChance(a, r);
                allArmor.Add(newItem);
            }
            if (a.rarity == Item.itemRarity.epic)
            {
                itemChance newItem = new itemChance(a, e);
                allArmor.Add(newItem);
            }
        }
        int totalWeight = 0;
        foreach (itemChance s in allArmor)
        {
            totalWeight += s.chance;
        }
        GameObject inventory = GameObject.FindWithTag("Inventory");
        Armor armorToReturn = null;
        if (inventory != null)
        { 
            while (inventory.GetComponent<PlayerInventory>().InventoryContains(armorToReturn))
            {
                int rand = Random.Range(0, totalWeight);
                int cumulativeWeight = 0;
                foreach (itemChance w in allArmor)
                {
                    cumulativeWeight += w.chance;
                    if (rand < cumulativeWeight)
                    {
                        armorToReturn = (Armor)w.item;
                        break; 
                    }
                }
            } 
            return armorToReturn;
        }
        return (Armor)tempList[0];
    } 
}

 


