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
    public InventorySlot slot;

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
            inventory.OpenInventory(this); 
        } 
    }
    public void SelectInventoryItem(InventorySlot s)
    {
        Debug.Log(s);
        text.text = "Upgrade "+ s.item.displayName+" for "+s.item.cost;
        slot = s;
        image.sprite = s.item.sprite;
    }
    public void UpgradeItem()
    { 
        if (slot.item == null)
        {
            return;
        }

        Item upgradedItem;
        if (slot.item.type == Item.itemType.weapon)
        {
            upgradedItem = Resources.Load<Item>("Items/Weapons/" + slot.item.name + "+");
        }
        else
        {
            upgradedItem = Resources.Load<Item>("Items/Armor/" + slot.item.name + "+");
        }
        Debug.Log(slot.item.referenceName + "+");
        if (upgradedItem == null)
        { 
            Debug.Log("No more upgrades!");
            return;
        }
        else
        {
            Debug.Log(upgradedItem);
        }

        GoldManager goldMan = GameObject.FindWithTag("Player").GetComponent<GoldManager>();
        if (goldMan.GetGold() >= slot.item.cost)
        { 
            goldMan.SpendGold(slot.item.cost); 
              
             GameObject.FindWithTag("Inventory").GetComponent<PlayerInventory>().RemoveItemFromInventory(slot);
             GameObject.FindWithTag("Inventory").GetComponent<PlayerInventory>().AddItemToInventory(upgradedItem);

              Reset();
        }
        else
        {
            Debug.Log("U BROKE LOLLL");
        }
    }
    void Reset()
    {
        slot = null; 
        text.text = "Select Item to Upgrade";
        image.sprite = null;
    }
}
