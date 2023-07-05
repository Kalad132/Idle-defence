using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shooting;

namespace Stacking
{
    public class StockedWeapon : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;

        public Weapon Weapon => _weapon;
    }
}