using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceButton : MonoBehaviour 
{
    public ISelectFromInventory item;
    public InventorySlot slot;

    void Awake()
    {
        slot = transform.parent.gameObject.GetComponent<InventorySlot>();
    }
    public void SetItem(ISelectFromInventory i)
    {
        item = i;
    }
    public void Replace()
    {
        GameObject.FindWithTag("Inventory").GetComponent<PlayerInventory>().DeselectSlots();
        item.SelectInventoryItem(slot);
    }
}
