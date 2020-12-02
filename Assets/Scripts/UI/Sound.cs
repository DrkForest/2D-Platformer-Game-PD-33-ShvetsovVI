using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public abstract class Sound
{
   
    public AudioClip AudioClip;

    [Range(0, 1)]
    public float Volume;
    [Range(-3, 3)]
    public float Pitch;

    public bool Loop;

   
}
[Serializable]
public class UISound : Sound
{
    public UIClipNames clipName;
    [HideInInspector]
    public AudioSource AudioSource;
}
[Serializable]
public class InGameSounds : Sound
{
    public int Priority;
}