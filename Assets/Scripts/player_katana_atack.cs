using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_katana_atack : MonoBehaviour
{
    [SerializeField] private float startTimeBtwAttack;
    private float timeBtwAttack;

    [SerializeField] private Transform attackPos;
    [SerializeField] private LayerMask enemy;
    [SerializeField] private float attackRange;
    [SerializeField] private int damage;

    [Header("Audio")]
    [SerializeField] private InGameSounds hit;
    private AudioSource audioSource;
    private InGameSounds currentSound;

    private Player_Control playerControl;
    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        //anim = GameObject.Find("Player").GetComponent<Animation>();
        //anim = gameObject.GetComponent<Animator>;
        playerControl = GameObject.Find("Player").GetComponent<Player_Control>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBtwAttack <= 0)
        {
            StopAudio(hit);
            if (Input.GetMouseButton(0))
            {
                
                playerControl.KatanaAnimation();
                PlayAudio(hit);
                //Debug.Log("anume");
                Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].GetComponent<Enemy>().TakeDamage(damage);
                }
            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
        

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    public void PlayAudio(InGameSounds sound)
    {
        if (currentSound != null && (currentSound == sound || currentSound.Priority > sound.Priority))
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
