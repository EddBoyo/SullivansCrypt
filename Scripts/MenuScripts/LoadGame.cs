using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    [Header("Menu Navigation")]
    [SerializeField] private MainMenu mainMenu;

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

        // Loops through each save slot and fills the UI through saveSlots script
        foreach (SaveSlots saveSlot in saveSlots)
        {
            gameData saveSlotInfo = null;
            saveSlotData.TryGetValue(saveSlot.getSaveSlotID(), out saveSlotInfo);
            saveSlot.setData(saveSlotInfo);
            if (saveSlotInfo == null)
            {
                saveSlot.SetInteractable(false);
            }
        }
    }

    public void OnSaveSlotClicked(SaveSlots saveSlot)
    {
        // Update Current chosen SaveSlot then load through function within saveDataManager
        saveDataManager.instance.changeSelectedSaveSlotID(saveSlot.getSaveSlotID());
        // Stops the main menu music
        AudioManager.instance.musicSource.Stop();
        //Loads the scene that the player saved at, though not yet implemented, will just send them to HealthSystem
        SceneManager.LoadSceneAsync("Dungeon3D");
        // SceneManager.LoadSceneAsync("HealthSystem");
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
}
