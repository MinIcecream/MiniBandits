using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MarketItem :Interactable
{ 
    public Item item;
    bool used;
    public SpriteRenderer backgroundColor; 
    
    public override void ActivatePopup()
    { 
        if (!used)
        {
            base.ActivatePopup();
        }
    } 
    void Update()
    {
        if (item == null)
        {
            return;
        }
        //Initalizing vars
        popup.GetComponent<TextMeshPro>().text = "[E] to buy "+item.displayName+" for " + item.cost + " gold";
        GetComponent<SpriteRenderer>().sprite = item.sprite;
        Color color = item.color;
        color.a = 0.3f;
        backgroundColor.color = color;  
    }

    public override void Interact()
    {
        if (GameObject.FindWithTag("Player") == null)
        {
            return;
        }
        if (GameObject.FindWithTag("Player").GetComponent<GoldManager>().GetGold() >= item.cost)
        {
            GameObject.FindWithTag("Player").GetComponent<GoldManager>().SpendGold(item.cost);
            var newItem = Instantiate(Resources.Load<GameObject>("Misc/ItemDrop"), transform.position, Quaternion.identity);
            newItem.GetComponent<ItemDrop>().item = item;
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("YOU BROKE LOL");
        }
    }
}
