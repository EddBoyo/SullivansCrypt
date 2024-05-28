using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Make sure that anything that needs saving implements this interface in order to be properly saved and loaded
public interface saveDataInterface 
{
   void loadData(gameData data);

   void saveData(gameData data);
   
}
