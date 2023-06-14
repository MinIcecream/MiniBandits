using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class WeaponTemplate : MonoBehaviour 
{   
    float currentAttackCooldown;

    Player playerStats;

    [HideInInspector]
    public Vector2 attackDir;

    GetClosestEnemyPosition enemyPos;

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

        enemyPos = GameObject.FindWithTag("Player").GetComponent<GetClosestEnemyPosition>();
    } 
    public virtual void Update()
    { 
        //IF PLAYER IS GONE, PLAYER CAN't MOVE, OR MOUSE IS OVER UI, RETURN.
        if (player == null || !player.canMove)
        {
            return;
        }
        //Debug.Log(attackDir);
        //UPDATE ALL STATS 
        UpdateStats();
         
        if (currentAttackCooldown > 0)
        {
            currentAttackCooldown -= Time.deltaTime;
        } 
    }

    public virtual void WhileAttacking()
    {

    }
    //When the player presses the attack button:
    public virtual void GetAttackInput()
    {
        WhileAttacking();
        if (currentAttackCooldown <= 0)
        {
            currentAttackCooldown = attackCooldown; 
            Attack();
        }
    }
    //When the player stops presing the attack button:
    public virtual void StopAttack()
    {
         
    }

    public virtual void UpdateStats(){

        damage = baseDamage+ (int)((playerStats.strength / 100.0) * baseDamage); 
        numProjectiles = baseNumProjectiles + playerStats.numProjectiles;
        projectileSpeed = baseProjectileSpeed + playerStats.projectileSpeed;
        knockBack = baseKnockBack + playerStats.knockBack;
        AOE = baseAOE + playerStats.AOE;
        range = baseRange + playerStats.range; 
        attackCooldown = 1f/(weapon.attackSpeed+playerStats.attackSpeed);
         
        attackDir = enemyPos.GetClosestEnemyPos(); 
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
    public virtual void PlayAttackAnimation()
    {
        StartCoroutine(AttackAnimation(0.1f));
    }

    private IEnumerator AttackAnimation(float moveDuration)
    {
        float elapsedTime = 0f;

        Vector2 originalPosition = new Vector2(2, 0);
        Vector2 targetPosition = new Vector2(1, 0);

        while (elapsedTime < moveDuration)
        {
            // Calculate the normalized progress based on time
            float t = elapsedTime / moveDuration;

            // Lerp the object's position between the original position and the target position
            transform.localPosition = Vector3.Lerp(originalPosition, targetPosition, t);

            // Update the elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure that the object reaches the target position exactly
        transform.localPosition = targetPosition; 
        // Reset the elapsed time for the return trip
        elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            // Calculate the normalized progress based on time
            float t = elapsedTime / moveDuration;

            // Lerp the object's position between the target position and the original position
            transform.localPosition = Vector3.Lerp(targetPosition, originalPosition, t);

            // Update the elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure that the object reaches the original position exactly
        transform.localPosition = originalPosition;
    } 
}
