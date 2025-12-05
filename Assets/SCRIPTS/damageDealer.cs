using UnityEngine;

public class damageDealer : MonoBehaviour
{
    [SerializeField] int damage = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }

}
