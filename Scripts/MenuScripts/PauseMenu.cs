using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class PauseMenu : MonoBehaviour
{
    [Header("PlayerMainCamera")]
    public Transform head;
    public float spawnDistance = 5f;

    [Header("PausingButtons")]
    public GameObject menu;
    public GameObject mainMenu;
    public InputActionProperty showButton;

    [Header("MenuNavigation")]
    [SerializeField] private Options optionsMenu;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button exitGameButton;

    [Header("ConfirmationPrompt")]
    [SerializeField] private ConfirmPromptMenu confirmationPromptMenu;

    [Header("ControlDisabling")]
    [SerializeField] private XRRayInteractor teleportInteraction;
    [SerializeField] private XRRayInteractor LeftRay;
    [SerializeField] private XRRayInteractor RightRay;

    public static bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // timescale = 0 game is paused, timescale = 1 game is unpaused
       if (showButton.action.WasPressedThisFrame())
       {
        if (isPaused)
        {
            isPaused = false;
            teleportInteraction.enabled = true;
            Time.timeScale = 1f;
            menu.SetActive(!menu.activeSelf);
            LeftRay.enabled = false;
            RightRay.enabled = false;
            menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
            if (optionsMenu.gameObject.activeSelf)
            {
                optionsMenu.DeactivateMenu();
                this.ActivateMenu();
            }
            StartCoroutine(reEnableRay());
        }
        else
        {
            isPaused = true;
            teleportInteraction.enabled = false;
            Time.timeScale = 0f;
            menu.SetActive(!menu.activeSelf);
            LeftRay.enabled = true;
            RightRay.enabled = true;
            menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
            if (optionsMenu.gameObject.activeSelf)
            {
                optionsMenu.DeactivateMenu();
                this.ActivateMenu();
            }
        }

       }
       // options and pause menu follow player around invisibly in order to be in the correct orientation when pause is pressed
       optionsMenu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
       optionsMenu.transform.forward *= -1;
       menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
       menu.transform.forward *= -1;
    }

    public void onContinueClicked()
    {
        isPaused = false;
        teleportInteraction.enabled = true;
        Time.timeScale = 1f;
        menu.SetActive(!menu.activeSelf);
        menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        LeftRay.enabled = false;
        RightRay.enabled = false;
        StartCoroutine(reEnableRay());
    }

    public void onOptionsClicked()
    {
        optionsMenu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        optionsMenu.ActivateMenu();
        mainMenu.SetActive(false);
    }


    public void mainMenuClicked()
    {
        //Debug.Log("buttonClicked");
        isPaused = false;
        Time.timeScale = 1f;
        menu.SetActive(!menu.activeSelf);
        menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        AudioManager.instance.PlayMusic("MenuMusic");
        SceneManager.LoadSceneAsync("MainMenu");
        /*
       confirmationPromptMenu.ActivateMenu(
        "Are you sure you want to back to the Main Menu, you will lose any progress since your last save.", 
        // Confirm Action
        () =>
        {
            isPaused = false;
            Time.timeScale = 1f;
            menu.SetActive(!menu.activeSelf);
            menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;

            SceneManager.LoadSceneAsync("MainMenu");
        },
        // Reject Action
        () =>
        {
            this.ActivateMenu();
        }
       );
       */
        
    }

    public void quitButtonClicked()
    {
        isPaused = false;
        Time.timeScale = 1f;
        menu.SetActive(!menu.activeSelf);
        menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        Application.Quit();
    }

    public void ActivateMenu()
    {
        mainMenu.SetActive(true);
    }

    public void DeactivateMenu()
    {
        mainMenu.SetActive(false);
    }


    // Coroutine to reenable ray interactors
    private IEnumerator reEnableRay()
    {
        yield return new WaitForSeconds(0.5f);
        LeftRay.enabled = true;
        RightRay.enabled = true;
    }
}
