using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb;

    void Start()
    {
        // Glonțul se mișcă în sus față de rotația sa
        rb.linearVelocity = transform.up * speed;

        // Distruge glonțul după 2 secunde ca să nu consumi memoria calculatorului
        Destroy(gameObject, 2f);
    }
}