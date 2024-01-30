using System;
using UnityEngine;
using UnityEngine.Events;

public class BlueEnemyHealthLogic : MonoBehaviour, IDamagable, IBounty
{
    StatsAddedEvent statsAddedEvent = new StatsAddedEvent();
    private float _сurrentHealth;
    [SerializeField] private float _bounty;
    [SerializeField] private float _health;
    public bool TakeDamage(float damageAmount)
    {
        _сurrentHealth -= damageAmount;
        if (_сurrentHealth <= 0)
        {
            return true;
        }
        return false;
    }
    public void AddBountyAddedEventListener(UnityAction<float> listener)
    {
        statsAddedEvent.AddListener(listener);
    }
    private void OnEnable()
    {
        _сurrentHealth = _health;
    }
    private void Start()
    {
        StatsEventManager.AddEventInvoker(this);
        _сurrentHealth = _health;
    }
    private void Update()
    {
        HealthChecker();
    }
    private void HealthChecker() 
    {
        if(_сurrentHealth < 0 || _сurrentHealth == 0) 
        {
            Death();
        }
    }
    private void Death() 
    {
        statsAddedEvent?.Invoke(_bounty);
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        statsAddedEvent.RemoveAllListeners();
    }
}
