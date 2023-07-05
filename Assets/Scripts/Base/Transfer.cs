using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stacking;

namespace Base
{
    public class Transfer
    {
        private const float TakeRate = 10f;
        private readonly int WaveSize;
        private readonly float WaveCooldown;

        private float _cooldown;
        private int _waveIndex;

        public Bag Source { get; private set; }
        public Bag Target { get; private set; }

        public bool Active => Source!= null && Target!=null && Source.gameObject.activeInHierarchy && Target.gameObject.activeInHierarchy;

        public Transfer(Bag source, Bag target)
        {
            Source = source;
            Target = target;
            if (target.ResourseID.Value == 3)
            {
                WaveSize = 3;
                WaveCooldown = 0.3f;
            }
            else 
            {
                WaveSize = 1;
                WaveCooldown = 0f;
            }
        }

        public void Tick()
        {
            _cooldown -= Time.deltaTime;
            if (_cooldown < 0)
            {
                ResetCooldown();
                TryTransfer();
            }
        }

        private void TryTransfer()
        {
            Bag source = Source;
            Bag target = Target;
            if (source.ResourseID.Equals(target.ResourseID) == false)
                return;
            Resourse resourse = source.TryRemove(target.ResourseID);
            if (resourse == null)
                return;
            if (target.TryAdd(resourse) == false)
                source.TryAdd(resourse);
        }

        private void ResetCooldown()
        {
            _cooldown = 1 / TakeRate;
            if (Source.Count > TakeRate)
                _cooldown /= Source.Count / TakeRate;
            _waveIndex++;
            if (_waveIndex % WaveSize == 0)
            {
                _waveIndex = 0;
                _cooldown += WaveCooldown;
            }
        }
    }
}