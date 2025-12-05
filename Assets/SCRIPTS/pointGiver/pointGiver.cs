using UnityEngine;

public class PointGiver : MonoBehaviour
{
    [SerializeField] AudioClip petrolPickUp;
    [SerializeField][Range(0, 1)] float pickUpVolume = 0.75f;
    [Header("Fall Settings")]
    public float fallSpeed = 3f;
    public float destroyY = -15f;

    void Update()
    {
        // Move from top to bottom
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        // Destroy when off screen
        if (transform.position.y <= destroyY)
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(petrolPickUp, Camera.main.transform.position, pickUpVolume);
        }
    }
}
