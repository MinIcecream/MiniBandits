using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponTemplate : MonoBehaviour
{   
    float currentAttackCooldown;

    Player playerStats; 

    [HideInInspector]
    public PlayerMovement player;

    public GameObject projectile;

    public Weapon weapon;
    //Automatically initialized from weapon scriptable object

    [HideInInspector]
    public string weaponName;
    [HideInInspector]
    public float attackCooldown; 

    [HideInInspector]
    public int damage;
    [HideInInspector]
    public int attackSpeed;
    [HideInInspector]
    public int AOE;
    [HideInInspector]
    public int range;
    [HideInInspector]
    public int knockBack;
    [HideInInspector]
    public int numProjectiles;
    [HideInInspector]
    public int projectileSpeed;


    //FROM SCRIPTABLE OBJECT
    [HideInInspector]
    public float baseAttackSpeed;
    [HideInInspector]
    public int baseDamage;
    [HideInInspector]
    public int baseNumProjectiles;
    [HideInInspector]
    public int baseRange;
    [HideInInspector]
    public int baseAOE;
    [HideInInspector]
    public int baseKnockBack;
    [HideInInspector]
    public int baseProjectileSpeed;
     

    public virtual void Start()
    {
        attackCooldown = 1f / weapon.attackSpeed;
        weaponName = weapon.name; 
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        playerStats = GameObject.FindWithTag("Player").GetComponent<Player>();
        GetComponent<SpriteRenderer>().sprite = weapon.sprite;


        baseDamage = weapon.damage;
        baseAttackSpeed = weapon.attackSpeed;
        baseNumProjectiles = weapon.numProjectiles;
        baseRange = weapon.range;
        baseAOE = weapon.AOE;
        baseKnockBack = weapon.knockBack;
        baseProjectileSpeed = weapon.projectileSpeed; 
    } 
    public virtual void Update()
    {
        //IF PLAYER IS GONE, PLAYER CAN't MOVE, OR MOUSE IS OVER UI, RETURN.
        if (player == null || !player.canMove|| EventSystem.current.IsPointerOverGameObject())
        {
            return;
        } 

        //UPDATE ALL STATS

        UpdateStats();

        if (Input.GetMouseButton(0))
        {
            if (currentAttackCooldown <= 0)
            {
                currentAttackCooldown = attackCooldown;
                Attack();
            }
        }

        if (currentAttackCooldown > 0)
        {
            currentAttackCooldown -= Time.deltaTime;
        } 
    }

    public virtual void UpdateStats(){

        damage = baseDamage+ (int)((playerStats.strength / 100.0) * baseDamage); 
        numProjectiles = baseNumProjectiles + playerStats.numProjectiles;
        projectileSpeed = baseProjectileSpeed + playerStats.projectileSpeed;
        knockBack = baseKnockBack + playerStats.knockBack;
        AOE = baseAOE + playerStats.AOE;
        range = baseRange + playerStats.range; 
        attackCooldown = 1f/(weapon.attackSpeed+playerStats.attackSpeed);
    }
    public virtual void Attack()
    {
        Debug.Log("Attacking!");
    }
    public int GetDamage()
    {
        return damage;
    }
    public string GetName()
    {
        return weaponName;
    }
}
