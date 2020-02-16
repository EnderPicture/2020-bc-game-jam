using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfluencerBubbleController : MonoBehaviour
{
    // Start is called before the first frame update

    List<GameObject> stuffInBubble = new List<GameObject>();
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(stuffInBubble.Count);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            stuffInBubble.Add(other.gameObject);
        }
    }

    void FixedUpdate()
    {
        stuffInBubble.Clear();
    }

}
