using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponTemplate : MonoBehaviour
{   
    float currentAttackCooldown;

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


    //FROM SCRIPTABLE OBJECT
    [HideInInspector]
    public float attackSpeed;

    [HideInInspector]
    public int baseDamage;

    [HideInInspector]
    public int numProjectiles;

    [HideInInspector]
    public int range;

    [HideInInspector]
    public int AOE;

    [HideInInspector]
    public int knockBack;

    [HideInInspector]
    public int projectileSpeed;
     

    public virtual void Start()
    {
        attackCooldown = 1f / weapon.attackSpeed;
        weaponName = weapon.name; 
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        GetComponent<SpriteRenderer>().sprite = weapon.sprite;


        baseDamage = weapon.damage;
        attackSpeed = weapon.attackSpeed;
        numProjectiles = weapon.numProjectiles;
        range = weapon.range;
        AOE = weapon.AOE;
        knockBack = weapon.knockBack;
        projectileSpeed = weapon.projectileSpeed; 
    } 
    public virtual void Update()
    {
        //IF PLAYER IS GONE, PLAYER CAN't MOVE, OR MOUSE IS OVER UI, RETURN.
        if (player == null || !player.canMove|| EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        damage = baseDamage+ (int)((player.GetComponent<Player>().strength / 100.0) * baseDamage); 
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
