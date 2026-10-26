using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    public Rigidbody2D rb;
    private Transform player;

    void Start()
    {
        // Găsește automat obiectul jucătorului după numele său
        player = GameObject.Find("Player").transform;
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            // Calculează direcția spre jucător și se deplasează
            Vector2 direction = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
    }
}