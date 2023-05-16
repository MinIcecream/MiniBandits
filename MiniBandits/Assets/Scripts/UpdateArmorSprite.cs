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



        //IF HELMET IS EQUIPPED:
        if (helmet)
        {
            string helmetType = helmet.referenceName.Replace(" ", "");

            //IF THAT ARMOR HAS A SCRIPT:
            if (Type.GetType(helmetType) != null)
            {
                //IF IT'S NOT EQUIPPED, DESTROY CURRENT SCRIPT AND ADD THE NEW ONE
                if (gameObject.GetComponent(Type.GetType(helmetType)) == null)
                {
                    Destroy(helmetScript);
                    helmetScript = (ArmorTemplate)gameObject.AddComponent(Type.GetType(helmetType));
                }
            }
            else
            {
                Destroy(helmetScript);
            }
            helmetSprite.sprite = helmet.sprite;
        }
        else
        {
            helmetSprite.sprite = head;
            Destroy(helmetScript);
        }



        //IF CHESTPLATE IS EQUIPPED:
        if (chestplate)
        {
            string typeName = chestplate.referenceName.Replace(" ", "");

            //IF THAT ARMOR HAS A SCRIPT:
            if (Type.GetType(typeName) != null)
            { 
                //IF IT'S NOT EQUIPPED, DESTROY CURRENT SCRIPT AND ADD THE NEW ONE
                if (gameObject.GetComponent(Type.GetType(typeName)) == null)
                { 
                    Destroy(chestplateScript);
                    chestplateScript = (ArmorTemplate)gameObject.AddComponent(Type.GetType(typeName));
                }
            }
            else
            { 
                Destroy(chestplateScript);
            }
            chestplateSprite.sprite = chestplate.sprite;
        } 
        else
        {
            chestplateSprite.sprite = body;
            Destroy(chestplateScript);
        }





        //IF PANTS IS EQUIPPED:
        if (pants)
        {
            string pantsType = pants.referenceName.Replace(" ", "");

            //IF THAT ARMOR HAS A SCRIPT:
            if (Type.GetType(pantsType) != null)
            {
                //IF IT'S NOT EQUIPPED, DESTROY CURRENT SCRIPT AND ADD THE NEW ONE
                if (gameObject.GetComponent(Type.GetType(pantsType)) == null)
                {
                    Destroy(pantsScript);
                    pantsScript = (ArmorTemplate)gameObject.AddComponent(Type.GetType(pantsType));
                }
            }
            else
            {
                Destroy(pantsScript);
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
