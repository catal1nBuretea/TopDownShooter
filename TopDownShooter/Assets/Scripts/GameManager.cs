using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;
    public float multiplier = 1.0f;
    public int baseScorePerKill = 10;

    private int killsForMultiplier = 0;
    public float maxComboTime = 5f; // Cât timp ai la dispoziție să faci o nouă victimă
    private float currentComboTime = 0f;

    public TMPro.TextMeshProUGUI scoreText; // Dacă folosești TextMeshPro, schimbă în: public TMPro.TextMeshProUGUI scoreText;
    public Slider comboBar;

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

    public void AddKill()
    {
        // Adaugă scor pe baza multiplicatorului curent
        score += Mathf.RoundToInt(baseScorePerKill * multiplier);

        // Contorizează victimele pentru următorul nivel de multiplicator
        killsForMultiplier++;
        if (killsForMultiplier >= 5)
        {
            multiplier += 0.1f;
            killsForMultiplier = 0; // O ia de la capăt pentru următoarele 5
        }

        // Reface bara de timp la maximum
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