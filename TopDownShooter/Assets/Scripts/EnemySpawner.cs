using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject bigEnemyPrefab;
    [Range(0f, 1f)]
    public float bigEnemyChance = 0.3f;

    public float startDelay = 2f; // Pauza de la începutul jocului (în secunde)
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
        float height = cam.orthographicSize;
        float width = height * cam.aspect;

        for (int i = 0; i < enemiesPerSpawn; i++)
        {
            float randomX = Random.Range(-width - 1f, width + 1f);
            float randomY = Random.Range(-height - 1f, height + 1f);
            Vector2 randomPosition = new Vector2(randomX, randomY);

            GameObject prefabToSpawn = enemyPrefab;
            if (bigEnemyPrefab != null && Random.value < bigEnemyChance)
            {
                prefabToSpawn = bigEnemyPrefab;
            }

            Instantiate(prefabToSpawn, randomPosition, Quaternion.identity);
        }
    }
}