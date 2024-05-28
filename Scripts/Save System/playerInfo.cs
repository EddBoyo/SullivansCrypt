using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInfo : MonoBehaviour, saveDataInterface
{
    [SerializeField] private Transform playerPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadData(gameData data)
    {
        if (data.position != Vector3.zero)
        {
            playerPosition.position = data.position;
        }
    }

    public void saveData(gameData data)
    {
        data.position = playerPosition.position;
        Debug.Log("positionSaved");
    }
}
