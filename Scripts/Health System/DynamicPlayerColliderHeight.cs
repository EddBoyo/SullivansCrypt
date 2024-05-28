using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicPlayerColliderHeight : MonoBehaviour
{
    public Transform headTransform; // Camera reference
    public float groundOffset = 0.1f; // Offset so that the collider will stop slightly above the camera

    private CapsuleCollider capsuleCollider;

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        // Raycast down to find ground
        if (Physics.Raycast(headTransform.position, Vector3.down, out RaycastHit hit))
        {
            // Find capsule 
            float capsuleHeight = hit.distance + groundOffset;

            // Update the collider's height and position
            capsuleCollider.height = capsuleHeight;
            capsuleCollider.center = new Vector3(0f, -(capsuleHeight * 0.5f), 0f);


            // Move the entire game object
            transform.position = headTransform.position;
        }
    }
}



