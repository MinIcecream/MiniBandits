using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractable : MonoBehaviour
{
    public GameObject popup;
    public Item item; 

    InventorySlot selectedSlot;

    void Awake()
    {
        UpdateItem();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        popup.SetActive(true);
        popup.transform.position = new Vector2(transform.position.x, transform.position.y + 1) ;
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        popup.SetActive(false); 
    }

    void Update()
    {
        if (GameObject.FindWithTag("Inventory") == null)
        {
            return;
        }
        if (popup.activeSelf && Input.GetKeyDown("e"))
        {
            PlayerInventory inventory = GameObject.FindWithTag("Inventory").GetComponent<PlayerInventory>(); 

            if (inventory.EmptySlot())
            {
                inventory.AddItemToInventory(item);
                Destroy(gameObject);
            }
            else
            {
                inventory.ShowUI();
                inventory.SelectSlot(this);
            }
        } 
    }  
     
    public void Replace(InventorySlot slot)
    {
        Item itemToReplace = slot.GetItem();
        GameObject.FindWithTag("Inventory").GetComponent<PlayerInventory>().AddItemToInventory(item, slot);
        item = itemToReplace;

        GameObject.FindWithTag("Inventory").GetComponent<PlayerInventory>().HideUI();
        UpdateItem();
    }

    public void UpdateItem()
    {
        if (item != null)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("WeaponPortraits/" + item);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateItem(Item newItem)
    {
        item = newItem;

        if (item != null)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("WeaponPortraits/" + item);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
