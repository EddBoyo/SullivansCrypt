using UnityEngine;

public class SkeletonAnimation : MonoBehaviour
{
    public float speed = 1.0f;   // Speed of the movement
    private float baseHeight;    // Starting height
    private bool movingUp = true; // Track the direction of movement

    private void Start()
    {
        baseHeight = transform.position.x;   // Store the starting height
    }

    private void Update()
    {
        Vector3 newPosition = transform.position;

        if (movingUp)
        {
            newPosition.x += speed * Time.deltaTime;
            if (newPosition.x >= baseHeight + 5)
            {
                newPosition.x = baseHeight + 5; // Cap at max height
                TurnAround(); // Rotate 180 degrees
                movingUp = false; // Switch direction
            }
        }
        else
        {
            newPosition.x -= speed * Time.deltaTime;
            if (newPosition.x <= baseHeight - 5)
            {
                newPosition.x = baseHeight - 5; // Cap at min height
                TurnAround(); // Rotate 180 degrees
                movingUp = true; // Switch direction
            }
        }

        transform.position = newPosition;
    }

    // Function to rotate the object 180 degrees around the Y-axis
    private void TurnAround()
    {
        transform.Rotate(0, 180, 0);
    }
}
