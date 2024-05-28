using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGame : MonoBehaviour
{
    [Header("Menu Navigation")]
    [SerializeField] private MainMenu mainMenu;

    [SerializeField] private Button backButton;

    [Header("ConfirmationPrompt")]
    [SerializeField] private ConfirmPromptMenu confirmationPromptMenu;

    private SaveSlots[] saveSlots;


    private void Awake()
    {
        saveSlots = this.GetComponentsInChildren<SaveSlots>();
    }

    public void ActivateMenu()
    {
        this.gameObject.SetActive(true);
        // Loads all save Slots that currently exists
        Dictionary<string, gameData> saveSlotData = saveDataManager.instance.getAllSaveData();

        //Sets the back button back to activatable from confirmation prompt
        backButton.interactable = true;

        // Loops through each save slot and fills the UI through saveSlots script
        foreach (SaveSlots saveSlot in saveSlots)
        {
            saveSlot.SetInteractable(true);
            gameData saveSlotInfo = null;
            saveSlotData.TryGetValue(saveSlot.getSaveSlotID(), out saveSlotInfo);
            saveSlot.setData(saveSlotInfo);
        }
    }
    
    // If you wish to test moving to the healthSystem Scene comment out the Dungeon3D and uncomment HealthSystem
    public void OnSaveSlotClicked(SaveSlots saveSlot)
    {
        // Disables all of the Menu Buttons for whenever the confirmation prompt appears when attempting to overwrite data
        disableMenuButtons();
        
        //Enters whenever trying to save into an already filled saveSlot
        if (saveSlot.hasData)
        {
            confirmationPromptMenu.ActivateMenu(
                //ConfirmAction Function
                "This will overwrite your save data for this slot Are you sure?",
                () => 
                {
                    // Update Current chosen SaveSlot then load through function within saveDataManager
                    saveDataManager.instance.changeSelectedSaveSlotID(saveSlot.getSaveSlotID());
                    //Makes a newGame
                    saveDataManager.instance.newGame();
                    // saves the game
                    saveDataManager.instance.saveGame();
                    // Stops playing main menu Music
                    AudioManager.instance.musicSource.Stop();
                    //Loads first game Scene
                    SceneManager.LoadSceneAsync("Dungeon3D");
                    // SceneManager.LoadSceneAsync("HealthSystem");
                },
                //RejectAction Function
                () =>
                {
                    this.ActivateMenu();
                }
            );
        }
        // Enters whenever Trying to save into an empty saveSlot
        else
        {
            saveDataManager.instance.changeSelectedSaveSlotID(saveSlot.getSaveSlotID());
            saveDataManager.instance.newGame();
            saveDataManager.instance.saveGame();
            AudioManager.instance.musicSource.Stop();
            SceneManager.LoadSceneAsync("Dungeon3D");
            // SceneManager.LoadSceneAsync("HealthSystem");
        }
    }

    public void onBackClicked()
    {
        mainMenu.ActivateMenu();
        this.DeactivateMenu();
    }


    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }

    private void disableMenuButtons()
    {
        foreach (SaveSlots saveSlot in saveSlots)
        {
            saveSlot.SetInteractable(false);
        }
        backButton.interactable = false;
    }
}
