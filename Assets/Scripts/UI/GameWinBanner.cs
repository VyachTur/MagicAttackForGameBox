using UnityEngine;
using Managers;

namespace UI
{
    public class GameWinBanner : MonoBehaviour
    {
        [SerializeField] private GameObject _gameWinBanner;
        private EnemySpawner _enemySpawner;

        private void Start()
        {
            _enemySpawner = FindObjectOfType<EnemySpawner>();

            _enemySpawner.OnAllEnemiesKillEvent += OnViewWinBanner;
        }

        private void OnViewWinBanner()
        {
            Invoke("WinBannerActivate", 1f);
        }

        private void WinBannerActivate() => 
            _gameWinBanner.SetActive(true);

        private void OnDestroy() => 
            _enemySpawner.OnAllEnemiesKillEvent -= OnViewWinBanner;
    }
}

