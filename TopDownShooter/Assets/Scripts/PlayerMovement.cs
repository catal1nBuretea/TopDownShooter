using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    private Vector2 movement;

    void Update()
    {
        // Preia comenzile de la tastele WASD sau Săgeți (-1, 0 sau 1)
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Normalizează vectorul pentru ca viteza pe diagonală să nu fie mai mare
        movement = movement.normalized;
    }

    void FixedUpdate()
    {
        // Aplică mișcarea fizică
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}