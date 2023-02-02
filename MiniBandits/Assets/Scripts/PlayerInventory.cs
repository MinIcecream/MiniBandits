using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public delegate void UpdateInventory();
    public static event UpdateInventory OnInventoryUpdate;

    public GameObject UIPanel;

    public Item[] inventoryItems= new Item[5];
    public Item activeHelmet, activeChestplate, activePants, activeWeapon;

    public InventorySlot[] slots = new InventorySlot[5];
    public InventorySlot helmetSlot, chestplateSlot, pantsSlot,weaponSlot;

    public List<GameObject> buttons = new List<GameObject>();

    public TextMeshProUGUI itemTitle, itemDescription;

    void Awake()
    {
        UpdateInventorySlots();
    }

    //Returns whether there's an empty inventory slot
    public bool EmptySlot()
    {
        for(int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i] == null)
            {
                return true;
            }
        }
        return false;
    }
    public void SetDescription(Item item)
    {
        if (item == null)
        {
            return;
        }
        itemTitle.text = item.name;
        itemDescription.text = item.description;
    }

    //Equips the item to the corresponding active slot
    public void EquipItem(Item itemToEquip)
    {
        Debug.Log("SUP");
        switch (itemToEquip.type)
        {
            case Item.itemType.helmet:
                activeHelmet = itemToEquip;
                UpdateInventorySlots();
                return;


            case Item.itemType.chestplate:
                activeChestplate = itemToEquip;
                UpdateInventorySlots();
                return;


            case Item.itemType.pants:
                activePants = itemToEquip;
                UpdateInventorySlots();
                return;


            case Item.itemType.weapon:
                activeWeapon = itemToEquip;
                UpdateInventorySlots();
                return;
        } 
        Debug.Log("NO ACTIVE SLOT OF TYPE " + itemToEquip.type);
    }
    public bool ActiveSlotAvailable(Item.itemType type)
    {
        switch (type)
        {
            case Item.itemType.helmet:
                if(activeHelmet!= null)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            case Item.itemType.chestplate:
                if (activeChestplate != null)
                {
                    return false;
                }
                else
                {
                    return true;
                }


            case Item.itemType.pants:
                if (activePants != null)
                {
                    return false;
                }
                else
                {
                    return true;
                }


            case Item.itemType.weapon:
                if (activeWeapon != null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
        }
        
        Debug.Log("NO ACTIVE SLOT OF TYPE " + type);
        return false;
    }

    //IF ITEM IS NULL, REMOVE ITEM FROM THE SLOT. OTHERWISE, ADD THE ITEM TO THE SLOT.
    public void AddItemToInventory(Item itemToAdd, InventorySlot slot)
    {
        if (itemToAdd == null)
        {
            switch (slot.acceptedItems)
            {
                case Item.itemType.helmet:
                    activeHelmet = null;
                    return;
                case Item.itemType.chestplate:
                    activeChestplate = null;
                    return;
                case Item.itemType.pants:
                    activePants = null;
                    return;
                case Item.itemType.weapon:
                    activeWeapon = null;
                    return;
                case Item.itemType.basic:
                    int index = Array.IndexOf(slots, slot);
                    inventoryItems[index] = null;
                    return;
            }
        }
        //Check if its a basic inventory slot
        if (slot.acceptedItems == Item.itemType.basic)
        {
            int index = Array.IndexOf(slots, slot);
            inventoryItems[index] = itemToAdd;
        } 
         
        //Otherwise, if the slot accepts the item
        else if (slot.acceptedItems == itemToAdd.type)
        {
            EquipItem(itemToAdd);
        }

        //Otherwise, the item is incompatible with the slot.
        else
        {
            Debug.Log("INCOMPATIBLE!");
        }

        UpdateInventorySlots();
    }
    public void AddItemToInventory(Item itemToAdd)
    {
        if (itemToAdd == null)
        {
            return;
        }
        //IF THER"S CURRENTLY NONE OF THAT ITEM EQUIPPED, EQUIP IT.
        if (ActiveSlotAvailable(itemToAdd.type))
        {
            EquipItem(itemToAdd);
        }

        //OTHERWISE JUST ADD IT TO A BASIC SLOT
        else
        {
            int emptySlot = -1;

            //GET FIRST EMPTY SLOT
            for (int i = 0; i < inventoryItems.Length; i++)
            {
                if (inventoryItems[i] == null)
                {
                    emptySlot = i;
                    break;
                }
            }

            //IF THERES AN EMPTY SLOT, TELL PLAYER. OTHERWISE, ADD TO IVNENTORY
            if (emptySlot == -1)
            {
                Debug.Log("INventory full bruh");
            }
            else
            {
                inventoryItems[emptySlot] = itemToAdd;
            }
        }

        UpdateInventorySlots();
    }
       

    public void RemoveItemFromInventory(InventorySlot slotToRemove)
    {
        switch (slotToRemove.acceptedItems)
        {
            case Item.itemType.helmet:
                activeHelmet = null;
                return;

            case Item.itemType.chestplate:
                activeChestplate = null;
                return;

            case Item.itemType.pants:
                activePants = null;
                return;

            case Item.itemType.weapon:
                activeWeapon = null;
                return;

            case Item.itemType.basic: 
                int index = Array.IndexOf(slots, slotToRemove);
                inventoryItems[index] = null;
                UpdateInventorySlots();
                return;
        } 
    }

    void UpdateInventorySlots()
    {
        //Update active slots
        helmetSlot.UpdateItem(activeHelmet);
        chestplateSlot.UpdateItem(activeChestplate);
        pantsSlot.UpdateItem(activePants);
        weaponSlot.UpdateItem(activeWeapon);

        //update basic slots
        for (int i=0; i<inventoryItems.Length;i++)
        { 
            slots[i].UpdateItem(inventoryItems[i]);
        }

        //DEALS WITH THE UPDATEINVENTORY EVENT THAT OTHER SCRIPTS SUBSCRIBE TO
        if (OnInventoryUpdate != null)
        {
            OnInventoryUpdate();
        }
    }
     
    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideUI();
        }
    }

    public void SelectSlot(ItemInteractable i)
    { 
        foreach (GameObject button in buttons)
        {
            button.SetActive(true);
            button.GetComponent<ReplaceButton>().SetItem(i); 
        }
    }

    public void DeselectSlots()
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(false);
        }
    }
    public Item GetActiveWeapon()
    {
        return activeWeapon;
    }

    bool InBounds(int index)
    {
        return (index >= 0 && index < inventoryItems.Length);
    } 
    public void HideUI()
    {
        DeselectSlots();
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().canMove = true;
        UIPanel.SetActive(false);
    }
    public void ShowUI()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().canMove = false;
        UIPanel.SetActive(true);
    }
}
