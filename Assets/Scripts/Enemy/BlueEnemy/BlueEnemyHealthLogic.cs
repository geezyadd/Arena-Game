using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemyHealthLogic : MonoBehaviour, IDamagable
{
    [SerializeField] private float _�urrentHealth;
    public bool TakeDamage(float damageAmount)
    {
        _�urrentHealth -= damageAmount;
        if (_�urrentHealth <= 0)
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
        if(_�urrentHealth < 0 || _�urrentHealth == 0) 
        {
            Death();
        }
    }
    private void Death() 
    {
        Destroy(gameObject);
    }
}
