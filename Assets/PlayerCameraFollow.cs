using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraFollow : MonoBehaviour
{
    public GameObject playerObject;
    public float cameraRadius;
    public float step = 0.05F;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (playerObject)
        {
            float distance = Vector3.Distance(playerObject.transform.position, transform.position);
            if (distance > cameraRadius)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerObject.transform.position, step);
            }
        }
    }
}
