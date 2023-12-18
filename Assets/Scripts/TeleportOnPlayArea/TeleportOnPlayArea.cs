using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class TeleportOnPlayArea : MonoBehaviour
{
    public static TeleportOnPlayArea Instance { get; private set; }
    public UnityEvent OnTeleport;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private GameObject _spawn;
    [SerializeField] private float _bundaryX;
    [SerializeField] private float _bundaryZ;
    [SerializeField] private float _distanceFromEnemy;
    [SerializeField] private GameObject _gameplayTriggerArea;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private Vector3 GetRandomPointFarFromEnemies(float searchRadius)
    {
        NavMeshHit hit;
        int maxAttempts = 30;
        int attempt = 0;

        while (attempt < maxAttempts)
        {
            Vector3 randomDirection = Random.insideUnitSphere.normalized * searchRadius;
            Vector3 randomPoint = _spawn.transform.position + randomDirection;

            if (NavMesh.SamplePosition(randomPoint, out hit, searchRadius, NavMesh.AllAreas))
            {
                Collider[] colliders = Physics.OverlapSphere(randomPoint, searchRadius, _enemyLayer);

                if (colliders.Length == 0)
                {
                    if (BoundaryCheck(hit.position))
                    {
                        return hit.position;
                    }
                }
            }
            attempt++;
        }
        return _spawn.transform.position;
    }
    private bool BoundaryCheck(Vector3 value) 
    {
        if((Math.Abs(value.x) < _bundaryX) && (Math.Abs(value.z) < _bundaryZ)) 
        {
            return true;
        }
        return false;
    }
   
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == _gameplayTriggerArea.name) 
        {
            OnTeleport.Invoke();
            transform.position = GetRandomPointFarFromEnemies(_distanceFromEnemy);
        }
    }
    private void OnDestroy()
    {
        OnTeleport.RemoveAllListeners();
    }
}
