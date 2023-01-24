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
        UpdateArmor();
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
            if (Type.GetType(helmet.name.Replace(" ", "")) != null)
            { 
                if(gameObject.GetComponent(Type.GetType(helmet.name.Replace(" ", "")))==null)
                { 
                    helmetScript = (ArmorTemplate)gameObject.AddComponent(Type.GetType(helmet.name.Replace(" ", "")));
                } 
            }
            helmetSprite.sprite = helmet.sprite;
        }
        else
        {
            helmetSprite.sprite = head;
            Destroy(helmetScript);
        }
        if (chestplate)
        {
            if (Type.GetType(chestplate.name.Replace(" ", "")) != null)
            {
                if (gameObject.GetComponent(Type.GetType(chestplate.name.Replace(" ", ""))) == null)
                {
                    chestplateScript = (ArmorTemplate)gameObject.AddComponent(Type.GetType(chestplate.name.Replace(" ", "")));
                } 
            } 
            chestplateSprite.sprite = chestplate.sprite;
        }
        else
        {
            chestplateSprite.sprite = body;
            Destroy(chestplateScript);
        }
        if (pants)
        {
            if (Type.GetType(pants.name.Replace(" ", "")) !=null)
            {
                if (gameObject.GetComponent(Type.GetType(pants.name.Replace(" ", ""))) == null)
                {
                    pantsScript = (ArmorTemplate)gameObject.AddComponent(Type.GetType(pants.name.Replace(" ", "")));
                } 
            } 
            pantsSprite.sprite = pants.sprite;
        }
        else
        {
            pantsSprite.sprite = legs;
            Destroy(pantsScript);
        }
    }
}
