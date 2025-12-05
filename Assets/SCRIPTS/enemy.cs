using UnityEngine;

public class enemy : MonoBehaviour
{

    [SerializeField] float health = 100;
    [SerializeField] float explosionDuration;
    [SerializeField] ParticleSystem explosionVFX;
    [SerializeField] AudioClip deathSound;
    [SerializeField][Range(0, 1)] float deathSoundVolume = 0.75f;
    void Start()
    {

    }

    void Update()
    {
     
    }


    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        damageDealer damageDealer = otherObject.gameObject.GetComponent<damageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(damageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        //damageDealer.Hit();
        if (health <= 0)
        {
            ParticleSystem explosion = Instantiate(explosionVFX,transform.position, Quaternion.identity);
            explosion.Play();
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
         
            Destroy(gameObject);
           
        }

    }
    
}