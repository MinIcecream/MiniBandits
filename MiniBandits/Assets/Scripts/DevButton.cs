using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DevButton : MonoBehaviour
{
    public Item item;
    public void SetItem(Item i)
    {
        item = i;
        Image image = GetComponent<Image>();
        image.sprite = item.sprite; 

        image.preserveAspect = true;
        image.type =Image.Type.Simple;
    }
    public void Equip()
    {
        GameObject inven = GameObject.FindWithTag("Inventory");

        if (!inven)
        {
            Debug.Log("no inven found");
            return;
        }

        inven.GetComponent<PlayerInventory>().AddItemToInventory(item); 
    }
}
