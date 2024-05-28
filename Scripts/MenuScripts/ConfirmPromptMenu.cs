using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

public class ConfirmPromptMenu : MonoBehaviour
{
    [Header("Navigation/Components")]
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button rejectButton;
    [SerializeField] private TextMeshProUGUI displayText;

    [SerializeField] private NewGame saveMenu;

    public void ActivateMenu(string inputText , UnityAction confirmAction, UnityAction rejectAction)
    {
        this.gameObject.SetActive(true);
        saveMenu.DeactivateMenu();

        this.displayText.text = inputText;

        // removes the listeners on the buttons to add the one passed in
        confirmButton.onClick.RemoveAllListeners();
        rejectButton.onClick.RemoveAllListeners();

        confirmButton.onClick.AddListener( () => 
        {
            DeactivateMenu();
            confirmAction();
        });

        rejectButton.onClick.AddListener( () => 
        {
            DeactivateMenu();
            rejectAction();
        });

    }

    private void DeactivateMenu()
    {
        saveMenu.ActivateMenu();
        this.gameObject.SetActive(false);
        
    }
}
