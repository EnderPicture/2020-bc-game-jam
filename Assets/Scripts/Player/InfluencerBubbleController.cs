using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfluencerBubbleController : MonoBehaviour
{
    List<OnionController> stuffInBubble = new List<OnionController>();
    public void influence()
    {
        foreach (OnionController enemy in stuffInBubble)
        {
            enemy.convertToFollower();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            stuffInBubble.Add(other.gameObject.GetComponent<OnionController>());
        }
    }

    void FixedUpdate()
    {
        stuffInBubble.Clear();
    }

}
