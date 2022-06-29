using Mirror;
using UnityEngine;

public class PlayerHealth : NetworkBehaviour
{
    [SerializeField] public float maxHealth = 100f;
    public float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void TakeDamage(float amount)
    {
        currentHealth -= amount;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet") && !other.gameObject.GetComponent<NetworkIdentity>().hasAuthority)
        {
            TakeDamage(25f);
            Destroy(other.gameObject);
        }
    }
}
