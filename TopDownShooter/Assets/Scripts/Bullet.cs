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

    // Detectează când glonțul (care e trigger) atinge ceva
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        // Distruge glonțul când atinge ceva, excepție fiind jucătorul care a tras
        if (!hitInfo.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}