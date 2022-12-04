using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject level;

    List<GameObject> enemies = new List<GameObject>();
     
    public bool levelComplete = false;
    public bool levelStarted = false;

    public GameObject openUpperWall, closedUpperWall, closedLowerWall,openLowerWall;
     

    void Start()
    { 
        Vector2 spawnPt = GameObject.FindWithTag("SceneSpawnPoint").transform.position;
        level.transform.position = spawnPt; 

        GameObject.FindWithTag("SceneSpawnPoint").transform.position = new Vector2(spawnPt.x, spawnPt.y + 12); 
    } 

    void Update()
    {
        if (levelComplete)
        {
            return;
        }
        if (levelStarted)
        { 
            foreach (GameObject enemy in enemies)
            {
                if (enemy != null)
                {
                    return;
                }
            }
            GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().inCombat = false;
            levelComplete = true;


            //SPAWN A WEAPON AFRTRE YOU BEAT IT
            var item=Instantiate(Resources.Load<GameObject>("ItemInteractable"), (Vector2)transform.position, Quaternion.identity);
            item.GetComponent<ItemInteractable>().UpdateItem(ItemManager.weaponList[Random.Range(0, 4)]); ;

            StartCoroutine(LoadNextLevel());
        } 
    }
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(1f); 
 
        SceneManager.LoadScene("Level2", LoadSceneMode.Additive);
        closedUpperWall.SetActive(false);
        openUpperWall.SetActive(true);  
    } 
    public void SealEntrances()
    {
        GameObject.FindWithTag("Inventory").GetComponent<PlayerInventory>().HideUI();
        closedLowerWall.SetActive(true);
        openLowerWall.SetActive(false);
        SpawnEnemies();
        levelStarted = true;
    }
    void SpawnEnemies()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemies.Add(enemy);
        }
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().inCombat = true;
    }
}
