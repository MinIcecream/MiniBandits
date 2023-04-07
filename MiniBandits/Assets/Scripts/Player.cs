using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Space]
    [Header("Base Stats")]
    [Space]
    //Base Stats
    public int baseLifeSteal;
    public int baseDefense;
    public int baseSpeed;
    public int baseStrength;
    public int baseHealth; 
    public int baseCrit;
    public int baseLuck;


    [Space(10)]
    [Header("Updated Stats")]
    [Space]
    //Stats including buffs
    public int lifeSteal;
    public int defense;
    public int speed;
    public int strength;
    public int health;
    public int crit;
    public int luck;

    [Space(10)]
    [Header("Weights")]
    [Space]

    [SerializeField]
    float lifeStealWeight;
    [SerializeField]
    float defenseWeight;
    [SerializeField]
    float speedWeight;
    [SerializeField]
    float strengthWeight;
    [SerializeField]
    float healthWeight;
    [SerializeField]
    float critWeight;
    [SerializeField]
    float luckWeight;

    //Adi's awesome number for combat power
    [Space(10)]
    public int combatPower;

    [HideInInspector]
    public PlayerInventory inven;

    void Awake()
    {
        inven = GameObject.FindWithTag("Inventory").GetComponent<PlayerInventory>();
        UpdateStats(); 
    }

    //*****************HELPER FUNCTIONS TO CHANGE PLAYER's HEALTH ACCORDING TO HEALTH STAT *****************
    //SET HEALTH TO A CERTAIN NUMBER
    public void SetHealth(int h)
    {
        health = h;
    }

    //ADD OR SUBTRACT A NUMBER FROM THE MAX HEALTH
    public void AddHealth(int h)
    {
        health += h;
    }

    public int GetHealth()
    {
        return health;
    }
    //*****************END HELPER FUCNTIONS ***********************************



    void OnEnable()
    {
        PlayerInventory.OnInventoryUpdate += UpdateStats;
    }

    void OnDisable()
    {
        PlayerInventory.OnInventoryUpdate -= UpdateStats;
    }
    void Update()
    {
        UpdateStats();
    }
    void UpdateStats()
    {
        lifeSteal=baseLifeSteal;
        defense=baseDefense;
        speed=baseSpeed;
        strength=baseStrength;
        health=baseHealth;
        crit=baseCrit;
        luck=baseLuck;

        Armor helmet = (Armor)inven.activeHelmet;
        Armor chestplate = (Armor)inven.activeChestplate;
        Armor pants = (Armor)inven.activePants;

        if (helmet != null)
        {
            lifeSteal += helmet.lifeSteal;
            defense += helmet.defense;
            speed += helmet.speed;
            strength += helmet.strength;
            health += helmet.health;
            crit += helmet.crit;
            luck += helmet.luck;
        }
        if (chestplate != null)
        { 
            lifeSteal += chestplate.lifeSteal;
            defense += chestplate.defense;
            speed += chestplate.speed;
            strength += chestplate.strength;
            health += chestplate.health;
            crit += chestplate.crit;
            luck += chestplate.luck;
        }
        if (pants != null)
        {
            lifeSteal += pants.lifeSteal;
            defense += pants.defense;
            speed += pants.speed;
            strength += pants.strength;
            health += pants.health;
            crit += pants.crit;
            luck += pants.luck;
        }
        //combatPower = strength*(1+(crit/100))*(1+(lifeSteal/100))+(health*(1+(defense/100)))/100; 
        combatPower = (int)(strength * strengthWeight + crit * critWeight + defense * defenseWeight + health * healthWeight + luck * luckWeight + speed * speedWeight + lifeSteal * lifeStealWeight);
    }
}
