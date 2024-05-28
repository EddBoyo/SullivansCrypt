using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TestMainMenu : MonoBehaviour
{
    [Header("Menu Navigation")]
    [SerializeField] private TestSubMenu subMenu;

    public void OnMenuButtonClicked()
    {
        subMenu.ActivateMenu();
        this.DeactivateMenu();
    }

    public void onMoveSceneButtonClicked()
    {
        SceneManager.LoadSceneAsync("SampleScene");
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
