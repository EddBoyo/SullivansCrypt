using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class bodySocket
{
    public GameObject gameObject;
    [Range(0.01f, 1f)]
    public float heightRatio;
}


public class socketManagement : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject XROrigin;
    public bodySocket[] bodySockets;

    private Vector3 currentCamPosition;
    private Vector3 currentPlayerPosition;
    private Quaternion currentCamRotation;
    //private Quaternion currentPlayerRotation;

    // Update is called once per frame
    void Update()
    {
        currentCamPosition = mainCamera.transform.position;
        currentCamRotation = mainCamera.transform.rotation;
        currentPlayerPosition = XROrigin.transform.position;
        
        foreach (var bodySocket in bodySockets)
        {
            //updateBodySocketHeight(bodySocket);
        }
        updateSocketInventory();
    }

    private void updateBodySocketHeight(bodySocket bodySocket)
    {
        bodySocket.gameObject.transform.position = new Vector3 (bodySocket.gameObject.transform.position.x, currentPlayerPosition.y + (currentCamPosition.y * bodySocket.heightRatio), bodySocket.gameObject.transform.position.z);
    }

    private void updateSocketInventory()
    {
        transform.position = new Vector3(currentCamPosition.x, currentPlayerPosition.y, currentCamPosition.z);
        transform.rotation = new Quaternion(transform.rotation.x, currentCamRotation.y, transform.rotation.z, currentCamRotation.w);
    }

}
