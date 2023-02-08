using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyTome : WeaponTemplate
{
    public Transform pivot;
    public int numBooks;
    public float radius;
    public float increasedRadius;
    public float speed;
    public float increasedSpeed;

    public float changeOrbitSpeed;

    public bool holding = false;

    List<GameObject> books = new List<GameObject>();

    public override void Awake()
    {
        base.Awake();
        for(int i =0;i< numBooks; i++)
        {
            float ang = (360 / numBooks) * i;

            var newBook=Instantiate(projectile, transform.position, Quaternion.identity);

            float newX=pivot.position.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
            float newY = pivot.position.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad); 
            newBook.transform.position = new Vector2(newX, newY);

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
              
                pivot.Rotate(0, 0, increasedSpeed);
            }
            else
            {
                if (Vector2.Distance(book.transform.position, pivot.position) > radius)
                {
                    Vector2 diff = book.transform.position - pivot.position;
                    Vector2 normalizedDir = diff.normalized;
                    book.transform.position -= (Vector3)(normalizedDir * changeOrbitSpeed);
                }
                pivot.Rotate(0, 0, speed);
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
    public override void Update()
    {
        base.Update(); 
        if (Input.GetMouseButtonUp(0))
        { 
            holding = false; 
        } 
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
