using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shopkeep : MonoBehaviour
{
    public Transform[] itemPos;

    public List<GameObject> itemDrops = new List<GameObject>();

    public GameObject itemPrefab,popup;

    PlayerInventory inven;

    void Start()
    {
        inven = GameObject.FindWithTag("Inventory").GetComponent<PlayerInventory>(); 
        popup.GetComponent<TextMeshPro>().text = "10 gold to refresh shop";
        SetItems();
    }
    void OnTriggerEnter2D(Collider2D coll)
    { 
        if (coll.gameObject.tag != "Player")
        {
            return;
        }
        popup.SetActive(true);
        popup.transform.position = new Vector2(transform.position.x, transform.position.y + 1.5f);
        
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag != "Player")
        {
            return;
        }
        popup.SetActive(false);
    }
    void Update()
    {  
        if (popup.activeSelf && Input.GetKeyDown("e"))
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
                newItem = RoomOptionGenerator.GenerateRandomArmor();
            }
            else
            {
                newItem = RoomOptionGenerator.GenerateRandomWeapon();
            }

            //if this item is already in the list or in the player's inventory, regenerate it.
            while (itemsToReturn.Contains(newItem) || inven.InventoryContains(newItem))
            {
                if (Random.Range(0, 3) < 2)
                {
                    newItem = RoomOptionGenerator.GenerateRandomArmor();
                }
                else
                {
                    newItem = RoomOptionGenerator.GenerateRandomWeapon();
                }
            }
            itemsToReturn.Add(newItem);
        }

        return itemsToReturn;
    }
}
