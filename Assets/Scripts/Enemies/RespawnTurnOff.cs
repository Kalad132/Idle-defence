using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shooting;

namespace Enemies
{
    public class RespawnTurnOff : MonoBehaviour
    {
        [SerializeField] private List<Target> _bosses;
        [SerializeField] private Spawner _spawner;
        [SerializeField] private AllTargets _allTargets;

        private int _counter;

        private void OnEnable()
        {
            foreach (Target boss in _bosses)
                boss.Died += OnBossDeath;
        }

        private void OnDisable()
        {
            foreach (Target boss in _bosses)
                boss.Died -= OnBossDeath;
        }

        private void OnBossDeath()
        {
            _counter++;
            if (_counter == _bosses.Count)
            {
                _spawner.enabled = false;
                KIllALL();
            }
            
        }

        private int AliveTargets()
        {
            int count = 0;
            foreach (Target target in _allTargets.All)
                if (target.Alive)
                    count++;
            return count;
        }

        private void KIllALL()
        {
            while (AliveTargets() > 0)
                foreach (Target target in _allTargets.All)
                    if (target.Alive)
                    {
                        target.Kill();
                        break;
                    }
        }
    }
}