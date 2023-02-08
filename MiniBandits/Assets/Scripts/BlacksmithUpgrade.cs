using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BlacksmithUpgrade : MonoBehaviour,ISelectFromInventory
{
    [SerializeField]
    GameObject popup;
    public Item item;
    bool used;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (!used)
        {
            if (coll.gameObject.tag != "Player")
            {
                return;
            }
            popup.SetActive(true);
            popup.transform.position = new Vector2(transform.position.x, transform.position.y + 1);
        }
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag != "Player")
        {
            return;
        }
        popup.SetActive(false);
    }

    void Update()
    {  
        if (popup.activeSelf && Input.GetKeyDown("e"))
        {
            if (GameObject.FindWithTag("Player") == null)
            {
                return;
            }
            PlayerInventory inventory = GameObject.FindWithTag("Inventory").GetComponent<PlayerInventory>();


            inventory.ShowUI();
            inventory.SelectSlot(this);
        } 
    }
    public void SelectInventoryItem(InventorySlot slot)
    {
        Debug.Log(slot.item);
    }
}
