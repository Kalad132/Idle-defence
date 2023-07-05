using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Tutorial
{
    public class TutorialStep : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        public Transform Target => _target;

        public event UnityAction<TutorialStep> Completed;

        protected void Complete()
        {
            Completed?.Invoke(this);
        }
    }
}