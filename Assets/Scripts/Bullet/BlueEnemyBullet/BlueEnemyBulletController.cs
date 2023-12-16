using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlueEnemyBulletController : MonoBehaviour
{
    
    [SerializeField] private float _bulletSpeed;
    private GameObject _player;
    [SerializeField] private float _bulletStrengthDamage;
    [SerializeField] private Vector3 _target;
    [SerializeField] private bool _isPlayerTeleported = false;
    public void SetBulletDamage(float bulletDamage) 
    {
        _bulletStrengthDamage = bulletDamage;
    }
    private void Start()
    {
        _isPlayerTeleported = false;
        _player = GameObject.Find("Player");
        TeleportOnPlayArea.Instance.OnTeleport.AddListener(SetIsPlayerTeleported);
    }

    private void Update()
    {
        if (!_isPlayerTeleported) 
        {
            _target = _player.transform.position;
        }
        Movement(_target);
        TargetRiched();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.name == "Player") 
        {
            collision.collider.gameObject.GetComponent<IStrengthDamageble>().TakeStrengthDamage(_bulletStrengthDamage);
            DestroyBullet();
        }
    }
    private void Movement(Vector3 position) 
    {
        Vector3 direction = position - transform.position;
        float distanceThisFrame = _bulletSpeed * Time.deltaTime;
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }
    private void SetIsPlayerTeleported() 
    {
        _isPlayerTeleported = true;
    }
    private void TargetRiched() 
    {
        if(transform.position == _target) 
        {
            DestroyBullet();
        }
    }
    private void DestroyBullet() 
    {
        Destroy(gameObject);
    }
}
