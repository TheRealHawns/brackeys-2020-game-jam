using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLibrary : MonoBehaviour
{
    
    public static AudioLibrary instance;
    private void Awake()
    {
        if (instance == null)
        {

        instance = this;
        DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public SoundAudioClip[] soundAudioClipArray;

    public AudioClip InitialMusic;
    public AudioClip LoopingMusic;

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }
}
