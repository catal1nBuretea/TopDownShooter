using UnityEngine;
using UnityEngine.UI;

public class TimeFreezeAbility : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public float freezeDuration = 4f; // Cât timp stau inamicii înghețați
    public float cooldown = 15f;      // Timpul de reîncărcare al abilității

    private float freezeTimer = 0f;
    private float cooldownTimer = 0f;

    public Text abilityStatusText; // Opțional: text UI pentru status/cooldown

    void Start()
    {
        if (playerMovement == null)
        {
            playerMovement = GetComponent<PlayerMovement>();
        }
    }

    void Update()
    {
        if (Time.timeScale == 0f || GameManager.instance == null) return;

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

        // Activare pe Click Dreapta (Mouse 1) la 10.000+ puncte și stamina plină
        if (GameManager.instance.timeFreezeUnlocked && Input.GetMouseButtonDown(1))
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
    }

    void UpdateUI()
    {
        if (abilityStatusText == null || GameManager.instance == null) return;

        if (GameManager.instance.timeFreezeUnlocked)
        {
            if (GameManager.instance.isTimeFrozen)
                abilityStatusText.text = "FREEZE ACTIV: " + freezeTimer.ToString("F1") + "s";
            else if (cooldownTimer > 0f)
                abilityStatusText.text = "Freeze Cooldown: " + Mathf.CeilToInt(cooldownTimer) + "s";
            else
                abilityStatusText.text = "Freeze Pregatit (Click Dreapta - Necesita Stamina 100%)";
        }
        else if (GameManager.instance.autoFireUnlocked)
        {
            abilityStatusText.text = "Auto-Fire Deblocat! (10.000 pct pt Freeze)";
        }
        else
        {
            abilityStatusText.text = "";
        }
    }
}