using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{

    public Transform target;//set target from inspector instead of looking in Update
    public float speed = 3f;
    // public List<Vector3> MyPositions = new List<Vector3>(1,1,1);

    public int row = -1;
    public int col = -1;


    // Start is called before the first frame update
    void Start()
    {
        // // assuming 20x20 grid
        //     // center 5x5 is unavailable
        //     // 00 to 20 20 
        //     // 5 5, 5 10, 10 5, 10 10 are locked. 
        // for( int i = 0; i < 100; i++ )
        // bool validNum = true; 
        // while( validNum ) {
        //     row = Random.Range(0.00f, 20.0f);
        //     col = Random.Range(0.00f, 20.0f);
        // }

        // transform.position = transform.position + new Vector3(row, col, 0);
        // Debug.Log(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //rotate to look at the player
        transform.LookAt(target.position);
        transform.Rotate(new Vector3(0,-90,0),Space.Self);//correcting the original rotation
        
        //move towards the player
        if (Vector3.Distance(transform.position,target.position)>1f){//move if distance from target is greater than 1
            transform.Translate(new Vector3(speed* Time.deltaTime,0,0) );
        }
    }


}




