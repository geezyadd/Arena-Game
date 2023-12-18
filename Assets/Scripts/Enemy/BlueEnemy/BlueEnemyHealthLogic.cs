using System;
using UnityEngine;
using UnityEngine.Events;

public class BlueEnemyHealthLogic : MonoBehaviour, IDamagable, IBounty
{
    StatsAddedEvent statsAddedEvent = new StatsAddedEvent();
    [SerializeField] private float _�urrentHealth;
    [SerializeField] private float _bounty;
    public event Action OnEnemyDestroyed;
    public bool TakeDamage(float damageAmount)
    {
        _�urrentHealth -= damageAmount;
        if (_�urrentHealth <= 0)
        {
            return true;
        }
        return false;
    }
    public void AddBountyAddedEventListener(UnityAction<float> listener)
    {
        statsAddedEvent.AddListener(listener);
    }
    private void Start()
    {
        StatsEventManager.AddEventInvoker(this);
    }
    private void Update()
    {
        HealthChecker();
    }
    private void HealthChecker() 
    {
        if(_�urrentHealth < 0 || _�urrentHealth == 0) 
        {
            Death();
        }
    }
    private void Death() 
    {
        //OnEnemyDestroyed?.Invoke();
        statsAddedEvent?.Invoke(_bounty);
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        OnEnemyDestroyed?.Invoke();
        statsAddedEvent.RemoveAllListeners();
    }
   
}
