using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject cameraTarget;
    public Vector3 offset = new Vector3(0, 0, -1);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraTarget)
        {
            transform.position = new Vector3(
                cameraTarget.transform.position.x + offset.x,
                cameraTarget.transform.position.y + offset.y,
                cameraTarget.transform.position.z + offset.z);
        }
    }
}
