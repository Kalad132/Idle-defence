using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooting
{
    public class Pistol : Weapon
    {
        [SerializeField] private Projectile _projectile;

        protected override void MakeShot(Targeter targeter, Transform agressionSource, float damageMult)
        {
            Projectile projectile = Instantiate(_projectile, View.ShootPoint.position, Quaternion.identity);
            projectile.Init(targeter.CurrentTarget, agressionSource, damageMult);
        }
    }
}