using UnityEngine;

public class PointGiver : MonoBehaviour
{
    [SerializeField] AudioClip petrolPickUp;
    [SerializeField][Range(0, 1)] float pickUpVolume = 0.75f;

    [Header("Fall Settings")]
    public float fallSpeed = 3f;
    public float destroyY = -20f;

    public int pointsWorth = 1; // how many points this petrol can gives

    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        if (transform.position.y <= destroyY)
        {
            Destroy(gameObject);
        }
    }

    // Detect collision with player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Add score
            ScoreManager.Instance.AddScore(pointsWorth);

            // Play sound
            AudioSource.PlayClipAtPoint(petrolPickUp, Camera.main.transform.position, pickUpVolume);

            // Destroy pickup
            Destroy(gameObject);
        }
    }
}
