using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class RedEnemyController : MonoBehaviour, IDamagable, IBounty
{
    StatsAddedEvent statsAddedEvent = new StatsAddedEvent();
    private float _currentHealth;
    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    [SerializeField] private float _flyDistance;
    [SerializeField] private float _airTimeDelay;
    [SerializeField] private float _startFlySpeed;
    [SerializeField] private float _endFlySpeed;
    [SerializeField] private GameObject _redEnemyMesh;
    [SerializeField] private float _bounty;
    [SerializeField] private GameObject _player;
    private bool _isFlying = true;
    public void AddBountyAddedEventListener(UnityAction<float> listener)
    {
        statsAddedEvent.AddListener(listener);
    }
    public bool TakeDamage(float damageAmount)
    {
        _currentHealth -= damageAmount;
        if (_currentHealth <= 0)
        {
            return true;
        }
        return false;
    }

    private void OnEnable()
    {
        _currentHealth = _health;
        _isFlying = true;
    }

    private void HealthChecker()
    {
        if (_currentHealth < 0 || _currentHealth == 0)
        {
            Death();
        }
    }
    private void Death()
    {
        statsAddedEvent?.Invoke(_bounty); 
        gameObject.SetActive(false);
    }
    private void Start()
    {
        StatsEventManager.AddEventInvoker(this);
        _player = GameObject.Find("Player");
        _currentHealth = _health;
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
            if(gameObject.activeSelf)
            {
                StartCoroutine(MoveToPlayerCoroutine(_airTimeDelay));
            }
            
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
            gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        statsAddedEvent.RemoveAllListeners();
        StopAllCoroutines();
    }
}
