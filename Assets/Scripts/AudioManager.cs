using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] sfxSounds;
    public AudioSource sfxSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void playSoundEffect(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        sfxSource.PlayOneShot(s.clip);
    }
}
