using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayProgress : MonoBehaviour
{

    public TextMeshProUGUI tmp;

    void Update()
    {
        tmp.text = GameManager.room.ToString() + "-" + GameManager.floor.ToString();
    }
}
