using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Unity.AI.Navigation;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject blueEnemyPrefab;
    [SerializeField] private GameObject redEnemyPrefab;
    private NavMeshSurface navMeshSurface;

    private float spawnInterval = 10f;
    private float minSpawnInterval = 6f;
    private int maxEnemies = 30;
    private int blueEnemyCount = 0;
    private int redEnemyCount = 0;

    private void Start()
    {
        navMeshSurface = FindObjectOfType<NavMeshSurface>();
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (spawnInterval > minSpawnInterval)
            {
                spawnInterval -= 2f;
            }

            SpawnEnemy(blueEnemyPrefab, 1, ref blueEnemyCount);
            SpawnEnemy(redEnemyPrefab, 4, ref redEnemyCount);
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab, int ratio, ref int enemyCount)
    {
        if (enemyCount < maxEnemies)
        {
            for (int i = 0; i < ratio; i++)
            {
                Vector3 spawnPoint = GetRandomSpawnPoint();
                GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
                // You might need to set up other enemy properties here

                enemyCount++;
            }
        }
    }

    private Vector3 GetRandomSpawnPoint()
    {
        NavMeshHit hit;
        Vector3 randomPoint = Vector3.zero;

        while (true)
        {
            randomPoint = navMeshSurface.transform.position + new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));

            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                break;
            }
        }

        return hit.position;
    }
}
