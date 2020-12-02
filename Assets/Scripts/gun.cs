using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gun : MonoBehaviour
{
    [SerializeField] private float offset;
    public GameObject bullet;
    public Transform shotPoint;

    private float timeBtwShots;
    public float startTimeBtwShots;
    //private Player_Control player1;

    [SerializeField] private int startAmmo;
    private int ammo;
    [SerializeField] Text uiAmmo;

    [Header("Audio")]
    [SerializeField] private InGameSounds shoot;
    private AudioSource audioSource;
    private InGameSounds currentSound;

    // Start is called before the first frame update
    void Start()
    {
        ammo = startAmmo;

        audioSource = GetComponent<AudioSource>();




    }

    // Update is called once per frame
    void Update()
    {
        uiAmmo.text = ammo.ToString();


        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0, rotateZ + offset);
        if (ammo > 0)
        {
            if (timeBtwShots <= 0)
            {
                StopAudio(shoot);
                if (Input.GetMouseButton(1))
                {
                    Instantiate(bullet, shotPoint.position, transform.rotation);
                    timeBtwShots = startTimeBtwShots;
                    PlayAudio(shoot);
                    
                    ammo--;
                }
            }
            else
            {
                
                timeBtwShots -= Time.deltaTime;
            }

        }
        if (!audioSource.isPlaying)
        {
          

        }

    }

    public void changeAmmo(int changeAmmo)
    {
        ammo += changeAmmo;
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
