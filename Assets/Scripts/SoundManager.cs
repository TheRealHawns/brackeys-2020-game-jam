using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public static class SoundManager
{
  public enum Sound
    {
        Water,
        Harvest,
        Walk, 
        Sell, 
        Buy, 
        Earthquake,
        FailToBuy,
        FixHole,
        Plant,
        ReWater
    }

    private static Dictionary<Sound, float> soundTimerDictionary;


    public static void Initialize()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.Walk] = 0;
    }

    public static void PlaySound(Sound sound)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject("Sound");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.PlayOneShot(GetAudioClip(sound));
        }
    }
    public static bool isPlayingMusic;
    public static void PlayMusic()
    {
        isPlayingMusic = true;
        GameObject musicGameObject = new GameObject("Music");
        Object.DontDestroyOnLoad(musicGameObject);
        AudioSource audioSource = musicGameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.5f;
        audioSource.PlayOneShot(AudioLibrary.instance.InitialMusic);
        

    }
    public static IEnumerator WaitForFirstMusicLoop()
    {
        var music = GameObject.Find("Music").GetComponent<AudioSource>();
        if (music != null)
        {
            yield return new WaitUntil(() => music.isPlaying == false);
            music.clip = AudioLibrary.instance.LoopingMusic;
            music.loop = true;
            music.Play();
        }
    }

    private static bool CanPlaySound(Sound sound)
    {
        switch (sound)
        {
            default:
                return true;
            case Sound.Walk:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float playerMoveTimerMax = 0.5f;
                    if (lastTimePlayed + playerMoveTimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    } else
                    {
                        return false;
                    }
                } else
                {
                    return true;
                }
                
        }
    }

    public static AudioClip GetAudioClip(Sound sound)
    {
        foreach (AudioLibrary.SoundAudioClip soundAudioClip in AudioLibrary.instance.soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found");
        return null;
    }

}
