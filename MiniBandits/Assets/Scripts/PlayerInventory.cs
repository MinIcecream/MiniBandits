using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInventory : MonoBehaviour
{
    public delegate void UpdateInventory();
    public static event UpdateInventory OnInventoryUpdate;

    public GameObject UIPanel;

    public string[] inventoryItems= new string[5];
    public InventorySlot[] slots = new InventorySlot[5];

    public List<GameObject> buttons = new List<GameObject>();

    void Awake()
    {
        UpdateInventorySlots();
    }
    public bool EmptySlot()
    {
        for(int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i] == "")
            {
                return true;
            }
        }
        return false;
    }
    public void AddItemToInventory(string itemName)
    {
        int emptySlot = -1;

        //GET FIRST EMPTY SLOT
        for(int i=0;i<inventoryItems.Length;i++)
        {
            if (inventoryItems[i] == "")
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
            inventoryItems[emptySlot] = itemName;
        }

        UpdateInventorySlots();
    }

    public void RemoveItemFromInventory(int indexToRemove)
    {
        if (!InBounds(indexToRemove))
        {
            return;
        }
        inventoryItems[indexToRemove] = "";

        UpdateInventorySlots();
    }
    public void RemoveItemFromInventory(string itemName)
    { 
        int indexToRemove = Array.IndexOf(inventoryItems, itemName);

        if (!InBounds(indexToRemove))
        {
            return;
        }
        inventoryItems[indexToRemove] = "";

        UpdateInventorySlots();
    }

    void UpdateInventorySlots()
    {
        for(int i=0; i<inventoryItems.Length;i++)
        {
            slots[i].UpdateItem(inventoryItems[i]);
        }
        if (OnInventoryUpdate != null)
        {
            OnInventoryUpdate();
        }
    }

    public void AddItemToInventory(string itemName, InventorySlot slot)
    {
        int index = Array.IndexOf(slots, slot);
        inventoryItems[index] = itemName;
        UpdateInventorySlots();
    }
    public void RemoveItemFromInventory(InventorySlot slotToRemove)
    {
        int index = Array.IndexOf(slots, slotToRemove);
        inventoryItems[index] = "";
        UpdateInventorySlots();
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
    public string GetActiveWeapon()
    {
        return inventoryItems[0];
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
