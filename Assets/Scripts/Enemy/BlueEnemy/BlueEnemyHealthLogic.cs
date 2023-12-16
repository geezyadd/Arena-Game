using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemyHealthLogic : MonoBehaviour, IDamagable
{
    [SerializeField] private float _ñurrentHealth;
    public bool TakeDamage(float damageAmount)
    {
        _ñurrentHealth -= damageAmount;
        if (_ñurrentHealth <= 0)
        {
            return true;
        }
        return false;
    }
    private void Update()
    {
        HealthChecker();
    }
    private void HealthChecker() 
    {
        if(_ñurrentHealth < 0 || _ñurrentHealth == 0) 
        {
            Death();
        }
    }
    private void Death() 
    {
        Destroy(gameObject);
    }
}
