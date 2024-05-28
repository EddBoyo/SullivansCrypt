using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarRotation : MonoBehaviour
{
   public Transform camera;

    // Update is called once per frame
    void LastUpdate()
    {
        transform.LookAt(camera);
    }
}
