using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class fileHandler
{
    private string directoryPath = "";
    private string fileName = "";
    // Start is called before the first frame update
   public fileHandler(string directoryPath, string fileName)
   {
        this.directoryPath = directoryPath;
        this.fileName = fileName;
   }


    public gameData load(string saveSlotID)
    {
        // bails immediately if the profile is somehow null
        if (saveSlotID == null)
        {
            return null;
        }

        string fullPath = Path.Combine(directoryPath, saveSlotID, fileName);
        gameData loadedData = null;
        if(File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using(FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                // Deserializes the saveData back into gameData
                loadedData = JsonUtility.FromJson<gameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to load: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }

    public void Save(gameData data, string saveSlotID)
    {
        // bails immediately if the saveSlotID is null
        if (saveSlotID == null)
        {
            return;
        }

        string fullPath = Path.Combine(directoryPath, saveSlotID, fileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            // Serializes the gameData as a saveData JSON File
            string dataToStore = JsonUtility.ToJson(data, true);
            using(FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save file: " + fullPath + "\n" + e);
        }
    }

    // Function used by main menu to get save slot information in order to display it
    public Dictionary<string, gameData> loadSaveSlots()
    {
        Dictionary<string, gameData> saveSlotDictionary = new Dictionary<string, gameData>();

        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(directoryPath).EnumerateDirectories();
        foreach(DirectoryInfo dirInfo in dirInfos)
        {
            string saveSlot = dirInfo.Name;
            string fullPath = Path.Combine(directoryPath, saveSlot, fileName);
            
            // if current saveSlot directory doesn't have a save slot, will not be loaded into dictionary in order to display
            if (!File.Exists(fullPath))
            {
                Debug.LogWarning("Skipping directory when loading since it has no data: " + saveSlot);
                continue;
            }

            gameData saveSlotData = load(saveSlot);

            // Checks to make sure it isn't null before adding to dictionary to ensure correctness
            if (saveSlotData != null)
            {
                saveSlotDictionary.Add(saveSlot, saveSlotData);
            }
            else
            {
                Debug.LogError("Something went wrong when trying to load saveslot. ID: " + saveSlot);
            }
        }
        return saveSlotDictionary;
    }

    // gets the most recent saveData to properly check within mainMenu to see the loadGame menu
    public string getMostRecentSaveData()
    {
        string mostRecentSaveDataID = null;
        Dictionary<string, gameData> recentSaveInfo = loadSaveSlots();
        foreach (KeyValuePair<string, gameData> pair in recentSaveInfo)
        {
            string currentSaveID = pair.Key;
            gameData checkInfo = pair.Value;


            // makes sure nothing null is check although shouldn't receive any at this stage
            if (checkInfo == null)
            {
                continue;
            }

            if (mostRecentSaveDataID == null)
            {
                mostRecentSaveDataID = currentSaveID;
            }
            else
            {
                DateTime mostRecentDateTime = DateTime.FromBinary(recentSaveInfo[mostRecentSaveDataID].lastUpdated);
                DateTime newDateTime = DateTime.FromBinary(checkInfo.lastUpdated);
                // checks if new is the more recent saveData
                if (newDateTime > mostRecentDateTime)
                {
                    mostRecentDateTime = newDateTime;
                }
            }
        }
        return mostRecentSaveDataID;
    }
}