using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
public class TestSubMenu : MonoBehaviour
{
    [Header("Menu Navigation")]
    [SerializeField] private TestMainMenu mainMenu;

    
    public void onSubMenuButtonClicked()
    {
        mainMenu.ActivateMenu();
        this.DeactivateMenu();
    }
    
    public void onMoveSceneButtonClicked()
    {
        SceneManager.LoadSceneAsync("HealthSystem");
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
