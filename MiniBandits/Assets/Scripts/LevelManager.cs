using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject level;

    List<GameObject> enemies = new List<GameObject>();
     
    bool levelComplete = false;

    public GameObject openUpperWall, closedUpperWall, closedLowerWall,openLowerWall;
     

    void Start()
    {
        Vector2 spawnPt = GameObject.FindWithTag("SceneSpawnPoint").transform.position;
        level.transform.position = spawnPt;
        GameObject.FindWithTag("SceneSpawnPoint").transform.position = new Vector2(spawnPt.x, spawnPt.y);

        GameObject.FindWithTag("SceneSpawnPoint").transform.position = new Vector2(spawnPt.x, spawnPt.y + 12);

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemies.Add(enemy);
        }
    } 

    void Update()
    {
        if (levelComplete)
        {
            return;
        }
        foreach(GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                return;
            }
        }
        levelComplete = true;

        StartCoroutine(LoadNextLevel());
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
        closedLowerWall.SetActive(true);
        openLowerWall.SetActive(false); 
    }
}
