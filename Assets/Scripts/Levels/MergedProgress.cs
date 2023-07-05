using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Levels
{
    public class MergedProgress : Progress
    {
        [SerializeField] private Progress _progress1;
        [SerializeField] private Progress _progress2;
        [Range(0, 1)]
        [SerializeField] private float _weight;

        private void OnEnable()
        {
            _progress1.ValueChanged += OnProgressChanged;
            _progress2.ValueChanged += OnProgressChanged;
        }

        private void OnDisable()
        {
            _progress1.ValueChanged -= OnProgressChanged;
            _progress2.ValueChanged -= OnProgressChanged;
        }

        private void OnProgressChanged(float value)
        {
            float progress = CalculateProgress();
            Set(progress);
        }

        private float CalculateProgress()
        {
            float progress1 = _progress1.Value *  _weight;
            float progress2 = _progress2.Value * (1 - progress1);
            return (progress1 + progress2);
        }

        private void OnValidate()
        {
            float progress = CalculateProgress();
            Set(progress);
        }
    }
}