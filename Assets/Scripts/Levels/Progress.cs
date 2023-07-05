using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Levels
{
    public class Progress : MonoBehaviour
    {
        private float _value;

        public float Value => _value;

        public event UnityAction<float> ValueChanged;

        protected void Set(float value)
        {
            if (value < 0 || value > 1)
                throw new System.ArgumentException();
            _value = value;
            ValueChanged?.Invoke(_value);
        }
    }
}