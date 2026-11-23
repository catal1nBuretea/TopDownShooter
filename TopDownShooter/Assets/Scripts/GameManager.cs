using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;
    public float multiplier = 1.0f;

    private int killsForMultiplier = 0;
    public float maxComboTime = 5f; // Cât timp ai la dispoziție să faci o nouă victimă
    private float currentComboTime = 0f;

    public TMPro.TextMeshProUGUI scoreText; // Dacă folosești TextMeshPro, schimbă în: public TMPro.TextMeshProUGUI scoreText;
    public Slider comboBar;
    [Header("Abilitati")]
    public bool isTimeFrozen = false;
    public bool autoFireUnlocked = false;
    public bool timeFreezeUnlocked = false;
    [Header("Scalare Dificultate Inamici")]
    public int[] enemyHealthThresholds = { 1000, 2500, 5000, 8000, 12000 };

    public int GetBonusEnemyHealth()
    {
        int bonus = 0;
        for (int i = 0; i < enemyHealthThresholds.Length; i++)
        {
            if (score >= enemyHealthThresholds[i])
            {
                bonus++;
            }
        }
        return bonus;
    }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (comboBar != null)
        {
            comboBar.maxValue = maxComboTime;
            comboBar.value = 0f;
        }
        UpdateScoreUI();
    }

    void Update()
    {
        // Dacă bara este activă, scade timpul
        if (currentComboTime > 0)
        {
            currentComboTime -= Time.deltaTime;
            if (comboBar != null) comboBar.value = currentComboTime;

            // Dacă timpul expiră, resetează multiplicatorul
            if (currentComboTime <= 0)
            {
                ResetMultiplier();
            }
        }
    }

    public void AddKill(int points)
    {
        score += Mathf.RoundToInt(points * multiplier);

        if (score >= 5000) autoFireUnlocked = true;
        if (score >= 10000) timeFreezeUnlocked = true;

        killsForMultiplier++;
        if (killsForMultiplier >= 5)
        {
            multiplier += 0.1f;
            killsForMultiplier = 0;
        }

        currentComboTime = maxComboTime;
        UpdateScoreUI();
    }
    void ResetMultiplier()
    {
        multiplier = 1.0f;
        killsForMultiplier = 0;
        currentComboTime = 0f;
        if (comboBar != null) comboBar.value = 0f;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Scor: " + score + " (x" + multiplier.ToString("F1") + ")";
        }
    }
}