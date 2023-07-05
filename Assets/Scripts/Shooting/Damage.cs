using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooting
{
    [System.Serializable]
    public struct Damage
    {
        [SerializeField] private float _value;
        [SerializeField] private bool _canKillBoss;

        [HideInInspector]
        public Transform AggressionSource { get; private set; }

        public Damage(float value, bool cankillBoss)
        {
            _value = value;
            _canKillBoss = cankillBoss;
            AggressionSource = null;
        }

        public Damage(Damage damage, Transform aggressionSource)
        {
            _value = damage.Value;
            _canKillBoss = damage.CanKillBoss;
            AggressionSource = aggressionSource;
        }

        public float Value => _value;
        public bool CanKillBoss => _canKillBoss;

        public void MultiplyValue(float value)
        {
            _value *= value;
        }
    }
}