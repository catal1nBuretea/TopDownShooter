using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 7f;
    public int damage = 1;
    public Rigidbody2D rb;

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * speed;
        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Ignoră alți inamici și alte gloanțe inamice
        if (hitInfo.CompareTag("Enemy") || hitInfo.GetComponent<EnemyBullet>() != null)
        {
            return;
        }

        if (hitInfo.CompareTag("Player"))
        {
            PlayerHealth playerHealth = hitInfo.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}