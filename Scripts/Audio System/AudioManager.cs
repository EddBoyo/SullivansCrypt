using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance {get; private set;}
    public SoundObject[] musicSounds, SFXSounds;
    public AudioSource musicSource, SFXSource;

    [SerializeField] private AudioMixer theMixer;
    // Sets up the Singleton Aspect of the AudioManger and ensures there is just the one throughout each scene
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // On startup attempt to utilize the player prefs for the mixer so it subscribes to any changes made to it via options
        float masterVolume = PlayerPrefs.GetFloat("masterMixer", 0.75f);
        float musicVolume = PlayerPrefs.GetFloat("musicMixer", 0.75f);
        float SFXVolume = PlayerPrefs.GetFloat("SFXMixer", 0.75f);

        theMixer.SetFloat("masterMixer", Mathf.Log10(masterVolume) * 20);
        theMixer.SetFloat("musicMixer", Mathf.Log10(musicVolume) * 20);
        theMixer.SetFloat("SFXMixer", Mathf.Log10(SFXVolume) * 20);

        Scene currentScene = SceneManager.GetActiveScene();
        // checks to see if it is the main menu scene just in case, although will usually always start at main menu to know to play main menu song
        if (string.Equals(currentScene.name, "MainMenu"))
        {
            PlayMusic("MenuMusic");
        }
    }

    // Method to play music from Audio Manager (takes in the name of the .mp3 or whichever song that you want to play)
    public void PlayMusic(string name)
    {
        // Looks through musicSounds for the specific song that wants to be played and saves it to s
        SoundObject s = Array.Find(musicSounds, x => x.soundName == name);
        // if can't find the song, Won't try to play it and throws a warning
        if (s == null)
        {
            Debug.LogWarning("Sound Not Found");
        }
        // If song is found will shuttle it to the musicSource and then the musicSource will Play it
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }

    }

    // Method to play SFX from the Audio Manager (takes int the name of the .wav or whichever SFX clip that you want to play)
    public void PlaySFX(string name)
    {
        // Looks through SFXSounds for the specific sound that wants to be played
        SoundObject sfx = Array.Find(SFXSounds, x => x.soundName == name);
        // If can't find the SFX, won't try to play it and throws a warning
        if (sfx == null)
        {
            Debug.LogWarning("SFX Not Found");
        }
        // If SFX is found will shuttle it the SFXSource and SFXSource will Play it just once
        else
        {
            SFXSource.PlayOneShot(sfx.clip);
        }
    }
}
