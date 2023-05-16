using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeysUI : MonoBehaviour
{
    TextMeshProUGUI tmp;
    KeyManager man;

    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        man = GameObject.FindWithTag("Player").GetComponent<KeyManager>();
    }

    void Update()
    {
        int keys = man.GetKeys();

        if (keys == 1)
        { 
            tmp.text = (keys).ToString() + " key";
            return;
        }
        tmp.text = (keys).ToString() + " keys";
    }
}
