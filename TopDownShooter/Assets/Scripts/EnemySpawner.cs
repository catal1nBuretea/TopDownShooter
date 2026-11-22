using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject bigEnemyPrefab;
    public GameObject shooterEnemyPrefab;

    [Range(0f, 1f)]
    public float bigEnemyChance = 0.25f;
    [Range(0f, 1f)]
    public float shooterEnemyChance = 0.25f;

    public float startDelay = 2f;
    public float spawnRate = 2f;
    public int enemiesPerSpawn = 3;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        InvokeRepeating("SpawnEnemy", startDelay, spawnRate);
    }

    void SpawnEnemy()
    {
        if (GameManager.instance != null && GameManager.instance.isTimeFrozen) return;
        float height = cam.orthographicSize;
        float width = height * cam.aspect;

        for (int i = 0; i < enemiesPerSpawn; i++)
        {
            float randomX = Random.Range(-width - 1f, width + 1f);
            float randomY = Random.Range(-height - 1f, height + 1f);
            Vector2 randomPosition = new Vector2(randomX, randomY);

            GameObject prefabToSpawn = enemyPrefab;
            float roll = Random.value;

            if (bigEnemyPrefab != null && roll < bigEnemyChance)
            {
                prefabToSpawn = bigEnemyPrefab;
            }
            else if (shooterEnemyPrefab != null && roll < bigEnemyChance + shooterEnemyChance)
            {
                prefabToSpawn = shooterEnemyPrefab;
            }

            Instantiate(prefabToSpawn, randomPosition, Quaternion.identity);
        }
    }
}