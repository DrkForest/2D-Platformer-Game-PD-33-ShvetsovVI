using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammo_picker : MonoBehaviour
{
    [SerializeField] private int ammoCount;
    private gun gumAmmo;
    private void OnTriggerEnter2D(Collider2D info)
    {
        
        if (info.name == "Player")
        {
            gumAmmo = GameObject.Find("pistol").GetComponent<gun>();
            gumAmmo.changeAmmo(ammoCount);
            Destroy(gameObject);
        }

    }
}
