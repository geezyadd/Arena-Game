using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemyHealthLogic : MonoBehaviour, IDamagable
{
    [SerializeField] private float _ņurrentHealth;
    public bool TakeDamage(float damageAmount)
    {
        _ņurrentHealth -= damageAmount;
        if (_ņurrentHealth <= 0)
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
        if(_ņurrentHealth < 0 || _ņurrentHealth == 0) 
        {
            Death();
        }
    }
    private void Death() 
    {
        Destroy(gameObject);
    }
}
