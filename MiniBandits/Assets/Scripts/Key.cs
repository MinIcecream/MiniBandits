using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    GameObject player;
    bool followPlayer = false;
    [SerializeField] float speed;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    void FixedUpdate()
    {
        if (player == null)
        {
            return;
        }
        if (Vector2.Distance(player.transform.position, transform.position) < 3.5f)
        {
            followPlayer = true;
        }
        if (followPlayer)
        {
            Vector2 dir = player.transform.position - transform.position;
            transform.position += (Vector3)(dir.normalized * Time.deltaTime * speed);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            player.GetComponent<KeyManager>().AddKeys(1);
            PopupManager.SpawnPopup(coll.gameObject.transform.position, "+1 key", false);
            Destroy(gameObject);
        }
    }
}
