using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 100;

    public int GetDamage()
    {
        return damage;
    }

    public void OnHit()
    {
        Destroy(gameObject, 0.1f);
    }
}
