using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class next_lvl : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D info)
    {

        if (info.name == "Player")
        {
            
            GameManager.Instanse.Invoke("EndLevel", 1.0f);
            //Destroy(gameObject);
        }

    }
}
