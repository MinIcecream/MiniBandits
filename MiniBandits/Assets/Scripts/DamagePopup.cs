using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    Color color;
    TextMeshPro tmp;
    public float dissapearSpeed;
    public int fadeDelay;
    public float speed;
    public GameObject critIcon;

    public void SetUp(Color col, string dmg, bool crit)
    { 
        tmp.text = dmg;
        color = col;
        tmp.color = color;
        if (crit)
        {
            critIcon.SetActive(true);
        }
        else
        {
            critIcon.SetActive(false);
        }
    }
    void Awake()
    { 
        tmp = GetComponent<TextMeshPro>();
        StartCoroutine(fade(fadeDelay));
    }
    void Update()
    {
        transform.position += new Vector3(0, speed) * Time.deltaTime;
    }
    IEnumerator fade(int delay)
    {
        yield return new WaitForSeconds(delay); 
        while (color.a > 0)
        { 
            color.a -= dissapearSpeed * Time.deltaTime;
            critIcon.GetComponent<SpriteRenderer>().color = color;
            tmp.color = color;
            yield return null;
        } 
    }
}
