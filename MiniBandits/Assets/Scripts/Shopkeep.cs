using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shopkeep : Interactable
{
    public Transform[] itemPos;

    public List<GameObject> itemDrops = new List<GameObject>();

    public GameObject itemPrefab;

    PlayerInventory inven;

    List<Item> previouslySoldItems = new List<Item>();
    void Start()
    {
        inven = GameObject.FindWithTag("Inventory").GetComponent<PlayerInventory>(); 
        popup.GetComponent<TextMeshPro>().text = "10 gold to refresh shop";
        SetItems();
    }  
    public override void Interact()
    {   
        if (GameObject.FindWithTag("Player") == null)
        {
            return;
        }
        if (GameObject.FindWithTag("Player").GetComponent<GoldManager>().GetGold() >= 10)
        {
            GameObject.FindWithTag("Player").GetComponent<GoldManager>().SpendGold(10);

            SetItems();
        }
        else
        {
            Debug.Log("YOU BROKE LOL");
        }
         
    }
    void SetItems()
    { 
        List<Item> itemList = GenerateNewItems(3);

        foreach (GameObject item in itemDrops)
        {
            if (item != null)
            {
                Destroy(item);
            } 
        }
        
        for (int i = 0; i < 3; i++)
        {
            GameObject newItem = Instantiate(itemPrefab, itemPos[i].position, Quaternion.identity);
            newItem.GetComponent<MarketItem>().item = itemList[i];
            itemDrops.Add(newItem);
            previouslySoldItems.Add(itemList[i]);
        }
    }
    List<Item> GenerateNewItems(int num)
    {
        List<Item> itemsToReturn = new List<Item>();

        for (int i = 0; i < num; i++)
        {
            Item newItem = null;

            if (Random.Range(0, 3) < 2)
            {
                newItem = RoomOptionGenerator.GenerateRandomArmor(3,15,3,1);
            }
            else
            {
                newItem = RoomOptionGenerator.GenerateRandomWeapon(3, 15, 3, 1);
            } 

            bool previouslySold = false;
            for (int m = previouslySoldItems.Count - 1; m > previouslySoldItems.Count - 7; m--)
            {
                if (m < 0)
                {
                    break;
                }
                if (previouslySoldItems[m] == newItem)
                {
                    previouslySold = true;
                    break;
                }
            }
            //if this item is already in the list or in the player's inventory, regenerate it.
            while (itemsToReturn.Contains(newItem) || inven.InventoryContains(newItem) || previouslySold)
            {
                if (Random.Range(0, 3) < 2)
                {
                    newItem = RoomOptionGenerator.GenerateRandomArmor(3, 15, 3, 1);
                }
                else
                {
                    newItem = RoomOptionGenerator.GenerateRandomWeapon(3,15,3,1);
                } 
                for (int m = previouslySoldItems.Count - 1; m > previouslySoldItems.Count - 7; m--)
                {
                    if (m < 0)
                    {
                        break;
                    }
                    if (previouslySoldItems[m] == newItem)
                    {
                        previouslySold = true;
                        break;
                    }
                    else
                    { 
                        previouslySold = false;
                    }
                } 
            }
            itemsToReturn.Add(newItem);
        }

        return itemsToReturn;
    }
}
