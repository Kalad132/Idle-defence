using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Levels
{
    public class ResetProgress : MonoBehaviour
    {
        [SerializeField] private Save _save;

        [SerializeField] private bool _buttonForReset;

        private void OnValidate()
        {
            if (_buttonForReset)
            {
                _buttonForReset = false;
                _save.ResetLevel();
                Debug.LogWarning("Progress was reseted");
            }
        }
    }
}