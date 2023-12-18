using System.Collections;
using UnityEngine;
namespace BlueEnemy 
{
    public class BlueEnemyShootControll : MonoBehaviour
    {
        [SerializeField] private GameObject _bulletPrephab;
        [SerializeField] private GameObject _bulletSpawnPoint;
        [SerializeField] private float _bulletStrengthDamage;
        private bool _isPlayerInVision;
        private bool _isFiring = false;

        private void Start()
        {
            BlueEnemyController.BlueEnemyVisionEvent += IsPlayerInVisionSet;
        }
        private void Update()
        {
            Shoot();
        }

        private void IsPlayerInVisionSet(bool value) 
        {
            if(value == true) { _isPlayerInVision = true; }
            if(value == false) { _isPlayerInVision = false; }
        }
        private void Shoot() 
        {
            if (_isPlayerInVision && !_isFiring)
            {
                _isFiring = true;
                StartCoroutine(FireCoroutine(3.0f));
            }
        }
        IEnumerator FireCoroutine(float delayTime)
        {
            while (_isPlayerInVision)
            {
                BulletInstantiate();

                yield return new WaitForSeconds(delayTime);
            }

            _isFiring = false;
        }
        private void BulletInstantiate()
        {
            GameObject bullet = Instantiate(_bulletPrephab, _bulletSpawnPoint.transform.position, Quaternion.identity);
            BlueEnemyBulletController bulletController = bullet.GetComponent<BlueEnemyBulletController>();
            bulletController.SetBulletDamage(_bulletStrengthDamage);
        }
    }

}
