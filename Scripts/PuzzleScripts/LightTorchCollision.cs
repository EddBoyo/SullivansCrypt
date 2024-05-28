using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTorchCollision : MonoBehaviour
{

    private void OnTriggerEnter(Collider collider)
    {
        GameObject thisFlame = transform.GetChild(0).gameObject;
        GameObject collidingTorch = collider.gameObject;
        Transform childTransform = collidingTorch.transform.Find("Flame");

        if (childTransform == null)
        {
            // Debug.Log("Error! Collided torch does not have a Flame component");
            return;
        }

        GameObject otherFlame = childTransform.gameObject;

        if (collider.CompareTag("Torch"))
        {
            // Debug.Log("is a torch!");
            if (thisFlame.activeSelf && otherFlame != null)
            {
                // light other torch's flame
                // Debug.Log("light other torch's flame");
                otherFlame.SetActive(true);
            } else if (!thisFlame.activeSelf && otherFlame.activeSelf)
            {
                // Light this torch's flame
                // Debug.Log("Light this torch's flame");
                thisFlame.SetActive(true);
            }
            
        }
    }

}
