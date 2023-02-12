using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnEnemyButton : MonoBehaviour
{

    public GameObject enemy;

    public void SetEnemy(GameObject obj)
    {
        enemy = obj;
        Image image = GetComponent<Image>();
        image.sprite = enemy.GetComponent<SpriteRenderer>().sprite;

        image.preserveAspect = true;
        image.type = Image.Type.Simple;
    }
    public void SpawnEnemy()
    {
        var NewEnemy=Instantiate(enemy, GameObject.FindWithTag("Player").transform.position+Vector3.right, Quaternion.identity);
        NewEnemy.GetComponent<EnemyAI>().StartLevel();
    }
}
