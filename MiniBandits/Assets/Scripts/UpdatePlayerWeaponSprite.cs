using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePlayerWeaponSprite : MonoBehaviour
{
    PlayerInventory inventory;
    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindWithTag("Inventory").GetComponent<PlayerInventory>();
        sprite = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (inventory == null)
        {
            return;
        }

        sprite.sprite = Resources.Load<Sprite>("WeaponPortraits/" + inventory.GetActiveWeapon()); 
    }
}
