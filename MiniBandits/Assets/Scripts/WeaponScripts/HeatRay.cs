using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatRay : WeaponTemplate
{
    public LayerMask raycastMask;
    public int knockBackAmt;
    List<GameObject> hitEnemies = new List<GameObject>();
    LineRenderer lineRen; 

    void Awake()
    {
        lineRen = GetComponent<LineRenderer>();
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetMouseButton(0))
        {
            lineRen.enabled = true;
            ShootLaser();
        }
        else
        { 

            lineRen.enabled = false;
        }
    }

    void ShootLaser()
    {
        if(Physics2D.Raycast(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition),100f,raycastMask))
        { 
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)-transform.position, 100f, raycastMask); 
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
                    obj.GetComponent<IAffectable>().Knockback(knockBackAmt, transform.position);
                }
                hitEnemies.Add(obj);
                StartCoroutine(RemoveFromList(obj));
            }
        }
    }
    IEnumerator RemoveFromList(GameObject obj)
    {
        yield return new WaitForSeconds(0.3f);
        hitEnemies.Remove(obj);
    }

    void DrawRay(Vector2 startPos, Vector2 endPos)
    {  
        lineRen.SetPosition(0, startPos);
        lineRen.SetPosition(1, endPos);
    }
}
