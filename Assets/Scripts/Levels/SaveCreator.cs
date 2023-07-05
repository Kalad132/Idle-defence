using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Levels
{
    public class SaveCreator : MonoBehaviour
    {
        [SerializeField] private Save _save;
        [SerializeField] private int _level;
        [SerializeField] private int _health;
        [SerializeField] private int _damage;
        [SerializeField] private int _money;
        [SerializeField] private int _upgradeDiff;
        [SerializeField] private bool _auto;

        private void Awake()
        {
            if (_auto == false)
                return;
            Apply();
        }

        [ContextMenu("Apply")]
        private void Apply()
        {
            _save.SetLevel(_level);
            _save.SetUpgradeHP(_health);
            _save.SetUpgradeDamage(_damage);
            _save.SetUpgradeHands(_money);
        }

        private void OnValidate()
        {
            _upgradeDiff = _level - 1 - _damage - _money - _health;
        }
    }
}