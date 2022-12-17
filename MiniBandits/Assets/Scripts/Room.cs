using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Room", menuName = "Room")]
public class Room : ScriptableObject
{ 
    [System.Serializable]
    public struct enemy
    {
        public Vector2 pos;
        public string name;
    }
    public List<enemy> enemies = new List<enemy>();

    public void AddEnemy(string name, Vector2 pos)
    {
        enemy newEnemy;
        newEnemy.name = name;
        newEnemy.pos = pos;
        enemies.Add(newEnemy);
    }
}
