using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class WeaponController : MonoBehaviour
{
    public UnityEvent ultimateStrengthReset;
    private ExternalDevicesInputReader _inputReader;
    [SerializeField] private GameObject _bulletPrephab;
    [SerializeField] private GameObject _bulletSpawnPoint;
    [SerializeField] private float _bulletDamage;
    [SerializeField] private EnemySpawner _enemySpawner;
    private GameObject[] _allEnemys;
    [SerializeField] private List<GameObject> _playerBulletPool = new List<GameObject>();
    private void Start()
    {
        _inputReader = GetComponent<ExternalDevicesInputReader>();
    }
    private bool _isFiring = false; 
    void Update()
    {
        Fire();
        Ultimate();
    }
    private void Fire()
    {
        if (_inputReader.Attack && !_isFiring)
        {
            _isFiring = true; 
            StartCoroutine(FireCoroutine(0.5f));
        }
    }
    private void Ultimate() 
    {
        if (_inputReader.Ultimate && PlayerStats.Instance.GetIsUltimateReady())
        {
            _enemySpawner.DisableAllActiveEnemies();
            ultimateStrengthReset.Invoke();
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
        GameObject bullet = GetPlayeBullet(_bulletPrephab);
        PlayerBulletController bulletController = bullet.GetComponent<PlayerBulletController>();
        bulletController.SetBulletDamage(_bulletDamage);
    }
    private GameObject GetPlayeBullet(GameObject bulletPrefab)
    {
        GameObject pooledBullet = _playerBulletPool.Find(enemy => enemy.activeSelf == false && enemy.CompareTag(bulletPrefab.tag));

        if (pooledBullet == null)
        {
            pooledBullet = Instantiate(bulletPrefab, _bulletSpawnPoint.transform.position, Quaternion.identity);
            _playerBulletPool.Add(pooledBullet);
            
        }
        if(pooledBullet != null)
        {
            pooledBullet.SetActive(true);
            pooledBullet.transform.position = _bulletSpawnPoint.transform.position;
        }
        return pooledBullet;
    }
    private void OnDestroy()
    {
        ultimateStrengthReset.RemoveAllListeners();
    }
}

