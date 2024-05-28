using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class savePoint : MonoBehaviour
{

   private void Start()
   {
       
   }

   public void activateSave()
   {
        saveDataManager.instance.saveGame();
   }
   
}
