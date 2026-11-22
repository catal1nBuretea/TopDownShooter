using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 7f;
    public int damage = 1;
    public Rigidbody2D rb;
    private float lifeTimer = 3f;

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * speed;
    }

    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.isTimeFrozen)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        rb.linearVelocity = transform.up * speed;
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
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