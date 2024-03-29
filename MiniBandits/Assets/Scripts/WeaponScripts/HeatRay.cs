using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HeatRay : WeaponTemplate
{
    public LayerMask raycastMask; 
    List<GameObject> hitEnemies = new List<GameObject>();
    LineRenderer lineRen; 

    void Awake()
    {
        lineRen = GetComponent<LineRenderer>();
    }
    public override void Update()
    {
        //IF PLAYER IS GONE, PLAYER CAN't MOVE, OR MOUSE IS OVER UI, RETURN.
        if (player == null || !player.canMove)
        {
            return;
        }
        UpdateStats();
        damage = baseDamage + (int)((player.GetComponent<Player>().strength / 100.0) * baseDamage);
          
    } 
    public override void WhileAttacking()
    { 
        PlayAttackAnimation();
        lineRen.enabled = true;
        ShootLaser();
    }
    public override void StopAttack()
    { 
        lineRen.enabled = false;
    }
    void ShootLaser()
    { 
        if (Physics2D.Raycast(player.transform.position, attackDir,100f,raycastMask))
        { 
            RaycastHit2D hit = Physics2D.Raycast(player.transform.position, (Vector3)attackDir, 100f, raycastMask); 
            DrawRay(transform.position, hit.point);

            GameObject obj = hit.collider.gameObject; 
            if (hitEnemies.Contains(obj))
            {
                return;
            }
            if (obj.GetComponent<Health>() != null)
            {
                if (obj.GetComponent<IDamageable>() != null)
                {
                    obj.GetComponent<IDamageable>().Damage(damage);
                }
                if (obj.GetComponent<IAffectable>() != null)
                {
                    obj.GetComponent<IAffectable>().Knockback(knockBack, transform.position);
                }
                hitEnemies.Add(obj);
                StartCoroutine(RemoveFromList(obj));
            }
        }
    }
    IEnumerator RemoveFromList(GameObject obj)
    {
        yield return new WaitForSeconds(1 / weapon.attackSpeed);
        hitEnemies.Remove(obj);
    }

    void DrawRay(Vector2 startPos, Vector2 endPos)
    {  
        lineRen.SetPosition(0, startPos);
        lineRen.SetPosition(1, endPos);
    }
}
