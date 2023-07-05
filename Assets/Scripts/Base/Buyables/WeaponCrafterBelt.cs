using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stacking;

namespace Base
{
    public class WeaponCrafterBelt : Belt
    {
        [SerializeField] private Bag _bag;

        protected override void OnMoveEnd(Resourse resourse)
        {
            _bag.TryAdd(resourse);
        }
    }
}