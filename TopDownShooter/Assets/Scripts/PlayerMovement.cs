using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Camera cam; // Referință către camera din joc

    private Vector2 movement;
    private Vector2 mousePos;

    void Update()
    {
        // Preluarea mișcării (WASD)
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        // Transformă poziția mouse-ului de pe ecran în coordonate din interiorul jocului
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        // Deplasarea jucătorului
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        // Calcularea direcției de la jucător către mouse
        Vector2 lookDir = mousePos - rb.position;

        // Calcularea unghiului de rotație
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        // Aplicarea rotației
        rb.rotation = angle;
    }
}