using UnityEngine;
using Player;

namespace UI
{
    public class GameOverBanner : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverBanner;
        private PlayerDie _playerDie;

        private void Start()
        {
            _playerDie = FindObjectOfType<PlayerDie>();

            _playerDie.OnPlayerDieEvent += OnViewGameOverBanner;
        }

        private void OnViewGameOverBanner() => 
            _gameOverBanner.SetActive(true);

        private void OnDestroy() => 
            _playerDie.OnPlayerDieEvent -= OnViewGameOverBanner;
    }
}

