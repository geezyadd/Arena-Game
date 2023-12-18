using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class WeaponController : MonoBehaviour
{
    public UnityEvent ultimateStrengthReset;
    private ExternalDevicesInputReader _inputReader;
    [SerializeField] private GameObject _bulletPrephab;
    [SerializeField] private GameObject _bulletSpawnPoint;
    [SerializeField] private float _bulletDamage;
    private GameObject[] _allEnemys;

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
            _allEnemys = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemys in _allEnemys)
            {
                Destroy(enemys);
                ultimateStrengthReset.Invoke();
            }
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
    private void OnDestroy()
    {
        ultimateStrengthReset.RemoveAllListeners();
    }
}

