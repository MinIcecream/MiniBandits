using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BlacksmithUpgrade : Interactable,ISelectFromInventory
{ 
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
    }
    public override void Interact()
    {
        if (GameObject.FindWithTag("Player") == null)
        {
            return;
        }
        PlayerInventory inventory = GameObject.FindWithTag("Inventory").GetComponent<PlayerInventory>();

        canvas.SetActive(true);
        inventory.OpenInventory(this);
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
        if (slot==null || slot.GetItem() == null)
        {
            return;
        }

        Item upgradedItem;
        if (slot.item.type == Item.itemType.weapon)
        {
            upgradedItem = Resources.Load<Item>("Items/Weapons/Upgrades/" + slot.item.referenceName + "+");
        }
        else
        {
            upgradedItem = Resources.Load<Item>("Items/Armor/Upgrades/" + slot.item.referenceName + "+");
        }
        Debug.Log("Items/Weapons/Upgrades/" + slot.item.referenceName + "+");
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
