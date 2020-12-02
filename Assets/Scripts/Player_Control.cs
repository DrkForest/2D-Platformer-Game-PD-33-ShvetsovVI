using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof (AudioSource))]
[RequireComponent(typeof(Player_Control))]
public class Player_Control : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private Rigidbody2D playerRB;
    private Animator playerAnimator;
    [Header("Horizontal (Y) Movement")]
    [SerializeField] private float speed;
    private bool faceRight = true;

    [Header("Jumping")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float radius;
    [SerializeField] private bool airControl;
    [SerializeField] private float extraJumps;
    private float extraJumpsValue;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask isGround;
    private bool canJump;

    [Header("Crawling")]
    [SerializeField] private Transform headCheck;
    [SerializeField] private Collider2D headCollider;
    private bool canStand;

    [Header("Audio")]
    [SerializeField] private InGameSounds runClip;
    [SerializeField] private InGameSounds jumpClip;
    private AudioSource audioSource;
    
    private InGameSounds currentSound;

    private SpriteRenderer mySpriteRenderer;

    void Start()
    {
        extraJumpsValue = extraJumps;
        playerRB = GetComponent <Rigidbody2D> ();
        playerAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();


    }

    private void Update()
    {
        
    }

    void FixedUpdate()
    {
      
    }

    

    //circle draving
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, radius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(headCheck.position, radius);
    }


    // player character flip Y
    void Flip()
    {
        mySpriteRenderer = GameObject.Find("pistol").GetComponent<SpriteRenderer>();
        //KatanaAnimation();
        faceRight = !faceRight;
        transform.Rotate(0, 180, 0);
        if (mySpriteRenderer != null)
        {
            mySpriteRenderer.flipY = !mySpriteRenderer.flipY;
        }
        
        mySpriteRenderer = null;
    }

    public void KatanaAnimation()
    {
        playerAnimator.SetTrigger("katanaAttack");
        
    }

    public void HurtAnimation()
    {
        playerAnimator.SetTrigger("Hurt");
    }


    public void Move(float moveX, bool inJump, bool crawl)
    {
        #region Movement and flip

        

        //move
        if (moveX != 0 && (canJump || airControl))
            playerRB.velocity = new Vector2(moveX * speed, playerRB.velocity.y);

        //flip check
        if (moveX > 0 && !faceRight)
        {
            Flip();
        }
        else if (moveX < 0 && faceRight)
        {
            Flip();
        }
        #endregion


        #region Jump and jump check`s
        //jump

        canJump = Physics2D.OverlapCircle(groundCheck.position, radius, isGround);

        if (canJump == true)
        {
            StopAudio(jumpClip);
            extraJumpsValue = extraJumps;
           // Debug.Log("jump - " + extraJumpsValue);
        }

        if (inJump && extraJumpsValue > 0)
        {
            //    && canJump 
            //playerRB.velocity = Vector2.up * +jumpForce/100;

            PlayAudio(jumpClip);

            playerRB.AddForce(Vector2.up * +jumpForce);
            extraJumpsValue--;
            //inJump = false;
            
           // Debug.Log("jump - " + extraJumpsValue);
            //canJump = true;
           // Debug.Log("jump - " + canJump);
        }
        //else if (inJump && canJump == true && extraJumpsValue == 0)
        //{
         //   playerRB.AddForce(Vector2.up * +jumpForce);
          //  inJump = false;
        //}

        // jump check
        // true or false

        #endregion


        #region Crawl and stand
        //stand check
        canStand = !Physics2D.OverlapCircle(headCheck.position, radius, isGround);

        if (crawl)
        {
            headCollider.enabled = false;
        }
        else if (!crawl && canStand)
        {
            headCollider.enabled = true;
        }
        #endregion

        playerAnimator.SetFloat("speed", Mathf.Abs(moveX));
        playerAnimator.SetBool("jump", !canJump);
        playerAnimator.SetBool("crouch", !headCollider.enabled);
        
        //audio
        if(canJump && playerRB.velocity.x != 0 && !audioSource.isPlaying)
        {
            PlayAudio( runClip);
        }
        else if(!canJump||playerRB.velocity.x == 0)
        {
            StopAudio(runClip);
        }


    }

    public void PlayAudio(InGameSounds sound)
    {
        if(currentSound != null && (currentSound == sound || currentSound.Priority > sound.Priority ))
        {
            return;
        }

        currentSound = sound;
        audioSource.clip = currentSound.AudioClip;
        audioSource.loop = currentSound.Loop;
        audioSource.pitch = currentSound.Pitch;
        audioSource.volume = currentSound.Volume;

        audioSource.Play();
    }

    public void StopAudio(InGameSounds sound)
    {
        if (currentSound == null || currentSound != sound)
        {
            return;
        }
        audioSource.Stop();
        audioSource.clip = null;
        currentSound = null;
    }
}






/*
    OverlapCircleAll
     Collider2D[] colliders

     if (colliders.Length >0)
     {
         canJump = true;
     }
     else
     {
         canJump = false;
     }
*/




// bad

//playerRB.MovePosition(playerRB.position + Vector2.right * moveX * speed);


//    * moveX     * Time.deltaTime 


/*   if (Input.GetKeyDown(KeyCode.Space))
   {
       rb.AddForce (Vector2.up * 5000);
   };*/
