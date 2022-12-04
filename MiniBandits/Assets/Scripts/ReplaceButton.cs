using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceButton : MonoBehaviour
{
    public ItemInteractable item;
    public InventorySlot slot;

    void Awake()
    {
        slot = transform.parent.gameObject.GetComponent<InventorySlot>();
    }
    public void SetItem(ItemInteractable i)
    {
        item = i;
    }
    public void Replace()
    {
        GameObject.FindWithTag("Inventory").GetComponent<PlayerInventory>().DeselectSlots();
        item.Replace(slot);
    }
}
