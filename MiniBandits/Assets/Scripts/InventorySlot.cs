using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour 
{
    public Image image;
    public Item item;
     
    public Item.itemType acceptedItems;

    PlayerInventory inven;

    public ISelectFromInventory reference;

    public Sprite backgroundImage;

    void Awake()
    {
        inven = GameObject.FindWithTag("Inventory").GetComponent<PlayerInventory>();
    }
    public Item GetItem()
    { 
        return item;
    }

    public void RemoveIcon()
    { 
        image.overrideSprite = backgroundImage; 
    }
    public void AddIcon()
    {
        if (item != null)
        { 
            image.overrideSprite = item.sprite;
            image.preserveAspect = true; 
        }
        else
        {
            image.overrideSprite = backgroundImage; 
        }
    }
    public void UpdateItem(Item newItem)
    {
        item = newItem;
        if (item != null)
        { 
            image.overrideSprite = item.sprite;
            image.preserveAspect=true; 
        }
        else
        {
            image.overrideSprite = backgroundImage; 
        }
    } 
    public void OnClick()
    {
        if (item == null)
        {
            return;
        }

        if (reference != null)
        { 
            reference.SelectInventoryItem(this); 
        }
        else
        {
            inven.SetDescription(this); 
        } 
    }
     
}
