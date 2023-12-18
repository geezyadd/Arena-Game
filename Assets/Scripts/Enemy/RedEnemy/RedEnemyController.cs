using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RedEnemyController : MonoBehaviour, IDamagable, IBounty
{
    StatsAddedEvent statsAddedEvent = new StatsAddedEvent();
    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    [SerializeField] private float _flyDistance;
    [SerializeField] private float _airTimeDelay;
    [SerializeField] private float _startFlySpeed;
    [SerializeField] private float _endFlySpeed;
    [SerializeField] private GameObject _redEnemyMesh;
    [SerializeField] private float _bounty;
    public event Action OnEnemyDestroyed;
    [SerializeField] private GameObject _player;
    private bool _isFlying = true;
    public void AddBountyAddedEventListener(UnityAction<float> listener)
    {
        statsAddedEvent.AddListener(listener);
    }
    public bool TakeDamage(float damageAmount)
    {
        _health -= damageAmount;
        if (_health <= 0)
        {
            return true;
        }
        return false;
    }
    private void HealthChecker()
    {
        if (_health < 0 || _health == 0)
        {
            Death();
        }
    }
    private void Death()
    {
        statsAddedEvent?.Invoke(_bounty); 
        Destroy(gameObject);
    }
    private void Start()
    {
        StatsEventManager.AddEventInvoker(this);
        _player = GameObject.Find("Player");
    }
    private void Update()
    {
        HealthChecker();
        transform.LookAt(_player.transform);
        StartFly();
        if (transform.position.y > _flyDistance) 
        {
            _isFlying = false;
        }
        if(_isFlying == false) 
        {
            StartCoroutine(MoveToPlayerCoroutine(_airTimeDelay));
        }
    }
    
    private void MoveToPlayer()
    {
        Vector3 direction = _player.transform.position - transform.position;
        float distanceThisFrame = _endFlySpeed * Time.deltaTime;
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }
    
    private void StartFly() 
    {
        if (_isFlying)
        {
            float newY = Mathf.Lerp(transform.position.y, transform.position.y + _flyDistance, _startFlySpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
    private IEnumerator MoveToPlayerCoroutine(float delayTime)
    {
        yield return new WaitForSeconds(delayTime); 
        MoveToPlayer();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.collider.gameObject.GetComponent<IDamagable>().TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        OnEnemyDestroyed?.Invoke();
        statsAddedEvent.RemoveAllListeners();
    }
}
