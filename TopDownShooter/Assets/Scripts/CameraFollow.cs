using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    // Păstrăm camera pe axa Z la -10 ca să poată vedea obiectele 2D
    public Vector3 offset = new Vector3(0f, 0f, -10f);

    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.position + offset;
        }
    }
}