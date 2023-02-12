using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BlacksmithUpgrade : MonoBehaviour,ISelectFromInventory
{
    [SerializeField]
    GameObject popup;

    [HideInInspector]
    public Item item;

    [SerializeField]
    GameObject canvas;

    [SerializeField]
    TextMeshProUGUI text;

    [SerializeField]
    Image image;

    bool used; 
     

    public Item testItem;
    
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (!used)
        {
            if (coll.gameObject.tag != "Player")
            {
                return;
            }
            popup.SetActive(true); 
        }
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
        if (canvas.activeSelf)
        {
            if (GameObject.FindWithTag("Inventory").GetComponent<PlayerInventory>().UIOpen == false)
            {
                Reset();
                canvas.SetActive(false);
            }
        }
        if (popup.activeSelf && Input.GetKeyDown("e"))
        {
            if (GameObject.FindWithTag("Player") == null)
            {
                return;
            }
            PlayerInventory inventory = GameObject.FindWithTag("Inventory").GetComponent<PlayerInventory>();

            canvas.SetActive(true);
            inventory.ShowUI();
            inventory.SelectSlot(this);
        } 
    }
    public void SelectInventoryItem(InventorySlot s)
    { 
        text.text = "Upgrade "+ s.item.displayName+" for "+s.item.cost;
        item = s.item;
        image.sprite = s.item.sprite;
    }
    public void UpgradeItem()
    { 
        if (item == null)
        {
            return;
        }

        Item upgradedItem;
        if (item.type == Item.itemType.weapon)
        {
            upgradedItem = Resources.Load<Item>("Items/Weapons/" + item.name + "+");
        }
        else
        {
            upgradedItem = Resources.Load<Item>("Items/Armor/" + item.name + "+");
        }
        Debug.Log(item.referenceName + "+");
        if (upgradedItem == null)
        { 
            Debug.Log("No more upgrades!");
            return;
        }
        else
        {
            Debug.Log(upgradedItem);
        }

        if (GameObject.FindWithTag("Player").GetComponent<GoldManager>().GetGold() <= item.cost)
        {
             
            GameObject.FindWithTag("Player").GetComponent<GoldManager>().SpendGold(item.cost); 
             
          
             GameObject.FindWithTag("Inventory").GetComponent<PlayerInventory>().ReplaceItem(item, upgradedItem);
             Reset();
        }
        else
        {
            Debug.Log("U BROKE LOLLL");
        }
    }
    void Reset()
    {
        item = null; 
        text.text = "Select Item to Upgrade";
        image.sprite = null;
    }
}
