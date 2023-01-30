using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldUI : MonoBehaviour
{
    TextMeshProUGUI tmp;
    GoldManager man;
     
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        man = GameObject.FindWithTag("Player").GetComponent<GoldManager>();
    }
    
    void Update()
    {
        tmp.text = (man.GetGold()).ToString()+" gold";
    }
}
