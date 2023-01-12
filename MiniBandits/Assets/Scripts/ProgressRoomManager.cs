using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RoomInfo;

public class ProgressRoomManager : BaseRoomManager
{
    public GameObject level;

    public Room room;

    List<GameObject> enemies = new List<GameObject>();
     
    public bool levelComplete = false;
    public bool levelStarted = false; 
     
     
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
            EndRoom(); 
        } 
    }

    //Load the next level
    IEnumerator LoadNextLevel()
    { 
        yield return new WaitForSeconds(1f); 
        foreach(GameObject door in doors)
        { 
            door.SetActive(true);
        } 
    } 

    
    void SpawnEnemies()
    {
        if (room)
        { 
            foreach (Room.enemy enemy in room.enemies)
            {
                var newEnemy = Instantiate(Resources.Load<GameObject>("EnemyPrefabs/" + enemy.name), new Vector2(transform.position.x + enemy.pos.x, transform.position.y + enemy.pos.y), Quaternion.identity);
                enemies.Add(newEnemy);
                newEnemy.GetComponent<EnemyAI>().StartLevel();
            }
            GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().inCombat = true;
        }  
    }

    //WHEN THE PLAYER WALKS THROUGH THE DOOR:
    public override void TransitionToNextRoom(room r)
    {
        floorMan.SpawnRoom(r);

        floorMan.UpdatePlayerAndCameraPos(); 
    }

    //WHEN THE PLAYER ENTERS THE ROOm!
    public void StartRoom()
    {  
        GameObject.FindWithTag("Inventory").GetComponent<PlayerInventory>().HideUI();
        SpawnEnemies();
        levelStarted = true;
    }

    //AFTER YOU BEAT ALL THE ENEMIES:
    public override void EndRoom()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().inCombat = false;
        levelComplete = true;

        base.EndRoom();
        StartCoroutine(LoadNextLevel());
    }
}
