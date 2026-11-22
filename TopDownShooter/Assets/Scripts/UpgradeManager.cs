using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public GameObject upgradePanel;
    public PlayerShoot playerShoot;
    public PlayerMovement playerMovement;
    public PlayerHealth playerHealth;

    public int nextUpgradeScore = 100;
    public int scoreIncrement = 100;

    void Start()
    {
        if (upgradePanel != null)
        {
            upgradePanel.SetActive(false);
        }
    }

    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.score >= nextUpgradeScore)
        {
            OpenUpgradeMenu();
        }
    }

    void OpenUpgradeMenu()
    {
        scoreIncrement += 50;
        nextUpgradeScore += scoreIncrement;

        if (upgradePanel != null)
        {
            upgradePanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void UpgradeDamage()
    {
        if (playerShoot != null) playerShoot.bonusDamage += 1;
        CloseUpgradeMenu();
    }

    public void UpgradeSpeed()
    {
        if (playerMovement != null)
        {
            playerMovement.moveSpeed += 1f;
            playerMovement.sprintSpeed += 1.5f;
        }
        CloseUpgradeMenu();
    }

    public void UpgradeHealth()
    {
        if (playerHealth != null) playerHealth.AddMaxHealth(1);
        CloseUpgradeMenu();
    }

    void CloseUpgradeMenu()
    {
        if (upgradePanel != null)
        {
            upgradePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}