using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class Options : MonoBehaviour
{
    [Header("MenuNavigation")]
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private PauseMenu pauseMenu;

    [Header("AudioMixerStuff")]
    [SerializeField] private AudioMixer theMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    [Header("Dropdown/TurnChanger")]
    [SerializeField] private TMP_Dropdown turnDropdown;
    [SerializeField] private TMP_Dropdown angleDropdown;
    [SerializeField] private ActionBasedControllerManager leftController;
    [SerializeField] private ActionBasedControllerManager rightController;
    [SerializeField] private ActionBasedSnapTurnProvider turnProvider;
    // TODO: FIGURE OUT HOW TO CHANGE SNAP TURN ANGLE

    // Checks if settings already set by player, if so load them, if not set default
    private void Start()
    {
       if(PlayerPrefs.HasKey("masterMixer"))
       {
            loadSettings();
       }
       else
       {
            SetSettings();
       }
    }

    // Sound Options Functions

    // The 20 is just a modifier to scale how quickly the volume changes with the slider, can be tweaked
    public void SetMasterVolume()
    {
        float volume = masterSlider.value;
        theMixer.SetFloat("masterMixer", Mathf.Log10(volume) * 20);
    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        theMixer.SetFloat("musicMixer", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        theMixer.SetFloat("SFXMixer", Mathf.Log10(volume) * 20);
    }

    private void SetSettings()
    {
        SetMusicVolume();
        SetSFXVolume();
        SetMasterVolume();
    }

    private void loadSettings()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicMixer", musicSlider.value);
        masterSlider.value = PlayerPrefs.GetFloat("masterMixer", masterSlider.value);
        SFXSlider.value = PlayerPrefs.GetFloat("SFXMixer", SFXSlider.value);
        SetSettings();
        
    }

    // Apply & Cancel Button Functions
    // PlayerPref smoothTurn 1 == Smooth Turning, 0 == Snap Turning
    public void ApplyChanges()
    {

        if (turnDropdown.value == 0)
        {
            PlayerPrefs.SetInt("smoothTurn", 0);
            leftController.changeMode();
            rightController.changeMode();
        }
        else if (turnDropdown.value == 1)
        {
            PlayerPrefs.SetInt("smoothTurn", 1);
            leftController.changeMode();
            rightController.changeMode();
        }
        
        switch(angleDropdown.value)
        {
            case 0:
                turnProvider.angleChange(30f);
                PlayerPrefs.SetFloat("turnAngle", 30f);
                break;
            case 1: 
                turnProvider.angleChange(45f);
                PlayerPrefs.SetFloat("turnAngle", 45f);
                break;
            case 2:
                turnProvider.angleChange(60f);
                PlayerPrefs.SetFloat("turnAngle", 60f);
                break;
            case 3: 
                turnProvider.angleChange(90f);
                PlayerPrefs.SetFloat("turnAngle", 90f);
                break;
            default:
                turnProvider.angleChange(45f);
                PlayerPrefs.SetFloat("turnAngle", 45f);
                break;
        }
        
        PlayerPrefs.SetFloat("musicMixer", musicSlider.value);
        PlayerPrefs.SetFloat("masterMixer", masterSlider.value);
        PlayerPrefs.SetFloat("SFXMixer", SFXSlider.value);
        mainMenu.ActivateMenu();
        this.DeactivateMenu();
    }

    public void ApplyChangesPauseMenu()
    {

        if (turnDropdown.value == 0)
        {
            PlayerPrefs.SetInt("smoothTurn", 0);
            leftController.changeMode();
            rightController.changeMode();
        }
        else if (turnDropdown.value == 1)
        {
            PlayerPrefs.SetInt("smoothTurn", 1);
            leftController.changeMode();
            rightController.changeMode();
        }
        
        switch(angleDropdown.value)
        {
            case 0:
                turnProvider.angleChange(30f);
                PlayerPrefs.SetFloat("turnAngle", 30f);
                break;
            case 1: 
                turnProvider.angleChange(45f);
                PlayerPrefs.SetFloat("turnAngle", 45f);
                break;
            case 2:
                turnProvider.angleChange(60f);
                PlayerPrefs.SetFloat("turnAngle", 60f);
                break;
            case 3: 
                turnProvider.angleChange(90f);
                PlayerPrefs.SetFloat("turnAngle", 90f);
                break;
            default:
                turnProvider.angleChange(45f);
                PlayerPrefs.SetFloat("turnAngle", 45f);
                break;
        }
        
        PlayerPrefs.SetFloat("musicMixer", musicSlider.value);
        PlayerPrefs.SetFloat("masterMixer", masterSlider.value);
        PlayerPrefs.SetFloat("SFXMixer", SFXSlider.value);
        pauseMenu.ActivateMenu();
        this.DeactivateMenu();
    }

    public void CancelChanges()
    {
        mainMenu.ActivateMenu();
        this.DeactivateMenu();
    }

    public void CancelChangesPauseMenu()
    {
        pauseMenu.ActivateMenu();
        this.DeactivateMenu();
    }

    // Menu Navigation Functions

    public void ActivateMenu()
    {
        this.gameObject.SetActive(true);
    }
    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }
}
