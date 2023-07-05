using UnityEngine;
using TMPro;
using Levels;

namespace UI
{
    public class LevelTitle : Title
    {
        [SerializeField] private float _hideDelay;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Save _save;

        private void Awake()
        {
            _text.text = _save.GetLevel().ToString();
        }

        private void Start()
        {
            Show();
            Invoke(nameof(Hide), _hideDelay);
        }
    }
}