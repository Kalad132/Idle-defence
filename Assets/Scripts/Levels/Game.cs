using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemies;
using UnityEngine.SceneManagement;
using UI;
using UnityEngine.Events;

namespace Levels
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private AllEnemies _enemies;
        [SerializeField] private Save _save;
        [SerializeField] private Title _win;

        public event UnityAction<int> LevelCompleted;
        public event UnityAction<int> LevelStarted;

        private void OnEnable()
        {
            _enemies.Removed += OnRemoved;
        }

        private void OnDisable()
        {
            _enemies.Removed -= OnRemoved;
        }

        private void Start()
        {
            LevelStarted?.Invoke(_save.GetLevel());
        }

        public void MoveToLobby()
        {
            _save.IncreaseLevel();
            ReturnToLobby();
        }

        private void OnRemoved(Enemy enemy)
        {
            if (_enemies.Count == 0)
                Win();
        }

        private void Win()
        {
            _win.Show();
            LevelCompleted?.Invoke(_save.GetLevel());
        }

        private void ReturnToLobby()
        {
            SceneManager.LoadScene("Lobby");
        }
    }
}