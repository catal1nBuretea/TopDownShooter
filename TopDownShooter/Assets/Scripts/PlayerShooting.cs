using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    void Update()
    {
        // 0 reprezintă click stânga la mouse
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Creează un glonț la poziția și rotația FirePoint-ului
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}