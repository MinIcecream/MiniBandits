using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStaminaUI : MonoBehaviour
{
    PlayerStamina stamina;
    int maxStamina;
    int currStamina;

    public List<StaminaIcon> staminaIcons = new List<StaminaIcon>();
    void Start()
    {
        stamina = GameObject.FindWithTag("Player").GetComponent<PlayerStamina>();
    }
     
    void Update()
    { 
        if (stamina == null)
        {
            return;
        }
        int currStamina = stamina.GetStamina();

        for(int i = 0; i < staminaIcons.Count; i++)
        {
            if (i < currStamina)
            {
                staminaIcons[i].Activate();
            }
            else
            { 
                staminaIcons[i].DeActivate();
            }
        }
    }

    /*
    void SpawnIcons(int diff)
    {
        if (diff > 0)
        { 
            for (int i = 0; i < diff; i++)
            { 
                Instantiate(Resources.Load<GameObject>("StaminaIcon"), transform.position, Quaternion.identity);
            }
        }
        else
        {
            for( int i=staminaIcons.Count -1; i >= staminaIcons.Count - diff; i--)
            {
                Destroy(staminaIcons[i].gameObject);
                staminaIcons.RemoveAt(i);
            }
        }
    }*/
}
