using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Damage_dealer : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float timeDelay;
    private Player_activity player1;
    private DateTime lastEncouter;
    private void OnTriggerEnter2D(Collider2D info)
    {
        //Debug.Log("1");
        if ((DateTime.Now - lastEncouter).TotalSeconds < 0.1f)
        {
            return;
        }
        lastEncouter = DateTime.Now;
        player1 = info.GetComponent<Player_activity>();
        //Debug.Log("2");
        if (player1 != null)
        {
           // Debug.Log("3");
            player1.ChangeHp(damage);
        }
        
    }
    private void OnTriggerExit2D(Collider2D info)
    {
        if(player1 == info.GetComponent<Player_activity>())
        {
            player1 = null;
        }
    }
    private void Update()
    {
        if (player1 != null && (DateTime.Now - lastEncouter).TotalSeconds > timeDelay)
        {
            player1.ChangeHp(damage);
            lastEncouter = DateTime.Now;
        }
    }
}
