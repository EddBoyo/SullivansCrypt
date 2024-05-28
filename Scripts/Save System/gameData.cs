using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
// Anything that needs to be saved will be held within this gameData object and needs to implement the saveDataInterface
public class gameData 
{
   public SerializableDictionary<string, bool> cubeCollected;
   public SerializableDictionary<int, bool> roomsCompleted;

   public long lastUpdated;
   public Vector3 position;
   public int dungeonSeed;
   public int currentNumberOfGameObjectiveCompleted;
   public int totalObjectiveRooms;

   public gameData()
   {
      cubeCollected = new SerializableDictionary<string, bool>();
      roomsCompleted = new SerializableDictionary<int, bool>();
      position = Vector3.zero;
      dungeonSeed = 0;
      currentNumberOfGameObjectiveCompleted = 0;
      totalObjectiveRooms = 0;
   }
}
