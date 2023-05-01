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
     
    public TextMeshProUGUI itemTitle, itemDescription;

    public GameObject dropButton, equipButton;
    InventorySlot selectedSlot;

    public bool UIOpen;

    public ItemDescriptionCardManager infoManager;

    void Awake()
    {
        UpdateInventorySlots();
    }
     
    public bool AvailableSlot(Item item)
    {
        switch (item.type)
        {
            case Item.itemType.helmet:
                if (activeHelmet == null)
                {
                    return true;
                }
                break;
            case Item.itemType.chestplate:
                if (activeChestplate == null)
                {
                    return true;
                }
                break;
            case Item.itemType.pants:
                if (activePants == null)
                {
                    return true;
                }
                break;
            case Item.itemType.weapon:
                if (activeWeapon == null)
                {
                    return true;
                }
                break;
        }

        for(int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i] == null)
            {
                return true;
            }
        }
        return false;
    }
    //Set item description UI  
    public void SetDescription(InventorySlot slot)
    {
        if (slot.item == null)
        { 
            return; 
        }

        switch (slot.item.type)
        {
            case Item.itemType.helmet: 
                infoManager.DisplayInfo(helmetSlot, slot);
                break;
            case Item.itemType.pants:
                infoManager.DisplayInfo(pantsSlot, slot);
                break;
            case Item.itemType.chestplate:
                infoManager.DisplayInfo(chestplateSlot, slot);
                break;
            case Item.itemType.weapon:
                infoManager.DisplayInfo(weaponSlot, slot);
                break;
        } 
          
    } 

    //If equip slot is null, equip the item. Else, swap the equipped item witht he item to equip.
    public void EquipItem(InventorySlot slotToEquip)
    {
        if (slotToEquip.acceptedItems != Item.itemType.basic)
        {
            return;
        }
        int index = Array.IndexOf(slots, slotToEquip);
        Item itemToEquip = inventoryItems[index];

        if (itemToEquip == null)
        {
            return;
        }

        switch (itemToEquip.type)
        {
            case Item.itemType.helmet:
                if (activeHelmet != null)
                {
                    Item equippedItem = activeHelmet;
                    RemoveItemFromInventory(slotToEquip);
                    AddItemToInventory(equippedItem);
                } 
                SetActiveItem(itemToEquip);
                return;

            case Item.itemType.chestplate:
                if (activeChestplate != null)
                {
                    Item equippedItem = activeChestplate;
                    RemoveItemFromInventory(slotToEquip);
                    AddItemToInventory(equippedItem);
                }
                SetActiveItem(itemToEquip);
                return;

            case Item.itemType.pants:
                if (activePants != null)
                {
                    Item equippedItem = activePants;
                    RemoveItemFromInventory(slotToEquip);
                    AddItemToInventory(equippedItem);
                }
                SetActiveItem(itemToEquip);
                return;

            case Item.itemType.weapon:
                if (activeWeapon != null)
                {
                    Item equippedItem = activeWeapon;
                    RemoveItemFromInventory(slotToEquip);
                    AddItemToInventory(equippedItem);
                }
                SetActiveItem(itemToEquip);
                return;
        }
        Debug.Log("NO ACTIVE SLOT OF TYPE " + itemToEquip.type);
    }

    //Equips the item to the corresponding active slot
    public void SetActiveItem(Item itemToEquip)
    { 
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

    //returns true if inventory contains the item
    public bool InventoryContains(Item item)
    {
        if (item == null)
        {
            return true;
        }
        foreach(Item i in inventoryItems)
        {
            if (item == i)
            {
                return true;
            }
        }
        switch (item.type)
        {
            case Item.itemType.helmet:
                if (activeHelmet == item)
                {
                    return true;
                }
                break;
            case Item.itemType.pants:
                if (activePants == item)
                {
                    return true;
                }
                break;
            case Item.itemType.chestplate:
                if (activeChestplate == item)
                {
                    return true;
                }
                break;
            case Item.itemType.weapon:
                if (activeWeapon == item)
                {
                    return true;
                }
                break;
        }
        return false;
    }
    //Checks if there is an open equip slot for an item
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

    //Adds Item to inventory. If there's an open equip slot, equiops it. Else, just puts it in inventory.
    public void AddItemToInventory(Item itemToAdd)
    {
        if (itemToAdd == null)
        {
            return;
        }
        //IF THER"S CURRENTLY NONE OF THAT ITEM EQUIPPED, EQUIP IT.
        if (ActiveSlotAvailable(itemToAdd.type))
        {
            SetActiveItem(itemToAdd);
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

    //Deletes item from inventory,
    public void RemoveItemFromInventory(InventorySlot slotToRemove)
    {
        switch (slotToRemove.acceptedItems)
        {
            case Item.itemType.helmet:
                activeHelmet = null;
                UpdateInventorySlots();
                return;

            case Item.itemType.chestplate:
                activeChestplate = null;
                UpdateInventorySlots();
                return;

            case Item.itemType.pants:
                activePants = null;
                UpdateInventorySlots();
                return;

            case Item.itemType.weapon:
                activeWeapon = null;
                UpdateInventorySlots();
                return;

            case Item.itemType.basic: 
                int index = Array.IndexOf(slots, slotToRemove);
                inventoryItems[index] = null;
                UpdateInventorySlots();
                return;
        } 
    }

    //Deletes and spawns interactable item on the floor.
    public void DropItem(InventorySlot slotToDrop)
    {
        if (slotToDrop.item == null)
        {
            return;
        }
        
        Item itemToDrop = slotToDrop.item;
        Debug.Log(itemToDrop);
        RemoveItemFromInventory(slotToDrop);

        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) 
        {
            return;
        }

        var itemDrop=Instantiate(Resources.Load<GameObject>("Misc/ItemDrop"), player.transform.position, Quaternion.identity);
        itemDrop.GetComponent<ItemDrop>().item = itemToDrop;
    } 

    public void DropSelectedItem()
    { 
        DropItem(selectedSlot);
    }
    public void EquipSelectedItem()
    {
        EquipItem(selectedSlot);
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
       
     
    public Item GetActiveWeapon()
    {
        return activeWeapon;
    }
     
    public void CloseInventory()
    {  
        UIPanel.SetActive(false); 
        UIOpen=false;
        foreach (InventorySlot slot in slots)
        {
            slot.reference = null;
        }
        helmetSlot.reference = null;
        chestplateSlot.reference = null;
        pantsSlot.reference = null;
        weaponSlot.reference = null;
    } 
    public void OpenInventory()
    {  
        UIPanel.SetActive(true);
        UIOpen = true; 
    }
    public void OpenInventory(ISelectFromInventory reference)
    { 
        UIPanel.SetActive(true);
        UIOpen = true;
        foreach (InventorySlot slot in slots)
        {
            slot.reference = reference;
        }
        helmetSlot.reference = reference;
        chestplateSlot.reference = reference;
        pantsSlot.reference = reference;
        weaponSlot.reference = reference;
    }
}
