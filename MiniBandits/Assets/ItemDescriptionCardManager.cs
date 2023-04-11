using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDescriptionCardManager : MonoBehaviour
{
    public PlayerInventory inven;

    public GameObject equipCard;
    public GameObject selectedCard;

    InventorySlot activeSlot, inactiveSlot;

    public void DisplayInfo(InventorySlot equipSlot, InventorySlot unequipSlot)
    {
        activeSlot = equipSlot;
        inactiveSlot = unequipSlot;

        Item unequipedItem = inactiveSlot.item;
        Item activeItem = activeSlot.item;

        if (activeSlot==inactiveSlot)
        {
            equipCard.SetActive(true);
            equipCard.GetComponent<ItemInfoCard>().DisplayStats(activeItem);
        }
        else if (activeItem == null)
        { 
            selectedCard.SetActive(true);
            selectedCard.GetComponent<ItemInfoCard>().DisplayStats(unequipedItem);
        }
        else
        {
            selectedCard.SetActive(true);
            equipCard.SetActive(true);  
            equipCard.GetComponent<ItemInfoCard>().DisplayStats(activeItem); 
            selectedCard.GetComponent<ItemInfoCard>().DisplayComparedStats(unequipedItem,activeItem);
        }
    }

    public void DestroyInfoCards()
    {
        equipCard.SetActive(false);
        selectedCard.SetActive(false);
    }

    public void DropItem()
    {
        inven.DropItem(inactiveSlot);
        DestroyInfoCards();
    }
    public void EquipItem()
    {
        Item temp = activeSlot.item;
        Item temp2 = inactiveSlot.item;

        inven.RemoveItemFromInventory(inactiveSlot);
        inven.SetActiveItem(temp2);
        inven.AddItemToInventory(temp);
        DestroyInfoCards();
    }
}
