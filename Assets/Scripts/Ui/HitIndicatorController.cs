using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitIndicatorController : MonoBehaviour
{
    [SerializeField] private GameObject _hitIndicator;

    private void Update()
    {
        if (PlayerStats.Instance.GetIsTakeDamage()) 
        {
            _hitIndicator.SetActive(true);
            StartCoroutine(HitIndicatorDisabled(1));
        }
    }
    private IEnumerator HitIndicatorDisabled(float delayTime)
    {
        PlayerStats.Instance.SetIsTakeDamageFalse();
        yield return new WaitForSeconds(delayTime); 
        _hitIndicator.SetActive(false);
    }
}
