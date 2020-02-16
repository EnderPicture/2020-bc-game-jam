using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour {
    
    public Transform player;
    public Spawner[] spawner;

    float timer = 0;

    void Update() {

        timer += Time.deltaTime;

        if( timer >= 1.5 ) {

            foreach (Spawner indiSpawner in spawner) {
                float dis = ((Vector2)player.position - (Vector2)indiSpawner.transform.position).magnitude;
                if (dis > 15) {
                    indiSpawner.spawn();
                    break;
                }
            }
            timer = 0;
        }
    } // update

} // SpawnEnemy Class 