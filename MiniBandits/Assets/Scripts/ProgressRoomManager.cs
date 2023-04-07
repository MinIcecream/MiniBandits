using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RoomInfo;
using System;
using System.Linq;

public class ProgressRoomManager : BaseRoomManager
{
    public GameObject level;

    public Room room;

    List<GameObject> enemies = new List<GameObject>();
     
    public bool levelComplete = false;
    public bool levelStarted = false;

    //if boss room, its initialized by floormanager. otherwise, its blank
    public string bossTheme;

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
    
    void MakeEnemiesAggro()
    {
        foreach(GameObject enemy in enemies)
        {
            if (enemy!=null)
            { 
                enemy.GetComponent<EnemyAI>().StartLevel();
            } 
        }
    }
    void SpawnEnemies()
    {
        //if a normal room:
        if (room)
        {
            string theme = GameObject.FindWithTag("FloorManager").GetComponent<FloorManager>().floorTheme.ToString();
            theme = char.ToUpper(theme[0]) + theme.Substring(1);
            foreach (Room.enemy enemy in room.enemies)
            {
               // Debug.Log("EnemyPrefabs/" + theme + "/" + enemy.name);
                var newEnemy = Instantiate(Resources.Load<GameObject>("EnemyPrefabs/" +theme+"/"+ enemy.name.Trim()), new Vector2(transform.position.x + enemy.pos.x, transform.position.y + enemy.pos.y), Quaternion.identity);
                enemies.Add(newEnemy); 
                newEnemy.GetComponent<EnemyAI>().Scale(GameManager.floor);
            }
            Invoke("MakeEnemiesAggro", 0.5f);
        }
        //if a boss room:
        else
        {
            var bossData = Resources.Load<GameObject>("BossPrefabs/" + bossTheme);
            GameObject newBoss;

            if (bossData != null)
            {
                newBoss = (GameObject)Instantiate(Resources.Load<GameObject>("BossPrefabs/" + bossTheme), transform.position, Quaternion.identity);
            }
            else
            {
                newBoss = (GameObject)Instantiate(Resources.Load<GameObject>("BossPrefabs/Graveyard"), transform.position, Quaternion.identity);
            }

             
            enemies.Add(newBoss);
            newBoss.GetComponent<EnemyAI>().StartLevel();
        }
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().inCombat = true;
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
        //GameObject.FindWithTag("Inventory").GetComponent<PlayerInventory>().HideUI(); 
        SpawnEnemies();
        levelStarted = true;
    }

    //AFTER YOU BEAT ALL THE ENEMIES:
    public override void EndRoom()
    {
        if (GameObject.FindWithTag("Player") == null)
        {
            return;
        }
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().inCombat = false;
        levelComplete = true;

        if (bossTheme=="")
        { 
            base.EndRoom();
        }
        StartCoroutine(LoadNextLevel());
    }

    //Load the next level
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(1f);

        foreach (GameObject door in doors)
        {
            door.SetActive(true);
        }
        switch (reward)
        {
            case rewardTypes.gold:
                GameObject.FindWithTag("Player").GetComponent<GoldManager>().AddGold(20);
                break;
            case rewardTypes.randomWeapon:
                {
                    Weapon newWeapon = RoomOptionGenerator.GenerateRandomWeapon();
                    GameObject newItem = Instantiate(Resources.Load<GameObject>("Misc/ItemDrop"), itemSpawnPt.position, Quaternion.identity);
                    newItem.GetComponent<ItemDrop>().item = newWeapon;
                }
                break;
            case rewardTypes.randomArmor:
                {
                    Armor newArmor = RoomOptionGenerator.GenerateRandomArmor();
                    GameObject newItem = Instantiate(Resources.Load<GameObject>("Misc/ItemDrop"), itemSpawnPt.position, Quaternion.identity);
                    newItem.GetComponent<ItemDrop>().item = newArmor;
                }
                break;
        }
    }
}
