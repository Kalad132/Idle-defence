using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base
{
    [System.Serializable]
    public class SoldierUpgradeData
    {
        [SerializeField] private float _damageMult;
        [SerializeField] private float _firerateMult;
        [SerializeField] private float _sizeMult;

        public float DamageMult => _damageMult;
        public float FirerateMult => _firerateMult;
        public float SizeMult => _sizeMult;
    }
}