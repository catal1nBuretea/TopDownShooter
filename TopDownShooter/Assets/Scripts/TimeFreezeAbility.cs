using UnityEngine;
using TMPro;

public class TimeFreezeAbility : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public float freezeDuration = 4f;
    public float cooldown = 15f;

    private float freezeTimer = 0f;
    private float cooldownTimer = 0f;

    [Header("UI Abilitate")]
    public GameObject freezeUIContainer;
    public TMP_Text abilityNameText;
    public TMP_Text cooldownText;

    void Start()
    {
        if (playerMovement == null)
        {
            playerMovement = GetComponent<PlayerMovement>();
        }

        if (freezeUIContainer != null)
        {
            freezeUIContainer.SetActive(false);
        }
    }

    void Update()
    {
        if (GameManager.instance == null) return;

        // Afișează UI-ul doar după ce abilitatea este deblocată (10.000 puncte)
        if (freezeUIContainer != null)
        {
            freezeUIContainer.SetActive(GameManager.instance.timeFreezeUnlocked);
        }

        if (Time.timeScale == 0f || !GameManager.instance.timeFreezeUnlocked) return;

        // Gestionează durata înghețării
        if (GameManager.instance.isTimeFrozen)
        {
            freezeTimer -= Time.deltaTime;
            if (freezeTimer <= 0f)
            {
                GameManager.instance.isTimeFrozen = false;
            }
        }

        // Gestionează cooldown-ul
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }

        // Activare pe Click Dreapta când nu e în cooldown și stamina e 100%
        if (Input.GetMouseButtonDown(1))
        {
            if (!GameManager.instance.isTimeFrozen && cooldownTimer <= 0f && playerMovement != null && playerMovement.HasFullStamina())
            {
                ActivateFreeze();
            }
        }

        UpdateUI();
    }

    void ActivateFreeze()
    {
        playerMovement.DrainAllStamina();
        GameManager.instance.isTimeFrozen = true;
        freezeTimer = freezeDuration;
        cooldownTimer = cooldown;
        if (AudioManager.instance != null)
            AudioManager.instance.PlaySFX(AudioManager.instance.freezeClip);
    }

    void UpdateUI()
    {
        if (abilityNameText == null || cooldownText == null) return;

        if (GameManager.instance.isTimeFrozen)
        {
            abilityNameText.text = "TIME FREEZE ACTIV";
            abilityNameText.color = Color.cyan;

            cooldownText.text = freezeTimer.ToString("F1") + "s";
            cooldownText.color = Color.cyan;
        }
        else if (cooldownTimer > 0f)
        {
            abilityNameText.text = "TIME FREEZE";
            abilityNameText.color = Color.white;

            cooldownText.text = "CD: " + Mathf.CeilToInt(cooldownTimer) + "s";
            cooldownText.color = new Color(1f, 0.35f, 0.35f);
        }
        else
        {
            bool hasStamina = playerMovement != null && playerMovement.HasFullStamina();
            abilityNameText.text = hasStamina ? "TIME FREEZE [Click Dreapta]" : "TIME FREEZE (Stamina 100%)";
            abilityNameText.color = hasStamina ? Color.white : Color.yellow;

            cooldownText.text = "";
        }
    }
}