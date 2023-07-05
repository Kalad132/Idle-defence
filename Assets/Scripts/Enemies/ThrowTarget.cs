using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class ThrowTarget : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particle;

        public void GetHit()
        {
            _particle.Play();
        }
    }
}