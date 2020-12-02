using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Player_Control))]
public class PC_Input_Controler : MonoBehaviour
{
    Player_Control playerControl;

    float moveX;
    bool inJump;
    bool crawl;

    private void Start()
    {
        playerControl = GetComponent<Player_Control>();
    }

    void Update()
    {
        //axis and buttons
        moveX = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            inJump = true;
        }
        //GetButtonUp("Jump")

        crawl = Input.GetKey(KeyCode.C);


        /* 
           if (Input.GetKeyDown(KeyCode.Space))
            {
                playerRB.AddForce(Vector2.up * 5000);
            };
            */
    }

    private void FixedUpdate()
    {
        playerControl.Move(moveX, inJump, crawl);
        inJump = false;
    }
}
