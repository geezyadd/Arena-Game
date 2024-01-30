using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Unity.AI.Navigation;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _blueEnemyPrefab;
    [SerializeField] private GameObject _redEnemyPrefab;
    private NavMeshSurface _navMeshSurface;
    private float _spawnInterval = 10f; 
    private float _minSpawnInterval = 6f;
    [SerializeField] private int _enemyCounter = 0;
    [SerializeField] private int _maxEnemyInScene;
    [SerializeField] private List<GameObject> _enemyPool = new List<GameObject>();
    public void DisableAllActiveEnemies()
    {
        foreach (GameObject enemy in _enemyPool)
        {
            if (enemy != null && enemy.activeSelf)
            {
                enemy.SetActive(false);
            }
        }
    }
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
                }
        }
    }
    private GameObject GetEnemy(GameObject enemyPrefab)
    {
        GameObject pooledEnemy = _enemyPool.Find(enemy => enemy.activeSelf == false && enemy.CompareTag(enemyPrefab.tag));

        if (pooledEnemy == null)
        {
            pooledEnemy = Instantiate(enemyPrefab);
            _enemyPool.Add(pooledEnemy);
            
        }
        if(pooledEnemy != null)
        {
            pooledEnemy.SetActive(true);
        }
        return pooledEnemy;
    }
    private void Update()
    {
        _enemyCounter = CountActiveEnemies(_enemyPool);
    }

    private int CountActiveEnemies(List<GameObject> objectList)
    {
        int activeCount = 0;

        foreach (GameObject obj in objectList)
        {
            if (obj != null && obj.activeSelf)
            {
                activeCount++;
            }
        }

        return activeCount;
    }
    private void SpawnEnemy(GameObject enemyPrefab, int ratio)
    {
        for (int i = 0; i < ratio; i++)
        {
            GameObject newEnemy = GetEnemy(enemyPrefab);
            newEnemy.transform.position = GetRandomSpawnPoint();
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
    
}
