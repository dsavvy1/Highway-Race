using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gameManager;  // assign this in the inspector
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] int playerHealth = 20;
    [SerializeField] AudioClip playerDeathSound;
    [SerializeField][Range(0, 1)] float playerDeathSoundVolume = 0.75f;
    [SerializeField] HealthBar healthBar;

    float xMin, xMax;

    void Start()
    {
        SetupMoveBoundaries();
        healthBar.SetMaxHealth(playerHealth);
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
        if (damageDealer != null) ProcessHit(damageDealer);
    }

    public void ProcessHit(damageDealer damageDealer)
    {
        playerHealth -= damageDealer.GetDamage();
        damageDealer.Hit();

        healthBar.SetHealth(playerHealth);

        if (playerHealth <= 0)
        {
            AudioSource.PlayClipAtPoint(playerDeathSound, Camera.main.transform.position, playerDeathSoundVolume);
            Destroy(gameObject);

            gameManager.PlayerDied();
        }

    }
}
