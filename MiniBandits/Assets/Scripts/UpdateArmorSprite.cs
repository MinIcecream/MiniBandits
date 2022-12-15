using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class UpdateArmorSprite : MonoBehaviour
{
    public PlayerInventory inven;
    public Transform spawnPt;

    public Sprite head, body, legs;

    public SpriteRenderer helmetSprite, chestplateSprite, pantsSprite;

    public ArmorTemplate helmetScript, chestplateScript, pantsScript;

    void Awake()
    {
        inven = GameObject.FindWithTag("Inventory").GetComponent<PlayerInventory>();
    }
    void OnEnable()
    {
        PlayerInventory.OnInventoryUpdate += UpdateArmor;
    }

    void OnDisable()
    {
        PlayerInventory.OnInventoryUpdate -= UpdateArmor;
    }

    void UpdateArmor()
    {
        Armor helmet = (Armor)inven.activeHelmet;
        Armor chestplate = (Armor)inven.activeChestplate;
        Armor pants = (Armor)inven.activePants;
         

        if (helmet)
        {  
            helmetSprite.sprite = helmet.sprite;  
            helmetScript = (ArmorTemplate)gameObject.AddComponent(Type.GetType(helmet.name));
        }
        else
        {
            helmetSprite.sprite = head;
            Destroy(helmetScript);
        }
        if (chestplate)
        {
            chestplateScript = (ArmorTemplate)gameObject.AddComponent(Type.GetType(chestplate.name));
            chestplateSprite.sprite = chestplate.sprite;
        }
        else
        {
            chestplateSprite.sprite = body;
            Destroy(chestplateScript);
        }
        if (pants)
        {
            pantsScript = (ArmorTemplate)gameObject.AddComponent(Type.GetType(pants.name));
            pantsSprite.sprite = pants.sprite;
        }
        else
        {
            pantsSprite.sprite = legs;
            Destroy(pantsScript);
        }
    }
}
