using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    private int stamina;
    public int maxStamina;

    public float rechargeTime;
    public float currentRechargeTime;

    void Start()
    {
        stamina = maxStamina;
    }

    public virtual void UseStamina(int staminaUsed)
    {
        stamina -= staminaUsed;
        currentRechargeTime = 0;
    }
    public void GainStamina(int staminaGained)
    {
        if (staminaGained + stamina <= maxStamina)
        {
            stamina += staminaGained;
        }
    }
    public int GetStamina()
    {
        return stamina;
    }
    public int GetMaxStamina()
    {
        return maxStamina;
    }

    void Update()
    {
        currentRechargeTime += 1 * Time.deltaTime;
        if (currentRechargeTime >= rechargeTime)
        {
            currentRechargeTime = 0;
            GainStamina(1);
        }
    } 
}
