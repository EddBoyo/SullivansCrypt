using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class saveDataManager : MonoBehaviour
{
    [Header("TestingMode")]
    [SerializeField] private bool testingMode = false;
    [SerializeField] private bool disableSaving = false;
    [SerializeField] private bool overrideSaveSlotID = false;
    [SerializeField] private string overrideSaveSlotName = "test";

    [Header("FileName Config")]
    [SerializeField] private string fileName;

    private gameData gameInfo; 
    private List<saveDataInterface> sdObjects;
    private fileHandler fileHandler;
    private string selectedSaveSlotID = "";
    // initializes the singleton instance
    public static saveDataManager instance {get; private set;}
    
    // Makes sure that there is only one instance of the dataManger as per singleton design 
    private void Awake()
    {
        //Debug.LogWarning("WE IN AWAKE");
        if(instance != null)
        {
            Debug.Log("Found another instance of dataManager, destroying newest instance");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        // Application.persistentDataPath location C:/Users/Username/AppData/LocalLow/CompanyName/GameName
        // on android /Data/Data/com.companyname.gamename/Files
        this.fileHandler = new fileHandler(Application.persistentDataPath, fileName);
        this.selectedSaveSlotID = fileHandler.getMostRecentSaveData();
        //this.sdObjects = getSDObjects();
        //loadGame();
        // (MAKE SURE TESTING MODE IS ON IN ORDER TO PROPERLY SAVE IN OVERRIDED SAVESLOT)
        // Overrides the SaveSlot that you want to save in with a chosen testing one 
        if(overrideSaveSlotID)
        {
            this.selectedSaveSlotID = overrideSaveSlotName;
            Debug.LogWarning("Currently Overriding saveSlot with Testing saveSlotID: " + overrideSaveSlotName);
        }
    }

    // Scene Transitional Functions
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.LogWarning(" WE IN ON SCENELOADED");
        this.sdObjects = getSDObjects();
        loadGame();
    }

    public void loadInDungeon()
    {
        Debug.LogWarning("WE IN REFRESH");
        this.sdObjects = getSDObjects();
        loadGame();
    }

    public void newGame()
    {
        this.gameInfo = new gameData();
    }
   
    public void loadGame()
    {
        // Skips loading if saving is disable for Testing
        if (disableSaving)
        {
            return;
        }
        this.gameInfo = fileHandler.load(selectedSaveSlotID);
        // Since new games can only be made through the main menu, this allows for testing of saving stuff when wanting specific scenes
        // without having to go through the whole main menu process when starting with no save data
        if(this.gameInfo == null && testingMode)
        {
            newGame();
        }

        if(this.gameInfo == null)
        {
            Debug.Log("No save data found, must utilize newGame functionality within main menu or turn on testingMode on the saveDataManager");
            return;
        }
        foreach(saveDataInterface sdObj in sdObjects)
        {
            sdObj.loadData(gameInfo);
        }

    }

    public void saveGame()
    {
        Debug.Log("saving");
        // disables saving if testing is done
        if(disableSaving)
        {
            return;
        }


        if(this.gameInfo == null)
        {
            Debug.LogWarning("No save data found, must utilize newGame functionality within main menu or turn on testingMode on the saveDataManager in order to save");
            return;
        }
        foreach(saveDataInterface sdObj in sdObjects)
        {
            sdObj.saveData(gameInfo);
        }
        //keeps track of most recent saveData game
        gameInfo.lastUpdated = System.DateTime.Now.ToBinary();

        fileHandler.Save(gameInfo, selectedSaveSlotID);
    }

   

    private List<saveDataInterface> getSDObjects()
    {
        // finds all the objects that implement the save data Interface that are of type MonoBehaviour
        // If needed possibly concatenate with another IEnumerable of different types depending on if we need to save anything not monobehaviour
        IEnumerable<saveDataInterface> sdObjects = FindObjectsOfType<MonoBehaviour>().OfType<saveDataInterface>();

        return new List<saveDataInterface>(sdObjects);
    }

    // functions used by mainMenu to properly show save data information 
    public Dictionary<string, gameData> getAllSaveData()
    {
        return fileHandler.loadSaveSlots();
    }
    
    // Changes the saveSlot and then loads the game (for use within saveSlotMenu's)
    public void changeSelectedSaveSlotID(string newSaveSlotID)
    {
        this.selectedSaveSlotID = newSaveSlotID;
        loadGame();
    }

    public bool hasSaveData()
    {
        return gameInfo != null;
    }

    public int getDungeonSeed()
    {
        return gameInfo.dungeonSeed;
    }
    public void setDungeonSeed(int newSeed)
    {
        gameInfo.dungeonSeed = newSeed;
    }
}
