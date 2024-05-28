using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Navigation")]
    [SerializeField] private NewGame newGameMenu;
    [SerializeField] private LoadGame loadGameMenu;
    [SerializeField] private Options optionsMenu;
    
    [Header("Button Management")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button loadGameButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button closeButton;
    private void Start()
    {
        
        if(!saveDataManager.instance.hasSaveData())
        {
            loadGameButton.interactable = false;
        }
        
    }

    public void GoToOptions()
    {
        optionsMenu.ActivateMenu();
        this.DeactivateMenu();
    }

    public void OnNewGameClicked()
    {
        // Will eventually lead to a separate submenu NewGameMenu when save slots are implemented, however for now will just create a new game and move to the healthSystem testing Scene
        newGameMenu.ActivateMenu();
        this.DeactivateMenu();
    }

    public void OnLoadGameClicked()
    {
        // Will also eventually go to a save slot submenu but for now will just load the Health System Scene if there is already saveData
        loadGameMenu.ActivateMenu();
        this.DeactivateMenu();
    }

    public void OnCloseGameClicked()
    {
        Application.Quit();
    }

    public void ActivateMenu()
    {
        this.gameObject.SetActive(true);
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }
}
