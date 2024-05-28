using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoopZone : MonoBehaviour
{
    bool cooldown = false;

    // Update is called once per frame
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && cooldown == false)

        {
            cooldown = true;
            saveDataManager.instance.newGame();
            saveDataManager.instance.saveGame();
            SceneManager.LoadSceneAsync("Dungeon3D", LoadSceneMode.Single);
            //GameObject.Find("Generator").GetComponent<Generator3D>().roundTwo();
        }
    }
}
