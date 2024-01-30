using System;
using UnityEngine;

public class BlueEnemyBulletController : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletStrengthDamage;
    [SerializeField] private Vector3 _target;
    [SerializeField] private bool _isPlayerTeleported = false;
    private GameObject _player;
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

    private void FixedUpdate()
    {
        if (!_isPlayerTeleported) 
        {
            _target = _player.transform.position;
        }
        else 
        {
            Collider collider = GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false; 
            }
        }
        Movement(_target);
        TargetRiched();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Player")) 
        {
            collision.collider.gameObject.GetComponent<IStrengthDamageble>().TakeStrengthDamage(_bulletStrengthDamage);
            DisableBullet();
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
        Vector3 bulletPosition = new Vector3((float)Math.Round(transform.position.x, 1), (float)Math.Round(transform.position.y, 0), (float)Math.Round(transform.position.z, 1));
        Vector3 playerLastPosition = new Vector3((float)Math.Round(_target.x, 1), (float)Math.Round(_target.y, 0), (float)Math.Round(_target.z, 1));
        if (bulletPosition == playerLastPosition)
        {
            DisableBullet();
        }
    }
    private void DisableBullet() 
    {
        gameObject.SetActive(false);
    }
}
