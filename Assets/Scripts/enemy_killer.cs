using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_killer : MonoBehaviour
{
    protected Rigidbody2D enemyRB;
    protected Animator enemyAnimator;
    protected Vector2 startpoint;

    [SerializeField] private float startTimeBtwAttack;
    private float timeBtwAttack;
    [SerializeField] private int damage;
    private float stopTime;
    [SerializeField] private float startStopTime;
    [SerializeField] private float normalSpeed;
    private Player_activity playerAct;
    private Animator anim;
    [SerializeField] private Transform attackPos;
    [SerializeField] private LayerMask enemy;
    [SerializeField] private float attackRange;

    [SerializeField] private int health;
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGraund;


    [SerializeField] private Collider2D visionCollider;
    [SerializeField] private Collider2D attackCollider;
    private bool faceRight = true;

    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
        playerAct = FindObjectOfType<Player_activity>();


        startpoint = transform.position;
        enemyRB = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();

        visionCollider.enabled = true;
        attackCollider.enabled = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (stopTime <= 0)
        {
            speed = normalSpeed;
        }
        else
        {
            speed = 0;
            stopTime -= Time.deltaTime;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }



        if (visionCollider.enabled == true)
        {
           
        }


        if (visionCollider.enabled == false)
        {
            if (playerAct.transform.position.x > transform.position.x)
            {
                // faceRight = !faceRight;
                transform.eulerAngles = new Vector3(0, 0, 0);

            }
            else
            {
                // faceRight = !faceRight;
                // transform.Rotate(0, 180, 0);
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            MoveToPlayer();
        }

    }

    protected virtual void Move()
    {
        enemyRB.velocity = transform.right * new Vector2(speed, enemyRB.velocity.y);
    }

    protected virtual void MoveToPlayer()
    {
        //enemyRB.velocity = transform.right * new Vector2.MoveTowards();
        transform.position = Vector2.MoveTowards(transform.position, playerAct.transform.position, speed * Time.deltaTime);
    }

    protected void Flip()
    {
        faceRight = !faceRight;
        transform.Rotate(0, 180, 0);
    }

    public void TakeDamage(int damage)
    {
        stopTime = startStopTime;
        health -= damage;
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (visionCollider.enabled == true)
            {
                attackCollider.enabled = true;
                visionCollider.enabled = false;
            }
        }


    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (timeBtwAttack <= 0)
            {
                anim.SetTrigger("AttackEnemy");
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }
        }



    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other = null;
        }
    }

    public void OnEnemyAttack()
    {
        playerAct.ChangeHp(damage);
        timeBtwAttack = startTimeBtwAttack;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(range * 2, 0.5f, 0));
    }

    private bool IsGroundEnding()
    {
        return !Physics2D.OverlapPoint(groundCheck.position, whatIsGraund);
    }
}
