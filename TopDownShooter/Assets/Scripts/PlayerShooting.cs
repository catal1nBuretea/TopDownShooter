using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public int bonusDamage = 0;
    public float autoFireRate = 0.15f; // Intervalul dintre gloanțe când ții apăsat
    private float nextFireTime = 0f;

    void Update()
    {
        if (Time.timeScale == 0f) return;

        bool canAutoFire = GameManager.instance != null && GameManager.instance.autoFireUnlocked;

        if (canAutoFire)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + autoFireRate;
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.damage += bonusDamage;
        }
    }
}