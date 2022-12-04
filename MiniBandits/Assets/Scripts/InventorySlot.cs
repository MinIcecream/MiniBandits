using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image image;
    public string item;

    public string GetItem()
    {
        return item;
    }

    public void RemoveIcon()
    { 
        image.overrideSprite = null; 
    }
    public void AddIcon()
    {
        if (item != "")
        {
            image.overrideSprite = Resources.Load<Sprite>("WeaponPortraits/" + item);
        }
        else
        {
            image.overrideSprite = null;
        }
    }
    public void UpdateItem(string itemName)
    {
        item = itemName;
        if (item != "")
        { 
            image.overrideSprite = Resources.Load<Sprite>("WeaponPortraits/" + item);
        }
        else
        {
            image.overrideSprite = null;
        }
    }
}
