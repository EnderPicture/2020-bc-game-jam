using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform tl;
    public Transform br;

    public Transform player;

    public static int RAND = 0;
    public int mode = RAND;
    public int amount = 10;

    public GameObject[] enemies;

    public GameObject container;

    public void spawn(int a)
    {
        if (mode == RAND)
        {
            for (int i = 0; i < a; i++)
            {
                int rand = Random.Range(0, enemies.Length);
                GameObject newEnemy = GameObject.Instantiate(enemies[0]);
                newEnemy.GetComponent<EnemyController>().player = player;
                newEnemy.transform.position = new Vector3(Random.Range(tl.position.x, br.position.x), Random.Range(br.position.y, tl.position.y), 0);
            }
        }
    }

    public EnemyController spawnBoss()
    {
        int rand = Random.Range(0, enemies.Length);
        GameObject newEnemy = GameObject.Instantiate(enemies[0]);
        newEnemy.GetComponent<EnemyController>().player = player;
        newEnemy.GetComponent<EnemyController>().mode = 2;
        newEnemy.GetComponent<EnemyController>().setHealth(100);
        newEnemy.transform.position = new Vector3(Random.Range(tl.position.x, br.position.x), Random.Range(br.position.y, tl.position.y), 0);
        return newEnemy.GetComponent<EnemyController>();
    }
    public void spawnGameObject(GameObject go) {
        GameObject newEnemy = GameObject.Instantiate(go);
        newEnemy.GetComponent<EnemyController>().player = player;
        newEnemy.transform.position = new Vector3(Random.Range(tl.position.x, br.position.x), Random.Range(br.position.y, tl.position.y), 0);
    }
}
