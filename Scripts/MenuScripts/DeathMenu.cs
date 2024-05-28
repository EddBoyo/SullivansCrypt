using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathMenu : MonoBehaviour, saveDataInterface
{
    // Start is called before the first frame update
    [SerializeField] private Button ReloadButton;
    [SerializeField] private Button MainMenuButton;
    [SerializeField] private Button QuitButton;
    [SerializeField] private TextMeshProUGUI RoomCountText;

    private int roomsCleared;
    private int totalRooms;

    private void Start()
    {
        RoomCountText.text = roomsCleared + " / " + totalRooms + " Rooms Cleared";
    }

    public void OnReloadClicked()
    {
        SceneManager.LoadSceneAsync("Dungeon3D");
    }
    
    public void OnMainMenuClicked()
    {
        AudioManager.instance.PlayMusic("MenuMusic");
        SceneManager.LoadSceneAsync("MainMenu");
    }
    
    public void OnCloseGameClicked()
    {
        Application.Quit();
    }

    public void loadData(gameData data)
    {
        roomsCleared = data.currentNumberOfGameObjectiveCompleted;
        totalRooms = data.totalObjectiveRooms;
    }

    public void saveData(gameData data)
    {

    }
}
