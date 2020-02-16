using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform tl;
    public Transform br;

    public Transform player;

    public static int RAND = 0;
    public int mode = RAND;
    public int amount = 10;

    public GameObject[] enemies;

    public GameObject container;

    public void spawn()
    {
        if (mode == RAND)
        {
            for (int c = 0; c < amount; c++)
            {
                int rand = Random.Range(0, enemies.Length);
                GameObject newEnemy = GameObject.Instantiate(enemies[rand]);
                newEnemy.GetComponent<EnemyController>().player = player;
                newEnemy.transform.position = new Vector3(Random.Range(tl.position.x, br.position.x), Random.Range(br.position.y, tl.position.y), 0);
                // newEnemy.transform.parent = container.transform;
            }
        }
    }
    public void spawnGameObject(GameObject go) {
        GameObject newEnemy = GameObject.Instantiate(go);
        newEnemy.GetComponent<EnemyController>().player = player;
        newEnemy.transform.position = new Vector3(Random.Range(tl.position.x, br.position.x), Random.Range(br.position.y, tl.position.y), 0);
        // newEnemy.transform.parent = container.transform;
    }
}
