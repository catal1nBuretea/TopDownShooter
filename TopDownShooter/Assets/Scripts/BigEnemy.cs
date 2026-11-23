using UnityEngine;

public class BigEnemy : MonoBehaviour
{
    public float speed = 1.5f;
    public Rigidbody2D rb;
    private Transform player;

    public int health = 6;
    public int scoreValue = 30;
    public int damageToPlayer = 2;
    public GameObject deathEffectPrefab;

    void Start()
    {
        if (GameObject.Find("Player") != null)
        {
            player = GameObject.Find("Player").transform;
        }

        if (GameManager.instance != null)
        {
            health += GameManager.instance.GetBonusEnemyHealth() * 2;
        }
    }

    void FixedUpdate()
    {
        if (GameManager.instance != null && GameManager.instance.isTimeFrozen) return;
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
            GameManager.instance.AddKill(scoreValue);
            if (AudioManager.instance != null)
                AudioManager.instance.PlaySFX(AudioManager.instance.enemyDeathClip);
            if (deathEffectPrefab != null)
            {
                GameObject effect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
                Destroy(effect, 2f);
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