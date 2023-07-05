using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base
{
    public class ConveierBelt : Belt
    {
        [SerializeField] private ResourceConverter _converter;

        protected override void OnMoveEnd(Resourse resourse)
        {
            _converter.Convert(resourse);
        }
    }
}