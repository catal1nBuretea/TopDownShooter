using UnityEngine;

public class ShooterEnemy : MonoBehaviour
{
    public float speed = 2.5f;
    public float stoppingDistance = 5f; // Distanța la care se oprește față de tine
    public Rigidbody2D rb;
    public int health = 3;
    public GameObject deathEffectPrefab;

    [Header("Atac")]
    public GameObject enemyBulletPrefab;
    public float fireRate = 2f;
    private float fireTimer;
    public int scoreValue = 20;

    private Transform player;

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        fireTimer = fireRate;

        GameObject playerObj = GameObject.Find("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        if (GameObject.Find("Player") != null)
        {
            player = GameObject.Find("Player").transform;
        }

        if (GameManager.instance != null)
        {
            health += GameManager.instance.GetBonusEnemyHealth();
        }

    }

    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.isTimeFrozen) return;
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        // Trage doar când a ajuns la distanța de oprire
        if (distance <= stoppingDistance + 0.5f)
        {
            fireTimer -= Time.deltaTime;
            if (fireTimer <= 0f)
            {
                Shoot();
                fireTimer = fireRate;
            }
        }
    }

    void FixedUpdate()
    {
        if (GameManager.instance != null && GameManager.instance.isTimeFrozen) return;
        if (player == null) return;

        // Rotește cercul cu fața spre jucător
        Vector2 lookDir = (Vector2)player.position - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        // Se apropie doar dacă este mai departe decât stoppingDistance
        float distance = Vector2.Distance(rb.position, player.position);
        if (distance > stoppingDistance)
        {
            Vector2 direction = lookDir.normalized;
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
    }

    void Shoot()
    {
        if (enemyBulletPrefab != null)
        {
            Instantiate(enemyBulletPrefab, transform.position, transform.rotation);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.AddKill(scoreValue);
            }
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
}