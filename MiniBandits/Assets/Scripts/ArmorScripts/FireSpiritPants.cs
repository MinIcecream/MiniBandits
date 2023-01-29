using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpiritPants : ArmorTemplate
{
    GameObject spirit;
    PlayerMovement player; 

    void Awake()
    {
        spirit = Resources.Load<GameObject>("Misc/FireSpirit");
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();

        StartCoroutine(SpawnSpirits());
    }
    IEnumerator SpawnSpirits()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            if (player.inCombat)
            {
                Instantiate(spirit, transform.position, Quaternion.identity);
            }
        }
    } 
}
