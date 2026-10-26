using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintSpeed = 10f;
    public Rigidbody2D rb;
    public Camera cam;

    public Slider sprintBar;
    public float maxStamina = 100f;
    public float staminaDrain = 30f; // Cât scade pe secundă când alergi
    public float staminaRegen = 15f; // Cât se încarcă pe secundă

    private float currentStamina;
    private float currentSpeed;
    private Vector2 movement;
    private Vector2 mousePos;

    void Start()
    {
        currentStamina = maxStamina;
        if (sprintBar != null)
        {
            sprintBar.maxValue = maxStamina;
            sprintBar.value = currentStamina;
        }
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        // Dacă apasă Left Shift, are stamina și se mișcă
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0 && movement != Vector2.zero)
        {
            currentSpeed = sprintSpeed;
            currentStamina -= staminaDrain * Time.deltaTime;
        }
        else
        {
            currentSpeed = moveSpeed;
            if (currentStamina < maxStamina)
            {
                currentStamina += staminaRegen * Time.deltaTime;
            }
        }

        // Actualizare UI bară
        if (sprintBar != null)
        {
            sprintBar.value = currentStamina;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
}