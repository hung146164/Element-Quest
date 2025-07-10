using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioManager : MonoBehaviour
{
    
    public static AudioManager instance;
    [Header("------------ Audio Sourcer------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SfxSource;
    [Header("------------ Audio Clip---------------")]

    public Sound[] musicSound,sfxSound;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlayMusic(string name)
    {
        Sound s=Array.Find(musicSound,x=>x.name==name);
        if(s == null)
        {
            Debug.Log("Khong co nhac nay");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSound, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Khong co nhac nay");
        }
        else
        {
            SfxSource.PlayOneShot(s.clip);
        }
    }
   
    public void ToggleMusic()
    {
        musicSource.mute=!musicSource.mute;
    }
    public void ToggleSFX()
    {
        musicSource.mute = !musicSource.mute;
    }   
    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    public void SFXVolume(float volume)
    {
        SfxSource.volume = volume;
    }


}

