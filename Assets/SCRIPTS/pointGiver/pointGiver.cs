using UnityEngine;

public class PointGiver : MonoBehaviour
{
    [Header("Fall Settings")]
    public float fallSpeed = 3f;
    public float destroyY = -6f;

    void Update()
    {
        // Move from top to bottom
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        // Destroy when off screen
        if (transform.position.y <= destroyY)
        {
            Destroy(gameObject);
        }
    }
}
