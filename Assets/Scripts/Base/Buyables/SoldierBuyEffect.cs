using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base
{
    public class SoldierBuyEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleTemplate;


        private void OnEnable()
        {
            Instantiate(_particleTemplate, transform.position, transform.rotation, transform);
        }
    }
}