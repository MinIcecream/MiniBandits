using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    public Animator anim;
    public GameObject characterPanel;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Open");
        }
    }
    public void OpenCharacterPanel()
    {
        Invoke("MessyCode", 2f);
    }
    
    void MessyCode()
    {
        characterPanel.SetActive(true);
    }
}
