using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Unity.AI.Navigation;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _blueEnemyPrefab;
    [SerializeField] private GameObject _redEnemyPrefab;
    private NavMeshSurface _navMeshSurface;

    private float _spawnInterval = 10f;
    private float _minSpawnInterval = 6f;
    [SerializeField] private int _enemyCounter = 0;
    [SerializeField] private int _maxEnemyInScene;

    private void Start()
    {
        _navMeshSurface = FindObjectOfType<NavMeshSurface>();
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
                yield return new WaitForSeconds(_spawnInterval);

                if (_spawnInterval > _minSpawnInterval)
                {
                    _spawnInterval -= 2f;
                }
                if (_enemyCounter + 5 <= _maxEnemyInScene) 
                {   
                    SpawnEnemy(_blueEnemyPrefab, 1);
                    SpawnEnemy(_redEnemyPrefab, 4);
                    _enemyCounter += 5;
                }
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab, int ratio)
    {
        for (int i = 0; i < ratio; i++)
        {
            Vector3 spawnPoint = GetRandomSpawnPoint();
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
            if(enemyPrefab == _blueEnemyPrefab) 
            {
                newEnemy.GetComponent<BlueEnemyHealthLogic>().OnEnemyDestroyed += DecreaseEnemyCount;
            }
            if(enemyPrefab == _redEnemyPrefab) 
            {
                newEnemy.GetComponent<RedEnemyController>().OnEnemyDestroyed += DecreaseEnemyCount;
            }
        }
        
    }

    private Vector3 GetRandomSpawnPoint()
    {
        NavMeshHit hit;
        Vector3 randomPoint = Vector3.zero;

        while (true)
        {
            randomPoint = _navMeshSurface.transform.position + new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));

            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                break;
            }
        }

        return hit.position;
    }
    private void DecreaseEnemyCount() 
    {
        _enemyCounter --;
    }
}
