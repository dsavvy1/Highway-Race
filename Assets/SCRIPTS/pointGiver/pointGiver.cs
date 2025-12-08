using UnityEngine;

public class PointGiver : MonoBehaviour
{
    [SerializeField] AudioClip petrolPickUp;
    [SerializeField][Range(0, 1)] float pickUpVolume = 0.75f;

    public float fallSpeed = 3f;
    public float destroyY = -20f;

    [HideInInspector] public PointGiverSpawner spawner;
    [HideInInspector] public GameManager gameManager;
    public int pointsWorth = 1;

    private bool hasReported = false;

    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        if (!hasReported && transform.position.y <= destroyY)
        {
            hasReported = true;
            Debug.Log($"[PointGiver] Missed (fell off). Reporting to spawner. name={gameObject.name}");
            spawner?.PetrolFinished();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasReported) return;

        if (collision.CompareTag("Player"))
        {
            hasReported = true;
            Debug.Log($"[PointGiver] Collected by player. name={gameObject.name}");
            ScoreManager.Instance?.AddScore(pointsWorth);
            if (petrolPickUp != null)
                AudioSource.PlayClipAtPoint(petrolPickUp, Camera.main.transform.position, pickUpVolume);

            spawner?.PetrolFinished();
            Destroy(gameObject);
        }
    }
}
