using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class testCollectable : MonoBehaviour, saveDataInterface
{

    [SerializeField] private string id;
    [ContextMenu("Generate guid for id")]
    private void generateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private bool collected = false;
    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grab = GetComponent<XRGrabInteractable>();
        grab.activated.AddListener(Grabbed);
    }
    
    public void loadData(gameData data)
    {
        data.cubeCollected.TryGetValue(id, out collected);
        if (collected)
        {
            gameObject.SetActive(false);
        }
    }

    public void saveData(gameData data)
    {
        if (data.cubeCollected.ContainsKey(id))
        {
            data.cubeCollected.Remove(id);
        }
        data.cubeCollected.Add(id, collected);
    }

    public void Grabbed(ActivateEventArgs arg)
    {
        this.collected = true;
        gameObject.SetActive(false);
    }
}
