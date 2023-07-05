using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shooting;

namespace Base
{
    public class UpgradableSoldier : MonoBehaviour
    {
        [SerializeField] private Upgrades _upgrades;
        [SerializeField] private Shooter _shooter;
        [SerializeField] private List<SoldierUpgradeData> _upgradesData;
        [SerializeField] private Transform _view;

        private void OnEnable()
        {
            _upgrades.UpgradeLevelChanged += OnUpgradeLevelChanged;
        }

        private void OnDisable()
        {
            _upgrades.UpgradeLevelChanged -= OnUpgradeLevelChanged;
        }

        private void OnUpgradeLevelChanged(int level)
        {
            level--;
            SoldierUpgradeData data = _upgradesData[level];
            _shooter.DamageMultiplier = data.DamageMult;
            _shooter.FirerateMultiplier = data.FirerateMult;
            StartCoroutine(Scale(data.SizeMult));
        }

        private IEnumerator Scale(float multiplier)
        {
            float time = 1f;
            float timer = 0f;
            Vector3 start = _view.localScale;
            while (timer < time)
            {
                timer += Time.deltaTime;
                _view.localScale = Vector3.Lerp(start, start * multiplier, timer / time);
                yield return null;
            }
        }
    }
}