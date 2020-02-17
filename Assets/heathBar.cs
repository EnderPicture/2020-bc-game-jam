using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class heathBar : MonoBehaviour
{
    public PlayerController player;
    public Health[] health;
    int hp = 10;
    // Update is called once per frame
    void Update()
    {
        if (player.health < hp)
        {
            while (player.health < hp)
            {
                health[hp - 1].gameObject.SetActive(false);
                hp--;
            }
            // Debug.Log("health bar should die");
        }
        if(player.health == 0) {
            SceneManager.LoadScene("Death Scene");
        }
    }
}
