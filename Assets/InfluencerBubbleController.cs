using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfluencerBubbleController : MonoBehaviour
{
    // Start is called before the first frame update

    List<EnemyController> stuffInBubble = new List<EnemyController>();
    void Start()
    {

    }

    public void influence()
    {
        foreach (EnemyController enemy in stuffInBubble) {
            enemy.convertToFollowers();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            stuffInBubble.Add(other.gameObject.GetComponent<EnemyController>());
        }
    }

    void FixedUpdate()
    {
        stuffInBubble.Clear();
    }

}
