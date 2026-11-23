using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Surse Audio")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Clipuri Audio")]
    public AudioClip backgroundMusic;
    public AudioClip shootClip;
    public AudioClip enemyDeathClip;
    public AudioClip playerHitClip;
    public AudioClip gameOverClip;
    public AudioClip freezeClip;
    public AudioClip upgradeClip;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}