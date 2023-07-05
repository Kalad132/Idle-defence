using UnityEngine;

namespace Shooting
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Damage _basicDamage;
        [SerializeField] private float _speed;
        [SerializeField] private float _speedRandomness;
        [SerializeField] private float _fullDamageDistance;
        [SerializeField] private float _noDamageDistance;

        private Target _target;
        private Vector3 _startPosition;
        private Vector3 _targetPosition;
        private float _flytime;
        private float _timer;
        private Damage _damage;
        private bool _canDamage;

        private void Awake()
        {
            gameObject.SetActive(false);
            _speed += Random.Range(-_speedRandomness, _speedRandomness);
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer <= _flytime)
                transform.position = Vector3.Lerp(_startPosition, _targetPosition, _timer / _flytime);
            else
                EndFly();
        }

        public void Init(Target target, Transform creator, float damageMultiplier = 1)
        {
            _startPosition = transform.position;
            _target = target;
            _targetPosition = _target.Position;
            Vector3 targetPath = _target.Position - transform.position;
            transform.rotation = Quaternion.LookRotation(targetPath);
            _flytime = targetPath.magnitude / _speed;
            _damage = new Damage(_basicDamage, creator);
            _damage.MultiplyValue(damageMultiplier);
            _canDamage = true;
            _target.Died += CancelDamage;
            gameObject.SetActive(true);
        }

        private void EndFly()
        {
            if (_canDamage)
            {
                _target.TakeDamage(CalculateDamage());
                CancelDamage();
            }
            Destroy(gameObject);
        }

        private void CancelDamage()
        {
            _canDamage = false;
            _target.Died -= CancelDamage;
        }

        private Damage CalculateDamage()
        {
            float distance = Vector3.Distance(_startPosition, _targetPosition);
            if (distance <= _fullDamageDistance)
                { }
            else if (distance > _noDamageDistance)
                _damage.MultiplyValue(0);
            else
            {
                float damageMult = 1 - (distance - _fullDamageDistance) / (_noDamageDistance - _fullDamageDistance);
                _damage.MultiplyValue(damageMult);
            }
            FloatingNumbers.Singletron.Spawn(transform.position, _damage.Value);
            return _damage;
        }
    }
}