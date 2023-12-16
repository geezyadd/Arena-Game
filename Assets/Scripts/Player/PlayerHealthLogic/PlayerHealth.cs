using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamagable, IStrengthDamageble
{
    [SerializeField] private PlayerDescriptor _descriptor;
    public bool TakeDamage(float damageAmount)
    {
        _descriptor.PlayerHealth -= damageAmount;
        return true;
    }
    public void TakeStrengthDamage(float strengthDamage)
    {
        if (strengthDamage != 0)
        {
            if (_descriptor.Strength - strengthDamage < 0)
            {
                _descriptor.Strength = 0;
            }
            else
            {
                _descriptor.Strength -= strengthDamage;
            }
        }
    }
    private void Update()
    {
        HealthChecker();
    }
    private void HealthChecker() 
    {
        if(_descriptor.PlayerHealth < 0) { Death(); }
    }
    private void Death() 
    {
        Debug.Log("You die!!!");
    }

    
}
