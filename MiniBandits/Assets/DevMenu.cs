using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevMenu : MonoBehaviour
{
    public GameObject panel;
    public GameObject button;

    void Awake()
    {
        if (!button)
        {
            return;
        }
        Object[] items = Resources.LoadAll("Items", typeof(Item));
        foreach(Object item in items)
        { 
            var newButton = Instantiate(button, transform.position, Quaternion.identity) ;
            newButton.GetComponent<DevButton>().SetItem((Item)item);
            newButton.transform.SetParent(this.gameObject.transform);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (panel.activeSelf)
            {
                panel.SetActive(false);
            }
            else
            { 
                panel.SetActive(true);
            }
        }
    }
}
