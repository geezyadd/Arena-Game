using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBulletController : MonoBehaviour
{
    StatsRicochetAddedEvent statsRicochetAddedEvent = new StatsRicochetAddedEvent();
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
    private float _chanceRicochet;
    public void SetBulletDamage(float bulletDamage)
    {
        _bulletDamage = bulletDamage;
    }
    public void AddBountyAddedEventListener(UnityAction listener)
    {
        statsRicochetAddedEvent.AddListener(listener);
    }
    private void OnEnable()
    {
        StartCoroutine(DestroyBulletAfter10Seconds());
        StatsRicochetAddedEventManager.AddEventInvoker(this);
        _camera = Camera.main;
        _fireDirection = _camera.transform.forward;
        _bulletRB = GetComponent<Rigidbody>();
        BulletMovement(_fireDirection, _bulletForce);
    }

    private void BulletMovement(Vector3 fireDirection, float bulletForce)
    {
        _bulletRB.velocity = fireDirection * bulletForce;
    }

    private void FixedUpdate()
    {
        RicochetMove();
        ChanceRicochet();
    }
    private void RicochetMove() 
    {
        if (_isRicochet && _ricochetTarget != null)
        {
            float step = _ricochetSpeed * Time.deltaTime;
            Vector3 newPosition = Vector3.MoveTowards(transform.position, _ricochetTarget.transform.position, step);
            _bulletRB.MovePosition(newPosition);
        }
        else if(_isRicochet && _ricochetTarget == null) 
        {
            gameObject.SetActive(false);
        }
    }
    private void ChanceRicochet() 
    {
        if (PlayerStats.Instance.GetIsPlayerLowHp()) 
        {
            _chanceRicochet = 1f;
        }
        else 
        {
            _chanceRicochet = 0.3f;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IDamagable enemy = collision.collider.GetComponent<IDamagable>();
            _isBulletKillEnemy = enemy.TakeDamage(_bulletDamage);
            float chance = Random.value;
            Collider[] nearbyEnemies = Physics.OverlapSphere(transform.position, _ricochetRadius);
            if (chance < _chanceRicochet && !_isRicochet && nearbyEnemies.Length != 0)
            {
                _bulletRB.velocity = Vector3.zero;
                _bulletRB.useGravity = false;
                foreach (Collider enemyCollider in nearbyEnemies)
                {
                    if (enemyCollider.CompareTag("Enemy") && enemyCollider.gameObject != collision.gameObject)
                    {
                        _ricochetTarget = enemyCollider.gameObject;
                        _ricochetCounter += 1;
                        _isRicochet = true;
                        break; 
                    }
                }
                if (!_isRicochet)
                {
                    gameObject.SetActive(false);
                }
            }
            else if(_ricochetCounter >= 1) 
            {
                if (_isBulletKillEnemy) 
                { 
                    statsRicochetAddedEvent?.Invoke();
                }
                _isRicochet = false;
                _bulletRB.useGravity = true;
                gameObject.SetActive(false);
            }
            else 
            {
                gameObject.SetActive(false);
            }
        }
    }
    IEnumerator DestroyBulletAfter10Seconds()
    {
        yield return new WaitForSeconds(10);
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        statsRicochetAddedEvent.RemoveAllListeners();
    }
}