using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public bool DisplayNumbers;
    public Slider healthBar;
    [SerializeField]
    private Health healthScript;
    public TextMeshProUGUI tmp;

    void Start()
    { 
        healthBar.maxValue = healthScript.GetMaxHealth();
    }

    void Update()
    {
        if (healthScript == null)
        {
            return;
        }
        int curHealth = healthScript.GetHealth();

        if (curHealth < 0)
        {
            curHealth = 0;
        }
        healthBar.value = (curHealth);
        if (DisplayNumbers)
        { 
            tmp.text = curHealth.ToString();
        } 
        healthBar.maxValue = healthScript.GetMaxHealth();
    }
}
