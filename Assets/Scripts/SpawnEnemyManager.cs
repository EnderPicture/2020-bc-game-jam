using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnEnemyManager : MonoBehaviour {
    
    public Transform player;
    public Spawner[] spawner;
    public EnemyController boss;
    float timer = 0;
    public int enemyAliveCount = 0; // to not crash the game
    int enemySpawn = 5;
    int incrementEveryRounds = 4;
    int enemyRound = 0;

    void Update() {
        timer += Time.deltaTime;

        if( timer >= 1.5 ) {
            bool notpass = true;
            while (notpass && enemyAliveCount < 250) {
                Spawner indiSpawner = spawner[Random.Range(0, spawner.Length)];
                float dis = ((Vector2)player.position - (Vector2)indiSpawner.transform.position).magnitude;
                if (dis > 15) {
                    indiSpawner.spawn(enemySpawn);
                    enemyAliveCount += enemySpawn;
                    notpass = false;
                }
            }
            enemyRound += 1;
            if (enemyRound % incrementEveryRounds == 0)
            {
                enemySpawn += 2;
            }
            if (enemyRound == 13)
            {
                notpass = true;
                while (notpass) {
                    Spawner indiSpawner = spawner[Random.Range(0, spawner.Length)];
                    float dis = ((Vector2)player.position - (Vector2)indiSpawner.transform.position).magnitude;
                    if (dis > 15) {
                        boss = indiSpawner.spawnBoss();
                        notpass = false;
                    }
                }
            }
            if (enemyRound >= 13 && boss != null && boss.health < 0) 
            {
                SceneManager.LoadScene("WinScene");
            }
            timer = 0;
        }
   
    } // update

} // SpawnEnemyManager Class 