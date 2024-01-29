using UnityEngine;
using UnityEngine.AI;
namespace BlueEnemy
{
    public class BlueEnemyController : MonoBehaviour
    {
        public delegate void BlueEnemyVision(bool value);
        public static event BlueEnemyVision BlueEnemyVisionEvent;
        [SerializeField] private float _stopDistanceToShoot;
        private GameObject _player;
        private NavMeshAgent _blueEnemy;
        [SerializeField] private GameObject _bulletSpawnPoint;
        
        private void Start()
        {
            _player = GameObject.Find("Player");
            _blueEnemy = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            BlueEnemyMovement();
            EventBulletVision();
        }
        

        private bool BlueVisionChecker()
        {
            Vector3 direction = _player.transform.position - _bulletSpawnPoint.transform.position;
            Ray ray = new Ray(_bulletSpawnPoint.transform.position, direction.normalized);
            Debug.DrawLine(_bulletSpawnPoint.transform.position, _player.transform.position, Color.red);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.gameObject.name == "Player")
                {
                    return true;
                }
            }
            return false;
        }

        private void BlueEnemyMovement()
        {
            switch (CheckState())
            {
                case 1:
                    _blueEnemy.SetDestination(_player.transform.position);
                    _blueEnemy.Resume();
                    break;

                case 2:
                    _blueEnemy.Stop();
                    break;

                case 3:
                    _blueEnemy.Resume();
                    break;
            }

            BlueVisionChecker();

            int CheckState()
            {
                float distance = Vector3.Distance(_player.transform.position, transform.position);

                if (distance > _stopDistanceToShoot)
                {
                    return 1;
                }
                else if (distance < _stopDistanceToShoot)
                {
                    return 2;
                }
                else if (!BlueVisionChecker())
                {
                    return 3;
                }

                return 0;
            }
        }
        private void EventBulletVision()
        {
            bool valueToSend = BlueVisionChecker();
            if (BlueEnemyVisionEvent != null)
            {
                BlueEnemyVisionEvent(valueToSend);
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object other)
        {
            return base.Equals(other);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
