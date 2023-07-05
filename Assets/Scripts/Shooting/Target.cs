using UnityEngine;
using UnityEngine.Events;
using Levels;

namespace Shooting
{
    public class Target : MonoBehaviour
    {
        [SerializeField] private float _maxHP;
        [SerializeField] private Collider _collider;
        [SerializeField] private bool _isBoss;

        private float _hp;

        public bool Alive { get; private set; }

        public bool IsBoss => _isBoss;

        public float MaxHP => _maxHP;

        public Vector3 Position => _collider.bounds.center + Vector3.up * Random.Range(0, _collider.bounds.size.y)/2;

        public event UnityAction Died;
        public event UnityAction Hit;
        public event UnityAction<Damage> Damaged;
        public event UnityAction<float> HPchanged;


        private void Awake()
        {
            if (_isBoss)
            {
                Save save = FindObjectOfType<Save>();
                _maxHP = save.GetLevel() * 20;
            }
            _hp = _maxHP;
            HPchanged?.Invoke(_hp);
            Alive = true;
        }

        public void TakeDamage(Damage damage)
        {
            _hp -= damage.Value;
            if (_hp < 0)
                _hp =0;
            HPchanged?.Invoke(_hp);
            Hit?.Invoke();
            Damaged?.Invoke(damage);
            if (_isBoss == false || damage.CanKillBoss == true)
                if (_hp <= 0 && Alive)
                    Kill();
        }

        public void Kill()
        {
            _hp = 0;
            HPchanged?.Invoke(_hp);
            Died?.Invoke();
            Alive = false;
        }

        public void MultiplyHP(float value)
        {
            _hp *= value;
            _maxHP *= value;
        }
    }
}