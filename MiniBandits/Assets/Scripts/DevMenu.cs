using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevMenu : MonoBehaviour
{
    public GameObject panel;
    public GameObject itemButton;
    public GameObject enemyButton;

    void Awake()
    {
        if (!itemButton)
        {
            return;
        }
        Object[] items = Resources.LoadAll("Items", typeof(Item));
        foreach(Object item in items)
        { 
            var newButton = Instantiate(itemButton, transform.position, Quaternion.identity) ;
            newButton.GetComponent<DevButton>().SetItem((Item)item);
            newButton.transform.SetParent(this.gameObject.transform);
        }
        Object[] enemies = Resources.LoadAll("EnemyPrefabs", typeof(GameObject));
        foreach (GameObject enemy in enemies)
        {
            var newButton = Instantiate(enemyButton, transform.position, Quaternion.identity);
            newButton.GetComponent<SpawnEnemyButton>().SetEnemy(enemy);
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
    public void AddGold()
    {

        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            player.GetComponent<GoldManager>().AddGold(10);
        }
    }
    public void AddHealth()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            player.GetComponent<Health>().Heal(10);
        }
             
    }
}
