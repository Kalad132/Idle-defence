using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooting
{
    public class ImpactEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _template;


        private void OnDestroy()
        {
            Instantiate(_template, transform.position, transform.rotation);
        }
    }
}