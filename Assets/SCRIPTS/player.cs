using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 8f;

    [Header("Health & Death")]
    [SerializeField] int playerHealth = 20;
    [SerializeField] AudioClip playerDeathSound;
    [SerializeField][Range(0, 1)] float playerDeathSoundVolume = 0.75f;
    [SerializeField] HealthBar healthBar;

    // REMOVED: public GameManager gameManager; // No longer needed—use singleton

    float xMin, xMax;

    private void Start()
    {
        SetupMoveBoundaries();
        healthBar.SetMaxHealth(playerHealth);

        // FIXED: Auto-find GameManager.Instance for cross-scene use
        if (GameManager.Instance == null)
        {
            Debug.LogError("[Player] GameManager.Instance not found on Start!");
        }
        else
        {
            Debug.Log("[Player] Connected to GameManager.Instance.");
        }
    }

    void Update()
    {
        Move();
    }

    void SetupMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + 0.5f;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - 0.5f;
    }

    void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float newX = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        transform.position = new Vector2(newX, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        damageDealer damageDealer = other.gameObject.GetComponent<damageDealer>();
        if (damageDealer != null)
        {
            ProcessHit(damageDealer);
        }
    }

    public void ProcessHit(damageDealer damageDealer)
    {
        
        if (damageDealer == null)
        {
            Debug.LogWarning("[Player] ProcessHit called with null damageDealer—skipping.");
            return;
        }

        playerHealth -= damageDealer.GetDamage();
        damageDealer.Hit();
        healthBar.SetHealth(playerHealth);

        if (playerHealth <= 0)
        {
           
            if (playerDeathSound != null)
            {
                AudioSource.PlayClipAtPoint(playerDeathSound, Camera.main.transform.position, playerDeathSoundVolume);
            }

           
            if (GameManager.Instance != null)
            {
                GameManager.Instance.PlayerDied();
            }
            else
            {
                Debug.LogError("[Player] GameManager.Instance is null on death—can't trigger PlayerDied!");
              
            }

            Destroy(gameObject); 
        }
    }
}