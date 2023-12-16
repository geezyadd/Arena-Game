using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    [SerializeField] private float _bulletForce;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _ricochetRadius;
    [SerializeField] private float _ricochetSpeed;
    private Camera _camera;
    private Vector3 _fireDirection;
    private Rigidbody _bulletRB;
    private float _bulletDamage;
    private bool _isRicochet = false;
    private GameObject _ricochetTarget;
    private float _ricochetCounter = 0;
    private bool _isBulletKillEnemy = false;

    public void SetBulletDamage(float bulletDamage)
    {
        _bulletDamage = bulletDamage;
    }

    private void Start()
    {
        _camera = Camera.main;
        _fireDirection = _camera.transform.forward;
        _bulletRB = GetComponent<Rigidbody>();
        BulletMovement(_fireDirection, _bulletForce);
    }

    private void BulletMovement(Vector3 fireDirection, float bulletForce)
    {
        _bulletRB.velocity = fireDirection * bulletForce;
    }

    private void Update()
    {
        RicochetMove();
    }
    private void RicochetMove() 
    {
        if (_isRicochet)
        {
            float step = _ricochetSpeed * Time.deltaTime;
            Vector3 newPosition = Vector3.MoveTowards(transform.position, _ricochetTarget.transform.position, step);
            _bulletRB.MovePosition(newPosition);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log(collision.gameObject.name);
            IDamagable enemy = collision.collider.GetComponent<IDamagable>();
            _isBulletKillEnemy = enemy.TakeDamage(_bulletDamage); //damage
            float chance = Random.value;
            float chanceRicochet = 0.3f;
            Collider[] nearbyEnemies = Physics.OverlapSphere(transform.position, _ricochetRadius);
            if (chance < chanceRicochet && !_isRicochet && nearbyEnemies.Length != 0)
            {
                _isRicochet = true;
                _bulletRB.velocity = Vector3.zero;
                _bulletRB.useGravity = false;
                foreach (Collider enemyCollider in nearbyEnemies)
                {
                    if (enemyCollider.CompareTag("Enemy"))
                    {
                        //if(enemyCollider.gameObject == collision.gameObject) 
                        //{
                        //    continue;
                        //}
                        if(enemyCollider.gameObject != collision.gameObject)
                        {
                            _ricochetTarget = enemyCollider.gameObject;
                            _ricochetCounter += 1;
                        }
                        else 
                        {
                            Destroy(gameObject);
                        }
                    }
                }
            }
            else if(_ricochetCounter >= 1) 
            {
                _isRicochet = false;
                _bulletRB.useGravity = true;
                //_isBulletKillEnemy = enemy.TakeDamage(_bulletDamage);
                Destroy(gameObject);
            }
            else 
            {
                Destroy(gameObject);
            }


        }
    }
}