using UnityEngine;

public class PointGiverMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float leftBoundary = -8f;
    public float rightBoundary = 8f;

    private int direction = 1; 
    void Update()
    {
        // Move left or right
        transform.Translate(Vector3.right * direction * moveSpeed * Time.deltaTime);

      
        if (transform.position.x >= rightBoundary)
        {
            direction = -1;
        }
        else if (transform.position.x <= leftBoundary)
        {
            direction = 1;
        }
    }
}
