using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class UIAudioManager : MonoBehaviour
{

    [SerializeField] private UISound[] sounds;
    [SerializeField] private AudioMixerGroup audioMixerGroup;
    public static UIAudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
       foreach(UISound s in sounds)
        {
            s.AudioSource = gameObject.AddComponent<AudioSource>();
            s.AudioSource.clip = s.AudioClip;
            s.AudioSource.volume = s.Volume;
            s.AudioSource.pitch = s.Pitch;
            s.AudioSource.loop = s.Loop;
            s.AudioSource.outputAudioMixerGroup = audioMixerGroup;
        }
    }

    public void Play(UIClipNames name)
    {
        UISound sound = Array.Find(sounds, s=> s.clipName == name);
        
        if(sound != null)
        {
            sound.AudioSource.Play();
        }
        else
        {
            Debug.LogError("WrongClipName =" + name);
        }
    }


}
public enum UIClipNames
{
    Play,
    Quit,
    Reset,
    ChooseMenu,
    Settings,
}