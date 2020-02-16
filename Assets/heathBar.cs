using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heathBar : MonoBehaviour
{

    public heathBar[] healths;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerController controller = GetComponent<PlayerController>();

        if (controller.health < 20f)
        {
            Debug.Log("health bar should die");
            gameObject.SetActive(false);
        }
    }
}
