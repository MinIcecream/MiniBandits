using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoomInfo;

public class FloorManager : MonoBehaviour
{
    public int roomOffset;

    //Gamemanager initializes variable holding this floor's theme (dungeon, spider, graveyard,etc)
    public roomThemes floorTheme;

    //Where to spawn the next room
    public Transform roomSpawnPt;

    //Where player is spawned when they enter the next room
    public Transform playerSpawnPt;
     

    //Generate a list of 10 random level scriptableobjects 
    public List<Room> enemies = new List<Room>(); 

    void Start()
    {
        floorTheme = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().currentTheme;

        Object[] everyEnemy = Resources.LoadAll("Rooms/"+floorTheme.ToString(), typeof(Room));
        List<Room> tempList = new List<Room>();

        foreach(Object r in everyEnemy)
        {
            tempList.Add((Room)r);
        }
         
        while (enemies.Count < 9)
        { 
            int ran = Random.Range(0, tempList.Count - 1);
            enemies.Add(tempList[ran]);
            tempList.RemoveAt(ran);
        } 
    }

    //SPAWN LEVELS, INITIALIZES THEIR ENEMIES TO SPAWN, SETS THE PLAYER SPAWN POINT FOR WHEN THEY ENTER THE LEVEL
    public void SpawnRoom(room room)
    { 
        if (room.roomType == roomTypes.normal)
        {
            if (enemies.Count == 0)
            {
                //SPAWN THE BOSS LEVEL
                var bossData = Resources.Load<GameObject>("RoomLayouts/" + floorTheme.ToString() + "Boss");
                GameObject newLevel;

                if (bossData != null)
                {
                    newLevel = (GameObject)Instantiate(Resources.Load("RoomLayouts/" + floorTheme.ToString() + "Boss"), roomSpawnPt.position, Quaternion.identity);
                }
                else
                {
                    newLevel = (GameObject)Instantiate(Resources.Load("RoomLayouts/GraveyardBoss"), roomSpawnPt.position, Quaternion.identity);
                } 

                ProgressRoomManager man = newLevel.transform.Find("LevelManager").gameObject.GetComponent<ProgressRoomManager>();

                if (man == null)
                {
                    Debug.Log("NO LEVEL MANAGER ATTACHED TO :" + newLevel);
                }
                else
                {
                    playerSpawnPt = man.playerSpawnPt;
                    man.reward = room.reward;
                    man.bossTheme = floorTheme.ToString();
                }
            }
            else
             {
                  
                var dataset = Resources.Load<GameObject>("RoomLayouts/" + floorTheme.ToString());
                GameObject newLevel;

                if (dataset != null)
                {
                    newLevel = (GameObject)Instantiate(Resources.Load("RoomLayouts/" + floorTheme.ToString()), roomSpawnPt.position, Quaternion.identity);
                }
                else
                { 
                    newLevel = (GameObject)Instantiate(Resources.Load("RoomLayouts/Graveyard"), roomSpawnPt.position, Quaternion.identity);
                }
                ProgressRoomManager man = newLevel.transform.Find("LevelManager").gameObject.GetComponent<ProgressRoomManager>();

                if (man == null)
                {
                    Debug.Log("NO LEVEL MANAGER ATTACHED TO :" + newLevel);
                }
                else
                {
                    playerSpawnPt = man.playerSpawnPt;
                    man.reward = room.reward;

                    if (enemies[0] != null)
                    {
                        man.room = enemies[0];
                        enemies.RemoveAt(0);
                    }
                    else
                    {
                        Debug.Log("NO MORE ROOMS TO ASSIGN THIS LEVELAMANGER!!!");
                    }
                }
            } 
        }
        else
        {
            Instantiate(Resources.Load("RoomLayouts/" + room.roomType.ToString()), roomSpawnPt.position, Quaternion.identity);

        } 
        roomSpawnPt.position = new Vector2(roomSpawnPt.position.x, roomSpawnPt.position.y + roomOffset);
    }  
    public void UpdatePlayerAndCameraPos()
    { 
        GameObject.FindWithTag("Player").transform.position = playerSpawnPt.position;

        GameObject.FindWithTag("CameraParent").transform.position = new Vector2(roomSpawnPt.position.x,roomSpawnPt.position.y-roomOffset);
    } 
}
