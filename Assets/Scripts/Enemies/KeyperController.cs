using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyperController : EnemyController
{
    // Start is called before the first frame update
    void Start()
    {
        sprite.color = new Color(1f, 0, 0);
        transform.localScale = new Vector3(2f, 2f, 2f);
    }
}
