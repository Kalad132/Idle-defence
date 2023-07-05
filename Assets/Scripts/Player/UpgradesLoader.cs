using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Levels;
using Shooting;

namespace Player
{
    public class UpgradesLoader : MonoBehaviour
    {
        [SerializeField] private PlayerHP _hp;
        [SerializeField] private Shooter _shooter;
        [SerializeField] private GameObject _handTemplate;

        private Save _save;

        private void Awake()
        {
            _save = FindObjectOfType<Save>();
        }

        private void Start()
        {
            if (_save == null)
                return;
            float hpMult = GetHPMultiplier(_save);
            _hp.Multiply(hpMult);
            float damageMult = GetDamageMultiplier(_save);
            _shooter.DamageMultiplier = damageMult;
            _shooter.FirerateMultiplier = 1;
            int handsAmount = GetHandsCount(_save);
            SpawnHands(handsAmount);
        }

        private float GetHPMultiplier(Save save)
        {
            int upgradesCount = save.GetUpgradeHP();
            return upgradesCount * 0.3f + 1f;
        }

        private float GetDamageMultiplier(Save save)
        {
            int upgradesCount = save.GetUpgradeDamage();
            return upgradesCount * 0.5f + 1f;
        }

        private int GetHandsCount(Save save)
        {
            int upgradesCount = save.GetUpgradeHands();
            return upgradesCount * 10;
        }

        private void SpawnHands(int amount)
        {
            for (var i = 0; i<amount; i++)
            {
                Instantiate(_handTemplate, transform.position, transform.rotation);
            }
        }
    }
}