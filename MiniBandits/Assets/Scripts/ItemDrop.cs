using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class ItemDrop : MonoBehaviour
{
    public SpriteRenderer backgroundColor;

    public GameObject popup;
    public Item item; 

    InventorySlot selectedSlot;

    public TextMeshProUGUI tmp;

    void Awake()
    { 
        StartCoroutine("SetInitialSprite");
    }
    IEnumerator SetInitialSprite()
    {
        yield return new WaitForSeconds(0.01f);
        UpdateItem();
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag != "Player")
        {
            return;
        }
        popup.SetActive(true);
        popup.transform.position = new Vector2(transform.position.x, transform.position.y + 1);
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
        if (GameObject.FindWithTag("Inventory") == null)
        {
            return;
        }
        if (popup.activeSelf && Input.GetKeyDown("e"))
        {
            PlayerInventory inventory = GameObject.FindWithTag("Inventory").GetComponent<PlayerInventory>();

               if (inventory.AvailableSlot(item))
               {
                   inventory.AddItemToInventory(item);
                   Destroy(gameObject);
               } 
        }
    } 
    public void UpdateItem()
    { 
        if (item != null)
        { 
            Color color = item.color;
            color.a = 0.3f;
            backgroundColor.color = color;
           
            tmp.text =item.name + "\n[E to interact]";
            GetComponent<SpriteRenderer>().sprite = item.sprite;
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
            Color color = item.color;
            color.a = 0.3f;
            backgroundColor.color = color;
            if (item.type == Item.itemType.helmet || item.type == Item.itemType.chestplate || item.type == Item.itemType.pants)
            {
                Sprite[] armorIconsAtlas = Resources.LoadAll<Sprite>("ArmorPortraits");
                // Get specific sprite
                Sprite armorSprite = armorIconsAtlas.Single(s => s.name == item.name);
                GetComponent<SpriteRenderer>().sprite = armorSprite;
            }
            else if (item.type == Item.itemType.weapon)
            {
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("WeaponPortraits/" + item.name);
            }
        }
        else
        { 
            Destroy(gameObject);
        }
    }
}
