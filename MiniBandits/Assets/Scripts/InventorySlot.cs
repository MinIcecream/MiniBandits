using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler
{
    public Image image;
    public Item item;
     
    public Item.itemType acceptedItems;

    PlayerInventory inven;

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
        image.overrideSprite = null; 
    }
    public void AddIcon()
    {
        if (item != null)
        { 
            image.overrideSprite = item.sprite;
            image.preserveAspect = true ;
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
            image.preserveAspect=true;
        }
        else
        {
            image.overrideSprite = null;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        inven.SetDescription(item);
    }
}
