using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 2f;
    public int enemiesPerSpawn = 3; // Numărul de inamici generați în același timp
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        InvokeRepeating("SpawnEnemy", 0f, spawnRate);
    }

    void SpawnEnemy()
    {
        float height = cam.orthographicSize;
        float width = height * cam.aspect;

        // Generăm numărul dorit de inamici
        for (int i = 0; i < enemiesPerSpawn; i++)
        {
            float randomX = Random.Range(-width - 1f, width + 1f);
            float randomY = Random.Range(-height - 1f, height + 1f);
            Vector2 randomPosition = new Vector2(randomX, randomY);

            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        }
    }
}