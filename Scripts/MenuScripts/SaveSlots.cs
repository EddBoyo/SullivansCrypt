using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlots : MonoBehaviour
{
    [Header("SaveSlot")]
    [SerializeField] private string saveSlotID = "";

    [Header("Information")]
    [SerializeField] private GameObject noDataSlot;
    [SerializeField] private GameObject hasDataSlot;

    private Button saveSlotButton;

    // publically gettablt only private settable
    public bool hasData { get; private set; } = false;

    private void Awake()
    {
        saveSlotButton = this.GetComponent<Button>();
    }
    
    public void setData(gameData data)
    {
        if (data == null)
        {
            hasData = false;
            noDataSlot.SetActive(true);
            hasDataSlot.SetActive(false);
        }
        else
        {
            hasData = true;
            noDataSlot.SetActive(false);
            hasDataSlot.SetActive(true);
        }
    }

    public string getSaveSlotID()
    {
        return this.saveSlotID;
    }

    public void SetInteractable(bool interactable)
    {
        saveSlotButton.interactable = interactable;
    }
}
