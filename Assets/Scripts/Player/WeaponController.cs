using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private ExternalDevicesInputReader _inputReader;
    [SerializeField] private GameObject _bulletPrephab;
    [SerializeField] private GameObject _bulletSpawnPoint;
    [SerializeField] private float _bulletDamage;
    private void Start()
    {
        _inputReader = GetComponent<ExternalDevicesInputReader>();
    }
    private bool _isFiring = false; 

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (_inputReader.Attack && !_isFiring)
        {
            _isFiring = true; 
            StartCoroutine(FireCoroutine(1.0f));
        }
    }

    IEnumerator FireCoroutine(float delayTime)
    {
        while (_inputReader.Attack) 
        {
            BulletInstantiate(); 

            yield return new WaitForSeconds(delayTime); 
        }

        _isFiring = false; 
    }

    private void BulletInstantiate()
    {
        GameObject bullet = Instantiate(_bulletPrephab, _bulletSpawnPoint.transform.position, Quaternion.identity);
        PlayerBulletController bulletController = bullet.GetComponent<PlayerBulletController>();
        bulletController.SetBulletDamage(_bulletDamage);
    }
}

