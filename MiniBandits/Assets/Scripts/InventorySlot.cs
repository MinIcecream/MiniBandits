using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image image;
    public Item item;
     
    public Item.itemType acceptedItems;

    public Item GetItem()
    {
        return item;
    }

    public void RemoveIcon()
    { 
        image.overrideSprite = null; 
    }
    public void AddIcon()
    {
        if (item != null)
        { 
             image.overrideSprite = item.sprite; 
        }
        else
        {
            image.overrideSprite = null;
        }
    }
    public void UpdateItem(Item newItem)
    {
        item = newItem;
        if (item != null)
        {
            image.overrideSprite = item.sprite;
        }
        else
        {
            image.overrideSprite = null;
        }
    }
}
