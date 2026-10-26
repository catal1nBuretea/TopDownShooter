using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 2f;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, spawnRate);
    }

    void SpawnEnemy()
    {
        // Alege coordonate aleatoare între limitele ecranului
        float randomX = Random.Range(-9f, 9f);
        float randomY = Random.Range(-5f, 5f);
        Vector2 randomPosition = new Vector2(randomX, randomY);

        Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
    }
}
