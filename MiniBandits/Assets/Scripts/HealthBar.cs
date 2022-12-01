using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    private Health playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
        healthBar.maxValue = playerHealth.GetMaxHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth == null)
        {
            return;
        }
        int curHealth = playerHealth.GetHealth();
        healthBar.value = (curHealth);
    }
}
