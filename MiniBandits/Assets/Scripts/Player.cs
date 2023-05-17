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

    public int baseNumProjectiles;
    public int baseAOE;
    public int baseRange;
    public int baseKnockBack; 
    public int baseProjectileSpeed; 
    public float baseAttackSpeed; 


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
    public int numProjectiles;
    public int projectileSpeed;
    public int AOE;
    public int range; 
    public int knockBack; 
    public float attackSpeed;

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
    float numProjectilesWeight;
    [SerializeField]
    float projectileSpeedWeight;
    [SerializeField]
    float AOEWeight;
    [SerializeField]
    float rangeWeight;
    [SerializeField]
    float knockBackWeight; 
    [SerializeField]
    float attackSpeedWeight; 


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
        baseHealth += h; 
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
        numProjectiles=baseNumProjectiles;
        projectileSpeed=baseProjectileSpeed;
        AOE=baseAOE;
        range=baseRange;
        knockBack=baseKnockBack; 
        attackSpeed=baseAttackSpeed;

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
            numProjectiles += helmet.numProjectiles;
            projectileSpeed += helmet.projectileSpeed;
            AOE += helmet.AOE;
            range += helmet.range;
            attackSpeed += helmet.attackSpeed;
            knockBack += helmet.knockBack; 
        }
        if (chestplate != null)
        { 
            lifeSteal += chestplate.lifeSteal;
            defense += chestplate.defense;
            speed += chestplate.speed;
            strength += chestplate.strength;
            health += chestplate.health;
            crit += chestplate.crit; 
            numProjectiles += chestplate.numProjectiles;
            projectileSpeed += chestplate.projectileSpeed;
            AOE += chestplate.AOE;
            range += chestplate.range;
            attackSpeed += chestplate.attackSpeed;
            knockBack += chestplate.knockBack; 
        }
        if (pants != null)
        {
            lifeSteal += pants.lifeSteal;
            defense += pants.defense;
            speed += pants.speed;
            strength += pants.strength;
            health += pants.health;
            crit += pants.crit; 
            numProjectiles += pants.numProjectiles;
            projectileSpeed += pants.projectileSpeed;
            AOE += pants.AOE;
            range += pants.range;
            attackSpeed += pants.attackSpeed;
            knockBack += pants.knockBack; 
        }
        //combatPower = strength*(1+(crit/100))*(1+(lifeSteal/100))+(health*(1+(defense/100)))/100; 
        combatPower = (int)(strength * strengthWeight + crit * critWeight + defense * defenseWeight + health * healthWeight + speed * speedWeight + lifeSteal * lifeStealWeight
        + numProjectiles * numProjectilesWeight + projectileSpeed * projectileSpeedWeight + AOE * AOEWeight + range * rangeWeight + attackSpeed * attackSpeedWeight + knockBack * knockBackWeight);
    }
}
