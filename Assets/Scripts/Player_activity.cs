using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_activity : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private int maxHp;
    [SerializeField] Slider hpSlider;

    [Header("Audio")]
    [SerializeField] private InGameSounds hurt;
    private AudioSource audioSource;
    private InGameSounds currentSound;


    private Player_Control playerControl;
    

    Vector2 startPosition;
    private int currentHp;
    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        Debug.Log("currentHp = " + currentHp);
        hpSlider.maxValue = maxHp;
        hpSlider.value = maxHp;
        startPosition = transform.position;

        gameManager = GameManager.Instanse;
        audioSource = GetComponent<AudioSource>();
        playerControl = GameObject.Find("Player").GetComponent<Player_Control>();
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            StopAudio(hurt);
        }
    }

    public void ChangeHp(int value)
    {
       


        playerControl.HurtAnimation();
        PlayAudio(hurt);

        currentHp -= value;
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
        else if (currentHp <= 0)
        {
            OnDeath();
        }
       // Debug.Log("value = " + value);
       // Debug.Log("value = " + currentHp);
        hpSlider.value = currentHp;
        
    }

    public void OnDeath()
    {
        Destroy(gameObject);
        gameManager.Restart();
    }


    public void PlayAudio(InGameSounds sound)
    {
        /*if (currentSound == sound || currentSound.Priority > sound.Priority)
        {
            return;
        }*/

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
