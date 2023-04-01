using UnityEngine;
using Enemies;
using Enemies.RedDragon;
using System;

namespace Player
{
    public class PlayerScore : MonoBehaviour
    {
        private int _scrore;
        public int Score => _scrore;

        public event Action OnPlayerScoreChangeEvent;

        private void Awake()
        {
            EnemyDie.OnEnemyMakeScoreEvent += OnSetScore;
            RedDragonDie.OnDragonMakeScoreEvent += OnSetScore;
        }

        private void OnSetScore(int score)
        {
            _scrore += score;
            OnPlayerScoreChangeEvent?.Invoke();
        }

        private void OnDestroy()
        {
            EnemyDie.OnEnemyMakeScoreEvent -= OnSetScore;
            RedDragonDie.OnDragonMakeScoreEvent -= OnSetScore;
        }
    }
}

