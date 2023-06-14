using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HolyTome : WeaponTemplate
{
    public Transform pivot;  
    public float increasedRadius;
    float speed;
    float increasedSpeed;

    public float changeOrbitSpeed;

    public bool holding = false;

    List<GameObject> books = new List<GameObject>();

    void SpawnBooks(int n)
    {
        for (int i =0;i< n; i++)
        {  
            float ang = (360 / n) * i;
             
            float newX=pivot.position.x + range * Mathf.Sin(ang * Mathf.Deg2Rad);
            float newY = pivot.position.y + range * Mathf.Cos(ang * Mathf.Deg2Rad);  

            var newBook = Instantiate(projectile, new Vector2(newX, newY), Quaternion.identity);
            newBook.transform.SetParent(pivot);
            newBook.GetComponent<HolyBookProjectile>().damage = weapon.damage;
            newBook.GetComponent<HolyBookProjectile>().knockBack = weapon.knockBack;
            books.Add(newBook);  
        }
    }
    public override void Start()
    {
        base.Start();  

        GameObject tempPlayer = GameObject.FindWithTag("Player");
        if (tempPlayer == null)
        {
            foreach (GameObject book in books)
            {
                Destroy(book);
            } 
        }
        else
        { 
            pivot.SetParent(tempPlayer.gameObject.transform);
            pivot.localPosition = Vector2.zero;
        }

        increasedSpeed = weapon.attackSpeed * 360f;
        speed = 0.3f * increasedSpeed;
    }

    public override void Update()
    {
        //IF PLAYER IS GONE, PLAYER CAN't MOVE, OR MOUSE IS OVER UI, RETURN.
        if (player == null || !player.canMove)
        {
            return;
        }
        UpdateStats();

        if(books.Count != numProjectiles)
        {
            foreach(GameObject book in books)
            {
                Destroy(book); 
            }
            books = new List<GameObject>();
            SpawnBooks(numProjectiles);
        } 
    }

    public override void StopAttack()
    { 
        holding = false;
    }
    public override void WhileAttacking()
    {
        holding = true;
    }

    void FixedUpdate()
    { 
        foreach (GameObject book in books)
        { 
            if (holding)
            { 
                if (Vector2.Distance(book.transform.position, pivot.position) < range * 1.6f)
                {
                    Vector2 diff = book.transform.position - pivot.position;
                    Vector2 normalizedDir = diff.normalized;
                    book.transform.position += (Vector3)(normalizedDir * changeOrbitSpeed);
                }
              
                pivot.Rotate(0, 0, increasedSpeed * Time.fixedDeltaTime); 
            }
            else
            {
                if (Vector2.Distance(book.transform.position, pivot.position) > range)
                {
                    Vector2 diff = book.transform.position - pivot.position;
                    Vector2 normalizedDir = diff.normalized;
                    book.transform.position -= (Vector3)(normalizedDir * changeOrbitSpeed);
                }
                pivot.Rotate(0, 0, speed*Time.fixedDeltaTime);
            }  
        }
    }
    void LateUpdate()
    { 
        pivot.eulerAngles = new Vector3(
            0,
            0,
            pivot.eulerAngles.z
        );
    }  
    void OnDisable()
    {
        foreach(GameObject book in books)
        {
            Destroy(book);
        }
    }
}
