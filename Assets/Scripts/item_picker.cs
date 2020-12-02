using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_picker : MonoBehaviour
{
    [SerializeField] private int healvalue;
    private void OnTriggerEnter2D(Collider2D info)
    {
        if (info.name == "Player")
        {
            info.GetComponent<Player_activity>().ChangeHp(-healvalue);
            Destroy(gameObject);
        }
        
    }
}
