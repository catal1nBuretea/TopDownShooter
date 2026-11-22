using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb;
    public int damage = 1;

    void Start()
    {
        rb.linearVelocity = transform.up * speed;
        Destroy(gameObject, 2f);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Player") || hitInfo.GetComponent<EnemyBullet>() != null)
        {
            return;
        }

        Enemy basicEnemy = hitInfo.GetComponent<Enemy>();
        if (basicEnemy != null)
        {
            basicEnemy.TakeDamage(damage);
        }

        BigEnemy bigEnemy = hitInfo.GetComponent<BigEnemy>();
        if (bigEnemy != null)
        {
            bigEnemy.TakeDamage(damage);
        }

        ShooterEnemy shooterEnemy = hitInfo.GetComponent<ShooterEnemy>();
        if (shooterEnemy != null)
        {
            shooterEnemy.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}