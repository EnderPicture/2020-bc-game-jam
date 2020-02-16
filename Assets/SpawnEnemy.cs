using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    float timer = 0;

    public Spawner[] spawner;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1.5)
        {
            float randomspawn;
            spawner[0].spawn();
            randomspawn = Random.Range(0, 3);

            if (randomspawn == 0)
                //Spawn0();
            if (randomspawn == 1)
                //Spawn1();
            if (randomspawn == 2)
                //Spawn2();
            if (randomspawn == 3) { }
                //Spawn3();

            int clusterspawn = Random.Range(1, 10);
            if (clusterspawn == 10)
            {
                int cluster = 3;
                for (int i = 0; i < cluster; i++)
                {
                    //Spawn0();
                }
                clusterspawn = 0;
            }
            timer = 0;
        }
    } // update

    public void spawnXPatternObject(bool s1, bool s2, bool s3, bool s4, GameObject go)
    {
        /*if (s1)
        spawner1.spawnGameObject(go);
        if (s2)
        spawner2.spawnGameObject(go);
        if (s3)
        spawner3.spawnGameObject(go);
        if (s4)
        spawner4.spawnGameObject(go);*/
    }
}