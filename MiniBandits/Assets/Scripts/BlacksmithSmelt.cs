using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BlacksmithSmelt : MonoBehaviour
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
        popup.GetComponent<TextMeshPro>().text = "[E] to buy " + item.name + " for " + item.cost + " gold";
        GetComponent<SpriteRenderer>().sprite = item.sprite;
        if (popup.activeSelf && Input.GetKeyDown("e"))
        {
            if (GameObject.FindWithTag("Player") == null)
            {
                return;
            }
            if (GameObject.FindWithTag("Player").GetComponent<GoldManager>().GetGold() >= item.cost)
            {
                GameObject.FindWithTag("Player").GetComponent<GoldManager>().SpendGold(item.cost);
                var newItem = Instantiate(Resources.Load<GameObject>("Misc/ItemInteractable"), transform.position, Quaternion.identity);
                newItem.GetComponent<ItemInteractable>().item = item;
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("YOU BROKE LOL");
            }
        }
    }
}
