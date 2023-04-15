using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HolyTome : WeaponTemplate
{
    public Transform pivot;
    public int numBooks;
    public float radius;
    public float increasedRadius;
    float speed;
    float increasedSpeed;

    public float changeOrbitSpeed;

    public bool holding = false;

    List<GameObject> books = new List<GameObject>();

    public override void Start()
    {
        base.Start();
        if (weapon.tier == 1)
        {
            numBooks=4;
        }
        else if (weapon.tier == 2)
        {
            numBooks=5;
        }
        for (int i =0;i< numBooks; i++)
        {
            float ang = (360 / numBooks) * i;
             
            float newX=pivot.position.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
            float newY = pivot.position.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);  

            var newBook = Instantiate(projectile, new Vector2(newX, newY), Quaternion.identity);
            newBook.transform.SetParent(pivot);
            newBook.GetComponent<HolyBookProjectile>().damage = weapon.damage;
            books.Add(newBook); 
        }

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
        if (player == null || !player.canMove || EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        damage = baseDamage + (int)((player.GetComponent<Player>().strength / 100.0) * baseDamage);
        if (Input.GetMouseButton(0))
        {  
            Attack(); 
        }
        if (Input.GetMouseButtonUp(0))
        {
            holding = false;
        }
    }
    void FixedUpdate()
    { 
        foreach (GameObject book in books)
        { 
            if (holding)
            { 
                if (Vector2.Distance(book.transform.position, pivot.position) < increasedRadius)
                {
                    Vector2 diff = book.transform.position - pivot.position;
                    Vector2 normalizedDir = diff.normalized;
                    book.transform.position += (Vector3)(normalizedDir * changeOrbitSpeed);
                }
              
                pivot.Rotate(0, 0, increasedSpeed * Time.fixedDeltaTime); 
            }
            else
            {
                if (Vector2.Distance(book.transform.position, pivot.position) > radius)
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
    public override void Attack()
    { 
        holding = true;
    }   
    void OnDisable()
    {
        foreach(GameObject book in books)
        {
            Destroy(book);
        }
    }
}
