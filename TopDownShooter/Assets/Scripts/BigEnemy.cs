using UnityEngine;

public class BigEnemy : MonoBehaviour
{
    public float speed = 1.5f;
    public Rigidbody2D rb;
    public int health = 5;
    public int damageToPlayer = 1;

    public GameObject smallEnemyPrefab;
    public int spawnCount = 2;

    private Transform player;

    void Start()
    {
        GameObject playerObj = GameObject.Find("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.AddKill();
            }

            if (smallEnemyPrefab != null)
            {
                for (int i = 0; i < spawnCount; i++)
                {
                    Vector2 offset = Random.insideUnitCircle * 0.5f;
                    Instantiate(smallEnemyPrefab, (Vector2)transform.position + offset, Quaternion.identity);
                }
            }

            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageToPlayer);
            }
            Destroy(gameObject);
        }
    }
}