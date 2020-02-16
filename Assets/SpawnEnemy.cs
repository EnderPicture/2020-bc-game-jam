using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour {
    
    public Transform player;
    public Spawner[] spawner;
    public EnemyController boss; 
   
    float timer = 0;
    int enemy = 0;
    int enemySpawn = 5;
    int incrementEveryRounds = 4;
    int enemyRound = 0;
    void Update() {

        timer += Time.deltaTime;

        if( timer >= 1.5 ) {
            bool notpass = true;
            while (notpass) {
                Spawner indiSpawner = spawner[Random.Range(0, spawner.Length)];
                float dis = ((Vector2)player.position - (Vector2)indiSpawner.transform.position).magnitude;
                if (dis > 15) {
                    indiSpawner.spawn(enemySpawn);
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
                Debug.Log("BOSS APPEAR");
            }
            timer = 0;
        }
     
    } // update

} // SpawnEnemy Class 