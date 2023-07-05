using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Levels
{
    public class WinParticles : MonoBehaviour
    {
        private Game _game;

        private ParticleSystem[] _particles;

        private void Awake()
        {
            _game = FindObjectOfType<Game>();
            _particles = GetComponentsInChildren<ParticleSystem>();
        }

        private void OnEnable()
        {
            _game.LevelCompleted += OnLevelCompleted;
        }

        private void OnDisable()
        {
            _game.LevelCompleted -= OnLevelCompleted;
        }

        private void OnLevelCompleted(int level)
        {
            Play();
        }

        private void Play()
        {
            foreach (ParticleSystem particle in _particles)
                particle.Play();
        }
    }
}