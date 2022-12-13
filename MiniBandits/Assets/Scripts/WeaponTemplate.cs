using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponTemplate : MonoBehaviour
{
    public float attackCooldown;

    float currentAttackCooldown;

    public PlayerMovement player;

    public GameObject projectile;

    public string weaponName;

    public Weapon weapon; 

    void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>(); 
        GetComponent<SpriteRenderer>().sprite = weapon.sprite;
    }
    void Update()
    {
        //IF PLAYER IS GONE, PLAYER CAN't MOVE, OR MOUSE IS OVER UI, RETURN.
        if (player == null || !player.canMove|| EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
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
}
