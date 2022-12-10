using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    private Health playerHealth;
    public TextMeshProUGUI tmp;

    void Start()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
        healthBar.maxValue = playerHealth.GetMaxHealth();
    }


    void Update()
    {
        if (playerHealth == null)
        {
            return;
        }
        int curHealth = playerHealth.GetHealth();
        healthBar.value = (curHealth);
        tmp.text = curHealth.ToString();
        healthBar.maxValue = playerHealth.GetMaxHealth();
    }
}
