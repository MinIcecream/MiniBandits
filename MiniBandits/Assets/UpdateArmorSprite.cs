using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateArmorSprite : MonoBehaviour
{
    public PlayerInventory inven;
    public Transform spawnPt; 

    public SpriteRenderer helmetSprite, chestplateSprite, pantsSprite;

    void Awake()
    {
        inven = GameObject.FindWithTag("Inventory").GetComponent<PlayerInventory>();
    }
    void OnEnable()
    {
        PlayerInventory.OnInventoryUpdate += UpdateWeapon;
    }

    void OnDisable()
    {
        PlayerInventory.OnInventoryUpdate -= UpdateWeapon;
    }

    void UpdateWeapon()
    {
        Item helmet = inven.activeHelmet;
        Item chestplate = inven.activeChestplate;
        Item pants = inven.activePants;

        if (helmet)
        {
            helmetSprite.sprite = helmet.sprite;
        }
        else
        {
            helmetSprite.sprite = null;
        }
        if (chestplate)
        {
            chestplateSprite.sprite = null;
        }
        if (pants)
        {
            pantsSprite.sprite = null;
        }
    }
}
