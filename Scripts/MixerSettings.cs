using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MixerSettings : MonoBehaviour
{
    // May potentially be merged into the actual main options script when menu's are actually made in order to put saving functionality on an apply button
    
    [SerializeField] private AudioMixer theMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private Slider masterSlider;

    private void Awake()
    {
        musicSlider.onValueChanged.AddListener(setMusicSlider);
        masterSlider.onValueChanged.AddListener(setMasterSlider);
        SFXSlider.onValueChanged.AddListener(setSFXSlider);
    }

    public void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicMixer", musicSlider.value);
        masterSlider.value = PlayerPrefs.GetFloat("masterMixer", masterSlider.value);
        SFXSlider.value = PlayerPrefs.GetFloat("SFXMixer", SFXSlider.value);
    }

    private void OnDisable()
    {
        // Saves the option when ever the user clicks off the slider for now
        PlayerPrefs.SetFloat("musicMixer", musicSlider.value);
        PlayerPrefs.SetFloat("masterMixer", masterSlider.value);
        PlayerPrefs.SetFloat("SFXMixer", SFXSlider.value);
    }

    // the 20 is just a modifier to scale how quickly the volume changes with the slider, can be tweaked
    public void setMusicSlider(float value)
    {
        theMixer.SetFloat("musicMixer", Mathf.Log10(value) * 20);
    }

    public void setMasterSlider(float value)
    {
        theMixer.SetFloat("masterMixer", Mathf.Log10(value) * 20);
    }
    
    public void setSFXSlider(float value)
    {
        theMixer.SetFloat("SFXMixer", Mathf.Log10(value) * 20);
    }
}
