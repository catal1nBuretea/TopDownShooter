using UnityEngine;
using TMPro;

public class StatsUI : MonoBehaviour
{
    public TMP_Text statsText;
    public PlayerHealth playerHealth;
    public PlayerMovement playerMovement;
    public PlayerShoot playerShoot;

    private int baseDamage = 1;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (playerHealth == null) playerHealth = player.GetComponent<PlayerHealth>();
            if (playerMovement == null) playerMovement = player.GetComponent<PlayerMovement>();
            if (playerShoot == null) playerShoot = player.GetComponent<PlayerShoot>();
        }

        if (playerShoot != null && playerShoot.bulletPrefab != null)
        {
            Bullet b = playerShoot.bulletPrefab.GetComponent<Bullet>();
            if (b != null) baseDamage = b.damage;
        }
    }

    void Update()
    {
        if (statsText == null || playerHealth == null || playerMovement == null || playerShoot == null) return;

        int currentHp = Mathf.Max(0, playerHealth.currentHealth);
        int totalAttack = baseDamage + playerShoot.bonusDamage;

        statsText.text = $"Health: {currentHp}/{playerHealth.maxHealth}\n" +
                         $"Speed: {playerMovement.moveSpeed}\n" +
                         $"Attack: {totalAttack}";
    }
}