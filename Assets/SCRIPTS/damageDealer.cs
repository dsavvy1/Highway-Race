using UnityEngine;

public class damageDealer : MonoBehaviour
{
    [SerializeField] int damage = 10;

    public int GetDamage() => damage;

    public void Hit()
    {
        Destroy(gameObject);
    }
}
