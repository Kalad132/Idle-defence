using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Levels;
using UnityEngine.UI;

namespace UI
{
    public class NextLevelButton : MonoBehaviour
    {
        [SerializeField] private UpgradeViews _views;
        [SerializeField] private Button _button;

        private Game _game;

        private void Awake()
        {
            _button.interactable = false;
            _game = FindObjectOfType<Game>();
        }

        private void OnEnable()
        {
            _views.Selected += OnViewSelected;
            _button.onClick.AddListener(_game.MoveToLobby);
        }

        private void OnDisable()
        {
            _views.Selected -= OnViewSelected;
            _button.onClick.RemoveListener(_game.MoveToLobby);
        }

        private void OnViewSelected(UpgradeView view)
        {
            _button.interactable = true;
        }
    }
}