using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooting
{
    public class Shotgun : Weapon
    {
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private Projectile _projectile;
        [SerializeField] private int _pellets;
        [SerializeField] private float _angle;

        protected override void MakeShot(Targeter targeter, Transform agressionSource, float damageMult)
        {
            Vector3 direction = targeter.CurrentTarget.Position - transform.position;
            SpawnPelets(direction);
            List<Target> targets = targeter.FindInCone(direction, _angle);
            SpawnBullets(targets, agressionSource, damageMult);
        }

        private void SpawnPelets(Vector3 direction)
        {
            for (int i=0; i<_pellets; i++)
            {
                float sideAngle = ((float)i / (_pellets - 1) - 0.5f) * _angle;
                float randomVertialAmgle = Random.Range(-_angle, _angle);
                var rotation = Quaternion.Euler(Quaternion.LookRotation(direction).eulerAngles + Vector3.up * sideAngle + Vector3.right * randomVertialAmgle);
                Instantiate(_particle, View.ShootPoint.position, rotation);
            }
        }

        private void SpawnBullets(List<Target> targets, Transform agressionSource, float damageMult)
        {
            foreach (Target target in targets)
            {
                Projectile projectile = Instantiate(_projectile, View.ShootPoint.position, Quaternion.identity);
                projectile.Init(target, agressionSource, damageMult);
            }
        }
    }
}